using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameSettings : MonoBehaviour {

	public int x = 30;
	public int y = 30;
	public double rel = 0.5;
	public List<Player> playerList;
	public int curPlayerIndex;

	// Use this for initialization
	void Start () {
		playerList = new List<Player>();
		playerList.Add (new Player (0));
		playerList.Add (new Player (1));
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		

	public void setPlayer0Animal(int spriteIndex){
		playerList[0].sprite = GameConfig.Instance.spriteList[spriteIndex];
	}

	public void setPlayer1Animal(int spriteIndex){
		playerList[1].sprite = GameConfig.Instance.spriteList[spriteIndex];
	}
}
