using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class District {

	public string districtName;
	public Color color;
	public List<Voter> voterList;
	public List<int> counterList;
	public GameObject textBox;

	public District(string name, GameObject textBox, Color color){
		this.color = color;
		this.districtName = name;
		this.voterList = new List<Voter>();
		this.counterList = new List<int> ();
		this.counterList.Add (0); //Counter Player A
		this.counterList.Add (0); //Counter Player B
		this.textBox = textBox;
	}

	public void updateCount(Voter voter){
		voterList.Add (voter);

		counterList [0] += 1;
		string scoreText = "" + counterList[0];
		foreach (var count in counterList.Skip(1)) {
			scoreText += " / " + count;
		}
		textBox.GetComponent<Text> ().text = scoreText;

	}
}
