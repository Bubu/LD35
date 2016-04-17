using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class VoterGrid {
	public int x = 10;
	public int y = 10;
	public double rel = 0.5;
	private string name_obj;
	public Voter[,] array;

	public int sprite_size;

	private Sprite sprite1;
	private Sprite sprite2;

	private SpriteRenderer rend;

	// Use this for initialization
	public VoterGrid () {
		loadSprites ();
		array = new Voter[x,y];
		System.Random rnd = new System.Random ();
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				array[i,j] = new Voter(i,j,sprite_size);
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
		sprite_size = tex2.width;
		sprite1 = Sprite.Create(tex1,new Rect(0,0,sprite_size,sprite_size),new Vector2(0,0),sprite_size);
		sprite2 = Sprite.Create(tex2,new Rect(0,0,sprite_size,sprite_size),new Vector2(0,0),sprite_size);
	}
		

	public float getSize(){
		return x * sprite_size;
	}

	public List<Voter> getNeighbors(int pos_x, int pos_y) {
		List<Voter> list = new List<Voter>();

		if (pos_x > 0)
			list.Add (array [pos_x - 1, pos_y]);
		if (pos_x < x - 1)
			list.Add (array [pos_x + 1, pos_y]);
		if (pos_y > 0)
			list.Add (array [pos_x, pos_y - 1]);
		if (pos_y < y - 1)
			list.Add (array [pos_x, pos_y + 1]);
		return list;
	}
		
}
