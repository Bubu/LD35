using UnityEngine;
using System.Collections;

public class ClickHandler : MonoBehaviour {
	GameLogic gl;
	void Start () {
		gl = GameObject.Find ("GameLogic").GetComponent<GameLogic>();
	}

	void OnMouseUp() {
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		if (gl.activeDistrict != null) {
			renderer.color = gl.activeDistrict.color;
		}
	}
}
