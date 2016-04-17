using UnityEngine;
using System.Collections.Generic;
using System;

public class GameConfig
{
	public static GameConfig instance;
	private GameConfig (){	}
	public static GameConfig Instance {
		get {
			if (instance == null) {
				instance = new GameConfig ();
			}
			return instance;
		}
	}

	public int numberOfDistricts = 5;
	public List<Color> colorList = new List<Color> (new Color [] {
		new Color (0.886f,0.29f,0.349f),
		new Color (0.918f,0.745f,0.302f),
		new Color (0.263f,0.306f,0.631f),
		new Color (0.341f,0.765f,0.251f),
		new Color (0.431f,0.235f,0.62f),
		new Color (0.918f,0.616f,0.302f),
		new Color (0.204f,0.482f,0.573f)
	});
}

