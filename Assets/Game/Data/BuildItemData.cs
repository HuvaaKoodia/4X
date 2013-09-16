using UnityEngine;
using System.Collections;

public class BuildItemData{
	
	public bool Ready{get;private set;}
	public int CurrentPoints{get;private set;}
	public int RequiredPoints{get;private set;}
	
	ColonyData _colony;
	
	//DEV build item type
	
	public int TurnsLeft{
		get{
			int left=(int)Mathf.Ceil((RequiredPoints-CurrentPoints)/(float)_colony.Industry);
			return left;
		}
	}
	
	public BuildItemData(ColonyData colony, int points){
		_colony=colony;
		RequiredPoints=points;
		CurrentPoints=0;
	}
	
	public int Build(int points){
		int max_p=Mathf.Min(points,RequiredPoints-CurrentPoints);
		CurrentPoints+=max_p;
		if (CurrentPoints==RequiredPoints){
			Ready=true;	
		}
		return max_p;
	}
}
