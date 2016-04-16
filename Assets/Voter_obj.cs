using UnityEngine;
using System.Collections;

public class Voter_obj {
	
	public GameObject gameobj;
	private BoxCollider2D box;

	public Voter_obj (float x, float y, int size) {
		gameobj = new GameObject ();
		gameobj.name = "Sprite (" + x + "," + y + ")";
		gameobj.transform.position = new Vector3 (x, y, 0);
		gameobj.transform.localScale = new Vector3 (size, size, 1);
		gameobj.AddComponent<SpriteRenderer> ();
		gameobj.AddComponent<BoxCollider2D> ();
		gameobj.GetComponent<BoxCollider2D>().offset = new Vector2(0.5f,0.5f);
		gameobj.AddComponent<GetMoBuUp> ();
	}


	public void Color() {


	}

}
