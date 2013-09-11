using UnityEngine;
using System.Collections;

public class BuildItemData{
	
	public bool Ready{get;private set;}
	public bool CurrentPoints{get;private set;}
	public bool RequiredPoints{get;private set;}
	
	//DEV build item type
	
	public BuildItemData(int points){
		RequiredPoints=points;
		CurrentPoints=0;
	}
	/// <summary>
	/// DEV. return spent points.
	/// </param>
	public void Build(int points){
		CurrentPoints+=points;
		if (CurrentPoints>=RequiredPoints){
			Ready=true;	
		}
	}
}
