using UnityEngine;
using System;
using System.Collections.Generic;

public class AII
{
	private GameLogic gl;
	private Player player;
	private int searchSteps;

	public AII (GameLogic gl, Player player)
	{
		this.gl = gl;
		this.player = player;
		this.searchSteps = 100;
	}

	private Move findBestMove(List<District> districtList, VoterGrid voterGrid, int playerIndex){

		Move bestMove = new Move (null, null, 0);
		bool finished = false;

		// Do loop over possible districts
		foreach (var district in gl.districtList) {
			if (district.voterList.Count > 0) {
				HashSet<Voter> voterIndexSet = district.neighborSet;
			} else {
				HashSet<Voter> voterIndexSet = gl.voterGrid.freeVoterSet;
				finished = true;
			}

			foreach (var voterIndex in voterIndexSet) {
				float score = scoreThisMove (voterIndex, district, districtList, voterGrid, playerIndex);
				if (score >= bestMove.score) {
					bestMove = new Move (district, voterIndex, score);
				}
			}
			if (finished) break;
		}
	}
	 
	private float scoreThisMove(Tuple<int,int> selVoterIndex, District selDistrict, List<District> districtList, VoterGrid voterGrid, int playerIndex){
		// 1. Copy game situation:
		List<District> tempDistrictList = new List<District> (districtList);
		for(int i = 0; i < districtList.Count; i++){
			tempDistrictList[i] = District.copyFrom(gl.districtList[i]);
		}
		VoterGrid tempVotergrid = VoterGrid.copyFrom(gl.voterGrid);

		// 2. Simulate Move:
		addVoterToDistrict (selVoterIndex, selDistrict, tempDistrictList, tempVotergrid);

		if (playerIndex == player.index && searchSteps > 0) {
			// 3. Select Best Enemy Move:
			Move enemyMove = findBestMove (tempDistrictList, tempVotergrid, (playerIndex + 1) % 2);

			// 4. Simulate best Enemy Move:
			addVoterToDistrict (enemyMove.voterIndex, enemyMove.district, tempDistrictList, tempVotergrid);

			// 5. Select best own Move:
			Move ownMove = findBestMove (tempDistrictList, tempVotergrid, playerIndex);
			float score = ownMove.score;

		} else {

			// 3. Score this situation for the player
			float score = scoreSituation(tempDistrictList, tempVotergrid, playerIndex);
		}
		return score;
	}
	
	private float scoreSituation (List<District> districtList, VoterGrid votergrid, int playerIndex){
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
