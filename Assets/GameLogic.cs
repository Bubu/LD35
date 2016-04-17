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
	public GameSettings gs;

	// Use this for initialization
	void Start () {
		gs = GameObject.Find ("Initialize").GetComponent<GameSettings> ();
		playerList = gs.playerList;
		x = gs.x;
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
	}



	// Update is called once per frame
	void Update () {
	}

	public void activateDistrict(int index){
		activeDistrict = districtList [index];
		GameObject.Find ("ActiveColor").GetComponent<Image> ().color = activeDistrict.color;
	}

}
