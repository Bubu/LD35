using System;


public class Move
{

	public District district;
	public Tuple<int,int> voterIndex;
	public float score;

	public Move (District district, Tuple<int,int> voterIndex, float score)
	{
		this.district = district;
		this.voterIndex = voterIndex;
		this.score = score;
	}
}


