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
		bool freeDistrictDone = false;
		foreach(var district in gl.districtList){
			if(district.voterList.Count == 0){
				foreach(var neighborindex in district.neighborSet){
					simulateTurnWith(neighborindex);
				}
			} else if(!freeDistrictDone) {
				foreach(var voterIndex in gl.voterGrid.freeVoterSet){
					simulateTurnWith(voterIndex);
				}
				freeDistrictDone = true;
			}
		}

		int selectedDistrict = 0;
		Voter selectedVoter = gl.voterGrid.array[2,2];
		selectedVoter.bgobj.GetComponent<VoterScript>().handleMove(gl.districtList[selectedDistrict]);
	}

	private void simulateTurnWith(Tuple<int,int> voterIndex){
		List<District> tempDistrictList = new List<District> (gl.districtList);
		for(int i = 0; i< gl.districtList.Count; i++){
			tempDistrictList[i] = District.copyFrom(gl.districtList[i]);
		}


		VoterGrid tempVotergrid = VoterGrid.copyFrom(gl.voterGrid);


	}

	private float score (List<District> districtList, VoterGrid votergrid){
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
			neighborCountSqrdSum += Math.Pow(district.neighborSet.Count,2);
		}
		int[] freeCounter = new int[2] {0,0};

		if (votergrid.freeVoterSet.Count > 0) {

			foreach(var voterIndex in votergrid.freeVoterSet){
				freeCounter[votergrid.array[voterIndex.First, voterIndex.Second].player.index]++;
			}

			float freePercentageP0 = (float)freeCounter[0]/(freeCounter[0]+freeCounter[1]);

			score[0] += freePercentageP0*Math.Pow(neighborCountSum, 2)/neighborCountSqrdSum;
			score[1] += (1 - freePercentageP0)*Math.Pow(neighborCountSum, 2)/neighborCountSqrdSum;
		}
		return score[player.index]/(score[player.index] + score[(player.index + 1) % 2]);
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

//	private void addVoterToDistrict(Voter voter, District district, List<District> districtList, List<Voter> freeVoterSet){
//		freeVoterSet.Remove(voter);
//		List<Voter> neighbors = gl.voterGrid.getNeighbors(voter);
//		if (neighbor.district == null){
//			district.neighborSet.Add(neighbor);
//		} else {
//			neighbor.district.neighborSet.Remove(voter);
//		}
//	}
//
//	}
//
//	private void updateDistrictNeighbors(District toDistrict){
//		List<Voter> neighbors = gl.voterGrid.getNeighbors(voter);
//		foreach (var neighbor in neighbors) {
//			if (neighbor.district == null){
//				toDistrict.neighborSet.Add(neighbor);
//			} else {
//				neighbor.district.neighborSet.Remove(voter);
//			}
//		}
//	}
//
//	public void addToDistrict(District toDistrict) {
//		tempFreeVoterSet.Remove(voter);
//	}

}

