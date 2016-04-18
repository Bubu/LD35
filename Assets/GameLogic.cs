using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {

	public VoterGrid voterGrid;
	public List<Player> playerList;
	public Player activePlayer;
	public List<District> districtList;
	public District activeDistrict;
	public Image activeColor;
	public int x;
	public double ratio;
	public GameSettings gs;

	public List<GameObject> voterDistrictPlayer0;
	public List<GameObject> voterDistrictPlayer1;

	// Start of actual game here.
	void Start () {
		System.Random rnd = new System.Random ();
		gs = GameObject.Find ("Initialize").GetComponent<GameSettings> ();
		playerList = gs.playerList;
		activePlayer = playerList[1];//rnd.Next(2)];
		for(int i = 0; i<2; i++){
			if(!playerList[i].isHuman){
				playerList[i].ai = new AI(this);
			}
		}
		x = gs.x;
		ratio = gs.ratio;
		GameObject.Destroy(GameObject.Find ("Initialize"));
		voterGrid = new VoterGrid();
		districtList = new List<District> ();
		float size = GameConfig.Instance.sprite_size * x;
		GameObject.Find("Main Camera").GetComponent<ZoomScript>().zoomTo(size);
		for (int index = 0; index < GameConfig.Instance.numberOfDistricts; index++) {
			GameObject button = GameObject.Find ("DistrictButton" + index);
			GameObject textBox = GameObject.Find ("Score" + index);
			button.SetActive (true);
			districtList.Add(new District(index, textBox, GameConfig.Instance.colorList[index],this));
			button.GetComponent<Image>().color = GameConfig.Instance.colorList [index];
		}
		activeDistrict = districtList[0];
		activeColor = GameObject.Find ("ActiveColor").GetComponent<Image> ();
		for (int i = 0; i < 5; i++) {
			voterDistrictPlayer0.Add (GameObject.Find ("1DistrictText"+i));
			voterDistrictPlayer0 [i].SetActive (false);
			voterDistrictPlayer1.Add (GameObject.Find ("2DistrictText"+i));
			voterDistrictPlayer1 [i].SetActive (false);
		}
		advanceTurn();
	}

	// Update is called once per frame
	void Update () {
	}

	public void activateDistrict(int index){
		foreach(var voter in activeDistrict.neighborSet){
			voter.bgobj.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
		}
		foreach(var voter in voterGrid.freeVoterSet){
			voter.bgobj.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
		}
		activeDistrict = districtList [index];
		activeColor.color = activeDistrict.color;

		if (activeDistrict.neighborSet.Count == 0){
			foreach(var voter in voterGrid.freeVoterSet){
				voter.bgobj.GetComponent<SpriteRenderer>().color = new Color(0.75f,0.75f,0.75f);
			}
		} else {			
			foreach(var voter in activeDistrict.neighborSet){
				voter.bgobj.GetComponent<SpriteRenderer>().color = new Color(0.75f,0.75f,0.75f);
			}
		}
	}

	public void refreshPossibleMoves(){
		foreach(var voter in voterGrid.freeVoterSet){
			voter.bgobj.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
		}
		foreach(var voter in activeDistrict.neighborSet){
			voter.bgobj.GetComponent<SpriteRenderer>().color = new Color(0.75f,0.75f,0.75f);
		}
	}

	public void advanceTurn(){
		activePlayer = playerList[(activePlayer.index + 1) % 2];
		if(!activePlayer.isHuman){
			activePlayer.ai.doMove();
		}
		refreshPossibleMoves();
	}

}
