using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeData{
	
	public string Name{get;private set;}
	public int Matter{get;set;}
	
	public NodeMain Node{get;private set;}
	public ColonyData Colony{get;private set;}
	
	public List<ShipData> Ships{get;private set;}
	
	public NodeData(string name){
		Name=name;
		Matter=1000;//size*100?
		
		Ships=new List<ShipData>();
	}
	
	/// <summary>
	/// Sets the colony if it's null.
	/// </param>
	public void SetColony(ColonyData data){
		if (Colony==null){
			Colony=data;
		}
	}
	public bool HasColony(){
		return Colony!=null;
	}

	public void setNode (NodeMain node)
	{
		Node=node;
	}
	
	public bool hasShips ()
	{
		return Ships.Count>0;
	}
}
