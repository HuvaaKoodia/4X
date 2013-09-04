using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactionData{
	public string Name{get;private set;}
	public bool AI{get;private set;}
	
	List<NodeData> known_nodes=new List<NodeData>();
	public List<ColonyData> Colonies{get;private set;}
	public List<ShipData> Ships{get;private set;}
	
	public ColonyData CapitalColony;
	public Color Color_{get;private set;}
	
	public int ShipCost{get{return 10;}}//dev temp
	
	FactionAI AI_c;
	
	public FactionData(string name,bool ai,WorldData world){
		Name=name;
		AI=ai;
		
		Colonies=new List<ColonyData>();
		Ships=new List<ShipData>();
		
		if (ai)
			AI_c=new FactionAI(this,world);
		
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
	
	public void AddShip(){
		
	}
	
	public void RemoveShip(ShipData ship){
		Ships.Remove(ship);
	}
	
	public void Update(){
		foreach (var c in Colonies){
			c.Update();
		}
		
		if (AI){
			AI_c.Update();
		}
	}
}
