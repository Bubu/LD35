using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameSettings : MonoBehaviour {

	public Slider gridSlider;
	public int x;
	public List<Player> playerList;
	public int curPlayerIndex;
	public Button startButton;

	// Use this for initialization
	void Start () {
		//gridSlider = GameObject.Find ("SliderGridSize").GetComponent<Slider>();
		//x = (int)gridSlider.value;
		playerList = new List<Player>();
		playerList.Add (new Player (0));
		playerList.Add (new Player (1));
		DontDestroyOnLoad (this);
		startButton = GameObject.Find ("StartButton").GetComponent<Button>();
		startButton.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	public void setPlayer0Animal(int spriteIndex){
		playerList[0].sprite = GameConfig.Instance.spriteList[spriteIndex];
		for (int button = 0; button < 6; button++) {
			if (button == spriteIndex) {
				GameObject.Find ("BButton" + button).GetComponent<Button> ().interactable = false;
			} else {
				GameObject.Find ("BButton" + button).GetComponent<Button> ().interactable = true;
			}
		}
		if (playerList [1].sprite) {
			startButton.interactable = true;
		}
			
	}

	public void setPlayer1Animal(int spriteIndex){
		playerList[1].sprite = GameConfig.Instance.spriteList[spriteIndex];
		for (int button = 0; button < 6; button++) {
			if (button == spriteIndex) {
				GameObject.Find ("AButton" + button).GetComponent<Button> ().interactable = false;
			} else {
				GameObject.Find ("AButton" + button).GetComponent<Button> ().interactable = true;
			}
		}
		if (playerList [0].sprite) {
			startButton.interactable = true;
		}
	}
}
