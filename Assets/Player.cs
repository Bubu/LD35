using UnityEngine;
using System.Collections;

public class Player {

	public int index;
	public Sprite sprite;
	public bool isHuman;
	public AI ai;

	public Player(int index) {
		this.index = index;
		this.isHuman = true;
	}
}
