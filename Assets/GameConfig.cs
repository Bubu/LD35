using UnityEngine;
using System.Collections.Generic;
using System;

public class GameConfig
{
	public static GameConfig instance;
	private GameConfig (){	
		spriteList = new List<Sprite> (new Sprite [] {
			loadSprites ("elephant"),
			loadSprites ("bunny"),
			loadSprites ("wolf"),
			loadSprites ("sheep"),
			loadSprites ("lion")
		});}
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
		new Color (47f/255f,86f/255f,131f/255f),
		new Color (119f/255f,143f/255f,168f/255f),
		new Color (201f/255f,24f/255f,24f/255f),
		new Color (207f/255f,153f/255f,153f/255f),
		new Color (1f,225f/255f,162f/255f),
		new Color (0.918f,0.616f,0.302f),
		new Color (0.204f,0.482f,0.573f)
	});

	public Sprite loadSprites(String spriteName){
		Texture2D tex = Resources.Load<Texture2D>(spriteName);
		int sprite_size = tex.width;
		Sprite sprite = Sprite.Create(tex,new Rect(0,0,sprite_size,sprite_size),new Vector2(0,0),sprite_size);
		return sprite;
	}

	public List<Sprite> spriteList;


}

