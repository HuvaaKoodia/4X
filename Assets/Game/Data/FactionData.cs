using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactionData{
	public string Name{get;private set;}
	public bool AI{get;private set;}
	
	List<NodeData> known_nodes=new List<NodeData>();
	List<ColonyData> Colonies=new List<ColonyData>();
	
	public ColonyData CapitalColony;
	public Color Color_{get;private set;}
	
	public int ShipCost{get{return 10;}}//dev temp
	
	public FactionData(string name,bool ai){
		Name=name;
		AI=ai;
		
		Color_=Subs.RandomColor();
	}
	
	void addKnownNode(NodeData node){
		if (!hasKnownNode(node)){
			known_nodes.Add(node);
		}
	}
	
	bool hasKnownNode(NodeData node){
		return known_nodes.Contains(node);
	}

	public void AddColony (ColonyData pc)
	{
		if (CapitalColony==null) CapitalColony=pc;
		Colonies.Add(pc);
	}
	
	public void Update(){
		foreach (var c in Colonies){
			c.Update();
		}
	}
}
