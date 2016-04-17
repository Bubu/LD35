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
		if (gl.activeDistrict != null && voter.district == null) {
			if (gl.activeDistrict.voterList.Count == 0) {
				addToDistrict ();
			} else {
				List<Voter> list = gl.voterGrid.getNeighbors (voter.col, voter.row);
				bool hasNeighbor = false;
				foreach (var neighbor in list) {
					if (neighbor.district == gl.activeDistrict)
						hasNeighbor = true;
				}
				if (hasNeighbor) {
					addToDistrict ();
				}
			}
		}
	}

	void addToDistrict() {
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		renderer.color = gl.activeDistrict.color;
		voter.district = gl.activeDistrict;
		voter.district.updateCount (voter);
	}
}
