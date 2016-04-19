using UnityEngine;  
using System.Collections;  
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class ModeButton : MonoBehaviour {

	GameSettings gs;
	// Use this for initialization
	void Start () {
		gs = GameObject.Find("Initialize").GetComponent<GameSettings>();
	}

	void Update(){
		if(gs.mode == 0){
			theText.color = Color.gray;
		}
		else{
			theText.color = Color.black;
		}
	}
}
