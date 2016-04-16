using UnityEngine;
using System.Collections;

public class GetMoBuUp : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0))
			Debug.Log ("left click");
	}
}
