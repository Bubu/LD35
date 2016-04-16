using UnityEngine;
using System.Collections;

public class ClickHandler : MonoBehaviour {

	void OnMouseUp() {
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		renderer.color = new Color(1,1,0);
	}
}
