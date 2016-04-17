using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoterScript : MonoBehaviour {
	GameLogic gl;
	public Voter voter;

	void Start () {
		gl = GameObject.Find ("GameLogic").GetComponent<GameLogic>();
	}

	void OnMouseUp() {
		if (gl.activeDistrict != null && voter.district == null && gl.activePlayer.isHuman) {
			List<Voter> neighborList = gl.voterGrid.getNeighbors (voter.col, voter.row);
			if (gl.activeDistrict.voterList.Count == 0) {
				handleAction(neighborList);
			} else {
				bool hasNeighbor = false;
				foreach (var neighbor in neighborList) {
					if (neighbor.district == gl.activeDistrict)
						hasNeighbor = true;
				}
				if (hasNeighbor) {
					handleAction(neighborList);
				}
			}
		}
	}

	public void handleAction(List<Voter> neighborList) {
		addToDistrict ();
		updateDistrictNeighbors(neighborList);
		gl.advanceTurn();
	}

	private void updateDistrictNeighbors(List<Voter> neighbors){
		foreach (var neighbor in neighbors) {
			if (neighbor.district == null){
				gl.activeDistrict.neighborSet.Add(neighbor);
			} else {
				neighbor.district.neighborSet.Remove(voter);
			}
		}
	}

	public void addToDistrict() {
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		renderer.color = gl.activeDistrict.color;
		voter.district = gl.activeDistrict;
		voter.district.updateCount (voter);
		gl.voterGrid.freeVoterSet.Remove(voter);
	}
}
