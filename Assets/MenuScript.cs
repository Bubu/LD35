using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape))
		{
			if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("menu")){
				quitGame();
			}
			else{
				showMenu();
			}
		}
	}

	public void startGame() {
		SceneManager.LoadScene("game");
	}
	public void quitGame() {
		Application.Quit();
	}
	public void showMenu() {
		SceneManager.LoadScene("menu");
	}
	public void showInstructions() {
		SceneManager.LoadScene("instructions");
	}

}
