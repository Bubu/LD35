using UnityEngine;
using System.Collections;

public class Voter {
	
	public GameObject gameobj;
	private BoxCollider2D box;
	public int row, col;
	public District district;
	public Player player;

	public Voter (int x, int y, int size) {
		gameobj = new GameObject ();
		gameobj.name = "Sprite (" + x + "," + y + ")";
		gameobj.transform.position = new Vector3 ((float)x*size, (float)y*size, 0);
		gameobj.transform.localScale = new Vector3 (size, size, 1);
		gameobj.AddComponent<SpriteRenderer> ();
		gameobj.AddComponent<BoxCollider2D> ();
		gameobj.GetComponent<BoxCollider2D>().offset = new Vector2(0.5f,0.5f);
		VoterScript voterscript = gameobj.AddComponent<VoterScript> ();
		voterscript.voter = this;
		col = x;
		row = y;
	}


	public void Color() {


	}

}
