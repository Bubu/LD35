using UnityEngine;
using System.Collections;

public class Voter {
	
	public GameObject gameobj;
	public GameObject bgobj;
	private BoxCollider2D box;
	public int row, col;
	public int district;
	public Player player;

	public static Voter copyFrom (Voter old){
		Voter copy = new Voter(old.col, old.row, old.player);
		copy.district = old.district;
		return copy;
	}

	public Voter (int x, int y, Player player) {
		this.player = player;
		col = x;
		row = y;
		this.district = -1;
	}

	public void initialize (int x, int y, int size, Player player)
	{
		bgobj = new GameObject ();
		bgobj.name = "BG (" + x + "," + y + ")";
		bgobj.transform.position = new Vector3 ((float)x * size, (float)y * size, 0);
		bgobj.transform.localScale = new Vector3 (size, size, 1);
		SpriteRenderer rend = bgobj.AddComponent<SpriteRenderer> ();
		rend.sprite = GameResources.Instance.bgSprite;
		rend.sortingOrder = -1;
		bgobj.AddComponent<BoxCollider2D> ();
		bgobj.GetComponent<BoxCollider2D> ().offset = new Vector2 (0.5f, 0.5f);
		VoterScript voterscript = bgobj.AddComponent<VoterScript> ();
		voterscript.voter = this;
		gameobj = new GameObject ();
		gameobj.name = "Sprite (" + x + "," + y + ")";
		gameobj.transform.position = new Vector3 ((float)x * size, (float)y * size, 0);
		gameobj.transform.localScale = new Vector3 (size, size, 1);
		rend = gameobj.AddComponent<SpriteRenderer> ();
		rend.sprite = player.sprite;
	}

	public void Color() {


	}

}
