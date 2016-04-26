using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class AI
{
	protected GameLogic gl;
	protected Player player;

	public AI (GameLogic gl, Player player)
	{
		this.gl = gl;
		this.player = player;
	}
	public void doMove(){

		District bestDistrict = null;
		Tuple<int,int> bestVoterIndex = null;

		Move selectedMove = findBestMove(gl.districtList, gl.voterGrid, this.player.index, 0);

		bestDistrict = selectedMove.district;
		bestVoterIndex = selectedMove.voterIndex;
		Debug.Log("AI "+ this.GetType().Name + " for player" + this.player.index + "  move at: " + selectedMove);
		Voter selectedVoter = gl.voterGrid.array[bestVoterIndex.First,bestVoterIndex.Second];
		selectedVoter.bgobj.GetComponent<VoterScript>().handleMove(bestDistrict);
	}

	public abstract Move findBestMove(List<District> districtList, VoterGrid voterGrid, int playerIndex, int depth, String logString = "");

}
