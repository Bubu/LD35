using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {

	public VoterGrid voterGrid;
	public List<Player> playerList;
	public Player activePlayer;

	// Use this for initialization
	void Start () {
		voterGrid = new VoterGrid();
		float size = voterGrid.getSize();
		GameObject.Find("Main Camera").GetComponent<ZoomScript>().setZoom();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
