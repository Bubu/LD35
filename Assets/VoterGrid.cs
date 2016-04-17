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
	GameLogic gl;

	private SpriteRenderer rend;

	// Use this for initialization
	public VoterGrid () {
		
		gl = GameObject.Find ("GameLogic").GetComponent<GameLogic>();
		array = new Voter[x,y];
		System.Random rnd = new System.Random ();
		int sprite_size = GameConfig.Instance.sprite_size;
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				if (rnd.NextDouble () < rel) {
					array[i,j] = new Voter(i,j, sprite_size, gl.playerList[0]);
				}
				else {
					array[i,j] = new Voter(i,j, sprite_size, gl.playerList[1]);
				}
			}
		}
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
