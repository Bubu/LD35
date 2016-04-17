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
	public int x;
	public double ratio;
	public GameSettings gs;

	//new!!!
	public List<GameObject> voterDistrictPlayer1;
	public List<GameObject> voterDistrictPlayer2;

	// Use this for initialization
	void Start () {
		gs = GameObject.Find ("Initialize").GetComponent<GameSettings> ();
		playerList = gs.playerList;
		x = gs.x;
		ratio = gs.ratio;
		voterGrid = new VoterGrid();
		districtList = new List<District> ();
		float size = GameConfig.Instance.sprite_size * x;
		GameObject.Find("Main Camera").GetComponent<ZoomScript>().zoomTo(size);
		for (int index = 0; index < GameConfig.Instance.numberOfDistricts; index++) {
			GameObject button = GameObject.Find ("DistrictButton" + index);
			GameObject textBox = GameObject.Find ("Score" + index);
			button.SetActive (true);
			districtList.Add(new District("District" + index, textBox, GameConfig.Instance.colorList[index]));
			button.GetComponent<Image>().color = GameConfig.Instance.colorList [index];
		}
			
		//new!!!
		for (int i = 0; i < 5; i++) {
			voterDistrictPlayer1.Add (GameObject.Find ("1DistrictText"+i));
			voterDistrictPlayer1 [i].SetActive (false);
			voterDistrictPlayer2.Add (GameObject.Find ("2DistrictText"+i));
			voterDistrictPlayer2 [i].SetActive (false);
		}

	}



	// Update is called once per frame
	void Update () {
	}

	public void activateDistrict(int index){
		activeDistrict = districtList [index];
		GameObject.Find ("ActiveColor").GetComponent<Image> ().color = activeDistrict.color;
	}

}
