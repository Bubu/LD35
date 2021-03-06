﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoterScript : MonoBehaviour {
	GameLogic gl;
	public Voter voter;

	void Awake () {
		gl = GameObject.Find ("GameLogic").GetComponent<GameLogic>();
	}

	void OnMouseUp() {
		if (voter.district == -1 && gl.activePlayer.isHuman) {
			if (gl.districtList[gl.activeDistrict].voterList.Count == 0) {
				handleMove(gl.districtList[gl.activeDistrict]);
			} else {
				bool hasNeighbor = false;
				foreach (var neighbor in gl.voterGrid.getNeighbors(voter)) {
					if (neighbor.district == gl.activeDistrict)
						hasNeighbor = true;
				}
				if (hasNeighbor) {
					handleMove(gl.districtList[gl.activeDistrict]);
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
			if (neighbor.district == -1){
				toDistrict.neighborSet.Add(Tuple.New(neighbor.col,neighbor.row));
			} else {
				gl.districtList[neighbor.district].neighborSet.Remove(Tuple.New(voter.col,voter.row));
			}
		}
	}

	public void addToDistrict(District toDistrict) {
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		renderer.color = toDistrict.color;
		voter.district = toDistrict.index;
		gl.districtList[voter.district].updateCount (voter);
		gl.voterGrid.freeVoterSet.Remove(Tuple.New(voter.col,voter.row));
	}
}
