using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {

	public VoterGrid voterGrid;
	public List<Player> playerList;
	public Player activePlayer;
	public List<District> districtList;
	public int activeDistrict;
	public Image activeColor;
	public int x;
	public double ratio;
	public GameSettings gs;
	public GameObject endPanel;
	public AI refrenceAI;

	public List<GameObject> voterDistrictPlayer0;
	public List<GameObject> voterDistrictPlayer1;

	// Start of actual game here.
	void Start () {
		System.Random rnd = new System.Random ();
		gs = GameObject.Find ("Initialize").GetComponent<GameSettings> ();
		playerList = gs.playerList;
		activePlayer = playerList[1];//rnd.Next(2)];


		for(int i = 0; i<2; i++){
			GameObject.Find ("Player" + i + "Image" + 0).GetComponent<Image> ().sprite = playerList[i].sprite;
			GameObject.Find ("Player" + i + "Image" + 1).GetComponent<Image> ().sprite = playerList[i].sprite;
			if(!playerList[i].isHuman && i == 0){
				playerList[i].ai = new GerryAI(this, playerList[i]);
			} else if(!playerList[i].isHuman)  {
				playerList[i].ai = new AI2(this, playerList[i]);
				this.refrenceAI = new GerryAI(this, playerList[i]);
			}
		}
		endPanel = GameObject.Find ("EndPanel");
		endPanel.SetActive (false);
		x = gs.x;
		ratio = gs.ratio;
		GameObject.Destroy(GameObject.Find ("Initialize"));
		voterGrid = new VoterGrid();
		voterGrid.initialize();
		districtList = new List<District> ();
		float size = GameResources.Instance.sprite_size * x;
		GameObject.Find("Main Camera").GetComponent<ZoomScript>().zoomTo(size);
		for (int index = 0; index < GameResources.Instance.numberOfDistricts; index++) {
			GameObject button = GameObject.Find ("DistrictButton" + index);
			GameObject textBox0 = GameObject.Find ("P" + 0 + "Score" + index);
			GameObject textBox1 = GameObject.Find ("P" + 1 + "Score" + index);
			button.SetActive (true);
			districtList.Add(new District(index, textBox0, textBox1, GameResources.Instance.colorList[index],this));
			button.GetComponent<Image>().color = GameResources.Instance.colorList [index];
		}
			
		activeDistrict = 0;
		activeColor = GameObject.Find ("ActiveColor").GetComponent<Image> ();
		activateDistrict(0);

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
		foreach(var voterIndex in districtList[activeDistrict].neighborSet){
			voterGrid.array[voterIndex.First,voterIndex.Second].bgobj.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
		}
		foreach(var voterIndex in voterGrid.freeVoterSet){
			voterGrid.array[voterIndex.First,voterIndex.Second].bgobj.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
		}
		activeDistrict = index;
		activeColor.color = districtList[activeDistrict].color;

		if (districtList[activeDistrict].neighborSet.Count == 0){
			foreach(var voterIndex in voterGrid.freeVoterSet){
				voterGrid.array[voterIndex.First,voterIndex.Second].bgobj.GetComponent<SpriteRenderer>().color = new Color(0.75f,0.75f,0.75f);
			}
		} else {			
			foreach(var voterIndex in districtList[activeDistrict].neighborSet){
				voterGrid.array[voterIndex.First,voterIndex.Second].bgobj.GetComponent<SpriteRenderer>().color = new Color(0.75f,0.75f,0.75f);
			}
		}
	}

	public void refreshPossibleMoves(){
		foreach(var voterIndex in voterGrid.freeVoterSet){
			voterGrid.array[voterIndex.First,voterIndex.Second].bgobj.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
		}
		foreach(var voterIndex in districtList[activeDistrict].neighborSet){
			voterGrid.array[voterIndex.First,voterIndex.Second].bgobj.GetComponent<SpriteRenderer>().color = new Color(0.75f,0.75f,0.75f);
		}
	}

	public void deactivateEndPanel(){
		endPanel.SetActive (false);
	}

	public void advanceTurn(){
		if (voterGrid.freeVoterSet.Count == 0) {
			endPanel.SetActive (true);
			if (playerList [0].districtSet.Count > playerList [1].districtSet.Count) {
				Debug.Log (playerList [0].districtSet.Count + "  " + playerList [1].districtSet.Count);
				GameObject.Find ("EndText").GetComponent<Text> ().text = "Player 1 wins!";
			} else if (playerList [0].districtSet.Count < playerList [1].districtSet.Count) {
				Debug.Log (playerList [0].districtSet.Count + "  " + playerList [1].districtSet.Count);
				GameObject.Find ("EndText").GetComponent<Text> ().text = "Player 2 wins!";
			} else {
				Debug.Log (playerList [0].districtSet.Count + "  " + playerList [1].districtSet.Count);
				GameObject.Find ("EndText").GetComponent<Text> ().text = "It's a draw!";
			}

		} else {
			activePlayer = playerList [(activePlayer.index + 1) % 2];
			if (!activePlayer.isHuman) {
				Move referenceMove = this.refrenceAI.findBestMove(this.districtList, this.voterGrid, activePlayer.index, 0);
				Debug.Log("Reference AI would chose: " + referenceMove);
				activePlayer.ai.doMove ();
			}
			refreshPossibleMoves ();
		}
	}

}
