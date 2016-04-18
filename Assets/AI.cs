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

