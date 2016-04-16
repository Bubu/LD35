using UnityEngine;
using System.Collections;
using System;

public class VoterGrid {
	public int x = 10;
	public int y = 10;
	public double rel = 0.6;
	private string name_obj;
	public Voter[,] array;

	private Sprite sprite1;
	private Sprite sprite2;
	public int sprite_size = 100;
	private SpriteRenderer rend;

	// Use this for initialization
	public VoterGrid () {
		loadSprites ();
		array = new Voter[x,y];
		System.Random rnd = new System.Random ();
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				array[i,j] = new Voter((float)(i*sprite_size),(float)(j*sprite_size), sprite_size);
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

	public void loadSprites(){
		Texture2D tex1 = Resources.Load<Texture2D>("sprite_a");
		Texture2D tex2 = Resources.Load<Texture2D>("sprite_b");
		sprite1 = Sprite.Create(tex1,new Rect(0,0,100,100),new Vector2(0,0),100);
		sprite2 = Sprite.Create(tex2,new Rect(0,0,100,100),new Vector2(0,0),100);
	}
		

	public float getSize(){
		return x * sprite_size;
	}
		
}
