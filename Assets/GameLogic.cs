using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	public Pattern m_pattern;

	// Use this for initialization
	void Start () {
		m_pattern = new Pattern();
		float size = m_pattern.getSize();
		GameObject.Find("Main Camera").GetComponent<ZoomScript>().setZoom();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
