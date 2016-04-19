using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

	public int index;
	public Sprite sprite;
	public bool isHuman;
	public AI ai;
	public HashSet<District> districtSet;

	public Player(int index) {
		this.index = index;
		this.isHuman = true;
		this.districtSet = new HashSet<District>();
	}
}
