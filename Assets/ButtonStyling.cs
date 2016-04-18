using UnityEngine;  
using System.Collections;  
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class ButtonStyling : MonoBehaviour {
	public Text theText;
	// Use this for initialization
	void Start () {
		theText = GetComponentInChildren<Text>();
	}

	void Update(){
		if(!GetComponent<Button>().interactable){
			theText.color = Color.gray;
		}
		else{
			theText.color = Color.black;
		}
	}
}
