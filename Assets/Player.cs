using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	public Color c_chosen;
	public Button but;

	// Use this for initialization
	void Start () {
	}

	public void setActiveColor() {
		GameObject.Find ("active").GetComponent<Text> ().color = new Color(1,1,0);
	}


}
