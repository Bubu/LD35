﻿using UnityEngine;
using System.Collections.Generic;
using System;

public class GameResources
{
	public static GameResources instance;
	private GameResources (){	
		spriteList = new List<Sprite> (new Sprite [] {
			loadSprite ("sheep"),
			loadSprite ("wolf"),
			loadSprite ("bunny"),
			loadSprite ("lion"),
			loadSprite ("elephant"),
			loadSprite ("donkey"),
});
bgSprite = loadSprite("grid");
}
	public static GameResources Instance {
		get {
			if (instance == null) {
				instance = new GameResources ();
			}
			return instance;
		}
	}

	public int numberOfDistricts = 5;
	public List<Color> colorList = new List<Color> (new Color [] {
		new Color (47f/255f,86f/255f,131f/255f),
		new Color (119f/255f,143f/255f,168f/255f),
		new Color (201f/255f,24f/255f,24f/255f),
		new Color (207f/255f,153f/255f,153f/255f),
		new Color (1f,225f/255f,162f/255f),
		new Color (0.918f,0.616f,0.302f),
		new Color (0.204f,0.482f,0.573f)
	});

	public List<String> districtNameList = new List<String> (new String [] {
		"Catsburgh",
		"Wulfcastle",
		"Buffalo Beach",
		"Lionsden",
		"Carrotterdam"
	});

	public Sprite loadSprite(String spriteName){
		Texture2D tex = Resources.Load<Texture2D>(spriteName);
		Sprite sprite = Sprite.Create(tex,new Rect(0,0,sprite_size,sprite_size),new Vector2(0,0),sprite_size);
		return sprite;
	}

	public List<Sprite> spriteList;
	public Sprite bgSprite;
	public int sprite_size =256;

}

