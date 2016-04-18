using UnityEngine;
using System;
using System.Collections.Generic;

public class AI
{
	private GameLogic gl;
	private Player player;

	public AI (GameLogic gl, Player player)
		{
		this.gl = gl;
		this.player = player;
		}

	public void doMove(){
		float bestScore=0;
		District bestDistrict = null;
		Tuple<int,int> bestVoterIndex = null;

		bool freeDistrictDone = false;
		foreach(var district in gl.districtList){
			if(district.voterList.Count == 0){
				foreach(var neighborindex in district.neighborSet){
					float score = simulateTurnWith(neighborindex, district);
					if (score >= bestScore){
						bestScore = score;
						bestDistrict = district;
						bestVoterIndex = neighborindex;
					}
				}
			} else if(!freeDistrictDone) {
				foreach(var voterIndex in gl.voterGrid.freeVoterSet){
					float score = simulateTurnWith(voterIndex, district);
					if (score >= bestScore){
						bestScore = score;
						bestDistrict = district;
						bestVoterIndex = voterIndex;
					}
				}
				freeDistrictDone = true;
			}
		}
		Debug.Log("AI move at: " + bestVoterIndex.First + ", " + bestVoterIndex.Second + " for district number: " + bestDistrict.index);
		Voter selectedVoter = gl.voterGrid.array[bestVoterIndex.First,bestVoterIndex.Second];
		selectedVoter.bgobj.GetComponent<VoterScript>().handleMove(bestDistrict);
	}

	private float simulateTurnWith(Tuple<int,int> selectedVoterIndex, District selectedDistrict){
		List<District> tempDistrictList = new List<District> (gl.districtList);
		for(int i = 0; i< gl.districtList.Count; i++){
			tempDistrictList[i] = District.copyFrom(gl.districtList[i]);
		}
		VoterGrid tempVotergrid = VoterGrid.copyFrom(gl.voterGrid);
		addVoterToDistrict(tempVotergrid.array[selectedVoterIndex.First,selectedVoterIndex.Second], selectedDistrict, tempDistrictList, tempVotergrid);

		Move enemyMove = selectMove(tempDistrictList,tempVotergrid, (player.index + 1)%2);
		addVoterToDistrict(tempVotergrid.array[enemyMove.voterIndex.First,enemyMove.voterIndex.Second], enemyMove.district, tempDistrictList, tempVotergrid);
		Move myMove = selectMove(tempDistrictList,tempVotergrid, player.index);
		return myMove.score;
	}

	private float simulateSecondTurnWith(Tuple<int,int> selectedVoterIndex, District selectedDistrict, int playerIndex){
		List<District> tempDistrictList = new List<District> (gl.districtList);
		for(int i = 0; i< gl.districtList.Count; i++){
			tempDistrictList[i] = District.copyFrom(gl.districtList[i]);
		}
		VoterGrid tempVotergrid = VoterGrid.copyFrom(gl.voterGrid);
		addVoterToDistrict(tempVotergrid.array[selectedVoterIndex.First,selectedVoterIndex.Second], selectedDistrict, tempDistrictList, tempVotergrid);

		return score(tempDistrictList, tempVotergrid, playerIndex);
	}

	private Move selectMove(List<District> districtList, VoterGrid voterGrid, int playerIndex){
		//Select turn for opponent
		bool freeDistrictDone = false;
		float bestScore=0;
		District bestDistrict = null;
		Tuple<int,int> bestVoterIndex = null;

		foreach(var district in districtList){

			if(district.voterList.Count == 0){
				foreach(var neighborindex in district.neighborSet){
					float score = simulateSecondTurnWith(neighborindex, district, playerIndex);
					if (score >= bestScore){
						bestScore = score;
						bestDistrict = district;
						bestVoterIndex = neighborindex;
					}
				}
			} else if(!freeDistrictDone) {
				foreach(var voterIndex in voterGrid.freeVoterSet){
					float score = simulateSecondTurnWith(voterIndex, district, playerIndex);
					if (score >= bestScore){
						bestScore = score;
						bestDistrict = district;
						bestVoterIndex = voterIndex;
					}
				}
				freeDistrictDone = true;
			}
		}
		return new Move(bestDistrict, bestVoterIndex, bestScore);
	}

	private float score (List<District> districtList, VoterGrid votergrid, int playerIndex){
		float[] score = new float[2] {0f,0f};
		int neighborCountSum = 0;
		float neighborCountSqrdSum = 0;
		foreach(var district in districtList){
			int[] districtCounter = new int[2] {0,0};
			foreach(var voterIndex in district.voterList){
				districtCounter[votergrid.array[voterIndex.First,voterIndex.Second].player.index]++;
			}
			int[] neighborCounter = new int[2] {0,0};
			foreach(var neighborIndex in district.neighborSet){
				neighborCounter[votergrid.array[neighborIndex.First,neighborIndex.Second].player.index]++;
			}

			int diff = districtCounter[0] - districtCounter[1];
			int totalNeighbors = neighborCounter[0] + neighborCounter[1];
			//player 0
			float distScore;
			if(diff>=1){
				distScore = 1 - probability(totalNeighbors, neighborCounter[1], diff);
			} else {
				distScore = probability(totalNeighbors, neighborCounter[0], -diff+1);
			}
			score[0] += distScore;
			//player 1
			if(diff<=-1){
				distScore = 1 - probability(totalNeighbors, neighborCounter[0], -diff);
			} else {
				distScore = probability(totalNeighbors, neighborCounter[1], diff+1);
			}
			score[1] += distScore;

			neighborCountSum += district.neighborSet.Count;
			neighborCountSqrdSum += Mathf.Pow(district.neighborSet.Count,2);
		}
		int[] freeCounter = new int[2] {0,0};

		if (votergrid.freeVoterSet.Count > 0) {

			foreach(var voterIndex in votergrid.freeVoterSet){
				freeCounter[votergrid.array[voterIndex.First, voterIndex.Second].player.index]++;
			}

			float freePercentageP0 = (float)freeCounter[0]/(freeCounter[0]+freeCounter[1]);

			score[0] += freePercentageP0*Mathf.Pow(neighborCountSum, 2)/neighborCountSqrdSum;
			score[1] += (1 - freePercentageP0)*Mathf.Pow(neighborCountSum, 2)/neighborCountSqrdSum;
		}
		return score[playerIndex]/(score[playerIndex] + score[(playerIndex + 1) % 2]);
	}

	private float probability(int n, int k, int d){
		float answ = 1;
		if (k >= d){
			for (int i = 0; i< d; i++){
				answ *= (float)(k-i)/(n-1);
			}
		}else{
			answ = 0;
		}
		return answ;
	}

	private void addVoterToDistrict(Voter voter, District district, List<District> districtList, VoterGrid voterGrid){
		voter.district = district.index;
		district.voterList.Add (Tuple.New(voter.col,voter.row));
		voterGrid.freeVoterSet.Remove(Tuple.New(voter.col, voter.row));

		List<Voter> neighbors = gl.voterGrid.getNeighbors(voter);
		foreach (var neighbor in neighbors) {
			if (neighbor.district == -1){
				district.neighborSet.Add(Tuple.New(neighbor.col,neighbor.row));
			} else {
				districtList[neighbor.district].neighborSet.Remove(Tuple.New(voter.col,voter.row));
			}
		}
	}
		
//
//	public void addToDistrict(District toDistrict) {
//		tempFreeVoterSet.Remove(voter);
//	}

}

