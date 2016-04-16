using UnityEngine;
using System.Collections;

public class GetMoBuUp : MonoBehaviour {

	void OnMouseUp() {
		SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer> ();
		rend.color = new Color(1,1,0);
	}
}
