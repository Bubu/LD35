using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameSettings : MonoBehaviour {

	public int x = 10;
	public int y = 10;
	public double rel = 0.5;
	public List<Player> playerList;
	public int curPlayerIndex;

	// Use this for initialization
	void Start () {
		playerList = new List<Player>();
		playerList.Add (new Player (0));
		playerList.Add (new Player (1));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setCurPlayerIndex(int index){
		//curPlayerIndex = 
	}

	public void setPlayerAnimal(int[] list){
		playerList[list[0]].sprite = GameConfig.Instance.spriteList[list[1]];
	}
}
