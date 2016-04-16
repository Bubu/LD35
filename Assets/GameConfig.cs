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
		new Color (0, 0, 1),
		new Color (0, 1, 0),
		new Color (0, 1, 1),
		new Color (1, 0, 0),
		new Color (1, 0, 1)
	});
}

