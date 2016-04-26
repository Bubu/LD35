using UnityEngine;
using System;
using System.Collections.Generic;

public class GerryAI : AI {
	public GerryAI(GameLogic gl, Player player):base(gl, player){
	}

	public override Move findBestMove(List<District> districtList, VoterGrid voterGrid, int playerIndex, int depth, String logString = ""){
		float bestScore=0;
		District bestDistrict = null;
		Tuple<int,int> bestVoterIndex = null;
		bool freeDistrictDone = false;
		foreach(var district in gl.districtList){
			if(district.voterList.Count > 0){
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
		return new Move(bestDistrict,bestVoterIndex,bestScore);

	}

	private float simulateTurnWith(Tuple<int,int> selectedVoterIndex, District selectedDistrict){
		List<District> tempDistrictList = new List<District> (gl.districtList);
		for(int i = 0; i< gl.districtList.Count; i++){
			tempDistrictList[i] = District.copyFrom(gl.districtList[i]);
		}
		VoterGrid tempVotergrid = VoterGrid.copyFrom(gl.voterGrid);
		addVoterToDistrict(selectedVoterIndex, selectedDistrict.index, tempDistrictList, tempVotergrid);

		if (tempVotergrid.freeVoterSet.Count > 0) {
			Move enemyMove = selectMove (tempDistrictList, tempVotergrid, (player.index + 1) % 2);
			addVoterToDistrict (enemyMove.voterIndex, enemyMove.district.index, tempDistrictList, tempVotergrid);

			if (tempVotergrid.freeVoterSet.Count > 0) {
				Move myMove = selectMove (tempDistrictList, tempVotergrid, player.index);
				return myMove.score;
			} else {
				return 1-enemyMove.score;
			}
		} else {
			return (float)0.5;
		}
	}

	private float simulateSecondTurnWith(List<District> districtList, VoterGrid voterGrid, Tuple<int,int> selectedVoterIndex, int selectedDistrict, int playerIndex){
		List<District> tempDistrictList = new List<District> (districtList);
		for(int i = 0; i< districtList.Count; i++){
			tempDistrictList[i] = District.copyFrom(districtList[i]);
		}
		VoterGrid tempVotergrid = VoterGrid.copyFrom(voterGrid);
		addVoterToDistrict(selectedVoterIndex, selectedDistrict, tempDistrictList, tempVotergrid);

		return score(tempDistrictList, tempVotergrid, playerIndex);
	}

	private Move selectMove(List<District> districtList, VoterGrid voterGrid, int playerIndex){
		//Select turn for opponent
		bool freeDistrictDone = false;
		float bestScore=0;
		District bestDistrict = null;
		Tuple<int,int> bestVoterIndex = null;
		List<float> scoreList = new List<float>();
		foreach(var district in districtList){
			if(district.voterList.Count > 0){
				foreach(var neighborindex in district.neighborSet){
					float score = simulateSecondTurnWith(districtList, voterGrid, neighborindex, district.index, playerIndex);
					scoreList.Add (score);
					if (score >= bestScore){
						bestScore = score;
						bestDistrict = district;
						bestVoterIndex = neighborindex;
					}
				}
			} else if(!freeDistrictDone) {
				foreach(var voterIndex in voterGrid.freeVoterSet){
					float score = simulateSecondTurnWith(districtList, voterGrid, voterIndex, district.index, playerIndex);
					scoreList.Add (score);
					if (score >= bestScore){
						bestScore = score;
						bestDistrict = district;
						bestVoterIndex = voterIndex;
					}
				}
				freeDistrictDone = true;
			}
		}
		if (bestDistrict == null) {
			Debug.LogError ("No possible move for Player" + playerIndex);
			foreach (var score in scoreList) {
				Debug.Log (score);
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
				distScore = (float)0.1*probability(totalNeighbors, neighborCounter[0], -diff+1);
			}
			score[0] += distScore;
			if (distScore < -1)
				Debug.Log ("First distScore: " + distScore);
			//player 1
			if(diff<=-1){
				distScore = 1 - probability(totalNeighbors, neighborCounter[0], -diff);
			} else {
				distScore = (float)0.1*probability(totalNeighbors, neighborCounter[1], diff+1);
			}
			score[1] += distScore;
			if (distScore < -1)
				Debug.Log ("Second distScore: " + distScore);

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
				answ *= (float)(k-i)/(n-i);
			}
		}else{
			answ = 0;
		}
		return answ;
	}

	private void addVoterToDistrict(Tuple<int,int> voterIndex, int district, List<District> districtList, VoterGrid voterGrid){
		voterGrid.array[voterIndex.First,voterIndex.Second].district = district;
		districtList[district].voterList.Add (voterIndex);
		voterGrid.freeVoterSet.Remove(voterIndex);

		List<Voter> neighbors = voterGrid.getNeighbors(voterGrid.array[voterIndex.First,voterIndex.Second]);
		foreach (var neighbor in neighbors) {
			if (neighbor.district == -1){
				districtList[district].neighborSet.Add(Tuple.New(neighbor.col,neighbor.row));
			} else {
				districtList[neighbor.district].neighborSet.Remove(voterIndex);
			}
		}
	}

}

