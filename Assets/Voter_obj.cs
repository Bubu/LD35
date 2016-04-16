using UnityEngine;
using System.Collections;

public class Voter_obj {
	
	public GameObject gameobj;

	public Voter_obj (float x, float y) {
		gameobj = new GameObject ();
		gameobj.name = "Sprite (" + x + "," + y + ")";
		gameobj.transform.position = new Vector3 (x, y, 0);
		gameobj.AddComponent<SpriteRenderer> ();
		gameobj.AddComponent<GetMoBuUp> ();
	}

}
