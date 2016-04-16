using UnityEngine;
using System.Collections;

public class GetMoBuUp : MonoBehaviour {

	void OnMouseDown() {
		SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer> ();
		rend.color = new Color(1,1,0);
	}
}
