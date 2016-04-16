using UnityEngine;
using System.Collections;
using System;

public class Pattern : MonoBehaviour {
	public int x = 10;
	public int y = 10;
	public double rel = 0.6;
	private string name_obj;

	public Sprite sprite1;
	public Sprite sprite2;
	public int sprite_size = 100;
	private SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		setSprites ();
		System.Random rnd = new System.Random ();
		Voter_obj[,] array = new Voter_obj[x,y];
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				array[i,j] = new Voter_obj((float)(i*sprite_size),(float)(j*sprite_size), sprite_size);
				rend = array[i,j].gameobj.GetComponent<SpriteRenderer> ();
				if (rnd.NextDouble () < rel) {
					rend.sprite = sprite1;
				}
				else {
					rend.sprite = sprite2;
				}
			}
		}
	}

	public void setSprites(){
		Sprite sprite3 = new Sprite ();
		sprite3 = Sprite.Create (sprite1.texture, new Rect (0, 0, sprite_size, sprite_size), new Vector2 (0, 0), 1);
		Sprite sprite4 = new Sprite ();
		sprite4 = Sprite.Create (sprite2.texture, new Rect (0, 0, sprite_size, sprite_size), new Vector2 (0, 0), 1);
	}
		

	// Update is called once per frame
	void Update () {
			
	}
}
