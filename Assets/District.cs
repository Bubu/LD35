using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class District {

	public int index;
	public Color color;
	public List<Tuple<int,int>> voterList;
	public HashSet<Tuple<int,int>> neighborSet;
	public List<int> counterList;
	public GameObject textBox0, textBox1;
	public GameLogic gl;

	public static District  copyFrom(District old){
		District copy = new District(old.index, old.textBox0, old.textBox1, old.color,old.gl);
		copy.voterList = new List<Tuple<int,int>>(old.voterList);
		copy.neighborSet = new HashSet<Tuple<int,int>>(old.neighborSet);
		return copy;
	}

	public District(int index, GameObject textBox0, GameObject textBox1, Color color, GameLogic gl){
		this.color = color;
		this.index = index;
		this.voterList = new List<Tuple<int,int>>();
		this.neighborSet = new HashSet<Tuple<int,int>>();
		this.counterList = new List<int> ();
		this.counterList.Add (0); //Counter Player A
		this.counterList.Add (0); //Counter Player B
		this.textBox0 = textBox0;
		this.textBox1 = textBox1;
		this.gl = gl;
	}

	public void updateCount(Voter voter){
		voterList.Add (Tuple.New(voter.col,voter.row));
		counterList [voter.player.index] += 1;

		textBox0.GetComponent<Text> ().text = "" + counterList[0];
		textBox1.GetComponent<Text> ().text = "" + counterList[1];

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
