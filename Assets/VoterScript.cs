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
			if (gl.activeDistrict.voterList.Count == 0) {
				handleMove(gl.activeDistrict);
			} else {
				bool hasNeighbor = false;
				foreach (var neighbor in gl.voterGrid.getNeighbors(voter)) {
					if (neighbor.district == gl.activeDistrict)
						hasNeighbor = true;
				}
				if (hasNeighbor) {
					handleMove(gl.activeDistrict);
				}
			}
		}
	}

	public void handleMove(District toDistrict) {
		addToDistrict (toDistrict);
		updateDistrictNeighbors(toDistrict);
		gl.advanceTurn();
	}

	private void updateDistrictNeighbors(District toDistrict){
		List<Voter> neighbors = gl.voterGrid.getNeighbors(voter);
		foreach (var neighbor in neighbors) {
			if (neighbor.district == null){
				toDistrict.neighborSet.Add(neighbor);
			} else {
				neighbor.district.neighborSet.Remove(voter);
			}
		}
	}

	public void addToDistrict(District toDistrict) {
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		renderer.color = toDistrict.color;
		voter.district = toDistrict;
		voter.district.updateCount (voter);
		gl.voterGrid.freeVoterSet.Remove(voter);
	}
}
