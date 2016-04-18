using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class VoterGrid {
	private string name_obj;
	public Voter[,] array;
	public HashSet<Voter> freeVoterSet;
	GameLogic gl;

	private SpriteRenderer rend;

	// Use this for initialization
	public VoterGrid () {
		
		gl = GameObject.Find ("GameLogic").GetComponent<GameLogic>();
		array = new Voter[gl.x, gl.x];
		freeVoterSet = new HashSet<Voter>();
		System.Random rnd = new System.Random ();
		int sprite_size = GameConfig.Instance.sprite_size;
		int numFirstAnimal = (int) Math.Round (gl.ratio * Math.Pow(gl.x,2));
		int numSecondAnimal = (int) Math.Pow(gl.x,2) - numFirstAnimal;
		List<int> voterList = new List<int> ();

		for (int i = 0; i < numFirstAnimal; i++)
			voterList.Add (0);
		for (int i = 0; i < numSecondAnimal; i++)
			voterList.Add (1);

		int remainingVoters = (int) Math.Pow(gl.x,2);

		for (int i = 0; i < gl.x; i++) {
			for (int j = 0; j < gl.x; j++) {

				int listIndex = rnd.Next(0, remainingVoters);
				int playerIndex = voterList[listIndex];
				voterList.RemoveAt (listIndex);
				array[i,j] = new Voter(i,j, sprite_size, gl.playerList[playerIndex]);
				freeVoterSet.Add(array[i,j]);
				remainingVoters--;
			}
		}
	}
		
	public List<Voter> getNeighbors(Voter voter) {
		List<Voter> list = new List<Voter>();

		int pos_x = voter.col;
		int pos_y = voter.row;

		if (pos_x > 0)
			list.Add (array [pos_x - 1, pos_y]);
		if (pos_x < gl.x - 1)
			list.Add (array [pos_x + 1, pos_y]);
		if (pos_y > 0)
			list.Add (array [pos_x, pos_y - 1]);
		if (pos_y < gl.x - 1)
			list.Add (array [pos_x, pos_y + 1]);
		return list;
	}
		
}
