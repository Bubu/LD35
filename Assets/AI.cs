using UnityEngine;
using System;
using System.Collections.Generic;

public class AI
{
	private GameLogic gl;

	public AI (GameLogic gl)
		{
		this.gl = gl;
		}

	public void doMove(){
		bool freeDistrictDone = false;
		foreach(var district in gl.districtList){
			if(district.voterList.Count == 0){
				foreach(var neighbor in district.neighborSet){
					simulateTurnWith(neighbor);
				}
			} else if(!freeDistrictDone) {
				foreach(var freeVoter in gl.voterGrid.freeVoterSet){
					simulateTurnWith(freeVoter);
				}
				freeDistrictDone = true;
			}
		}

		int selectedDistrict = 0;
		Voter selectedVoter = gl.voterGrid.array[2,2];
		gl.activateDistrict(selectedDistrict);
		selectedVoter.bgobj.GetComponent<VoterScript>().addToDistrict();
	}

	private void simulateTurnWith(Voter voter){
		//TODO!
		List<District> tempDistrictList = gl.districtList; //DeepCopy????!?
	}
}

