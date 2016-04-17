using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class District {

	public int index;
	public Color color;
	public List<Voter> voterList;
	public HashSet<Voter> neighborSet;
	public List<int> counterList;
	public GameObject textBox;
	public GameLogic gl;

	public District(int index, GameObject textBox, Color color){
		this.color = color;
		this.index = index;
		this.voterList = new List<Voter>();
		this.neighborSet = new HashSet<Voter>();
		this.counterList = new List<int> ();
		this.counterList.Add (0); //Counter Player A
		this.counterList.Add (0); //Counter Player B
		this.textBox = textBox;
		gl = GameObject.Find ("GameLogic").GetComponent<GameLogic>();
	}

	public void updateCount(Voter voter){
		voterList.Add (voter);
		counterList [voter.player.index] += 1;
		string scoreText = "" + counterList[0];
		foreach (var count in counterList.Skip(1)) {
			scoreText += " / " + count;
		} 
		textBox.GetComponent<Text> ().text = scoreText;

		if (counterList [0] > counterList [1]) {
			gl.voterDistrictPlayer0 [index].SetActive (true);
			gl.voterDistrictPlayer1 [index].SetActive (false);
		} else if (counterList [0] < counterList [1]) {
			gl.voterDistrictPlayer1 [index].SetActive (true);
			gl.voterDistrictPlayer0 [index].SetActive (false);
		}
		else {
			gl.voterDistrictPlayer0 [index].SetActive (false);
			gl.voterDistrictPlayer1 [index].SetActive (false);
		}
	}
}
