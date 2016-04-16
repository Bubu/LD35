using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class District {

	public string districtName;
	public Color color;
	public List<Voter> voterList;

	public District(string name, Color color){
		this.color = color;
		this.districtName = name;
		this.voterList = new List<Voter>();
		
	}
}
