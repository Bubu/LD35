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



	// Use this for initialization
	void Start () {
		playerList = GameObject.Find("Initialize").GetComponent<GameSettings>().playerList;
		voterGrid = new VoterGrid();
		districtList = new List<District> ();
		float size = voterGrid.getSize();
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
