using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldData{
	
	public List<NodeData> Nodes=new List<NodeData>();
	List<FactionData> Factions=new List<FactionData>();
	public List<ShipData> Ships{get;private set;}
	
	public FactionData player_faction;
	
	public int Turn{get;private set;}
	
	// Use this for initialization
	public WorldData(){
		Turn=1;
		
		Ships=new List<ShipData>();
		
		//find and populate nodes
		var world=GameObject.FindGameObjectWithTag("World");
		
		int i=0;
		
		foreach(Transform t in world.transform){
			var Node=t.gameObject.GetComponent<NodeMain>();
			
			var data=new NodeData("Node "+i);
			Node.setData(data);
			data.setNode(Node);
			i++;
			Nodes.Add(data);
		}
		//factions
		var pf=new FactionData("Player Faction",false);
		Factions.Add(pf);
		
		//create colonies
		var pn=Nodes[Subs.GetRandom(Nodes.Count)];
		var pc=new ColonyData(this,pf,pn,true);
		
		pn.SetColony(pc);
		
		player_faction=pf;
	}

	public void Update() {
		Turn++;
		foreach(var f in Factions){
			f.Update();
		}
		
		foreach (var s in Ships){
			s.Update();
		}
	}
	
	public void createShip(ShipData ship){
		Ships.Add(ship);
	}

	public void RemoveShip (ShipData s)
	{
		Ships.Remove(s);
		if (s.Orbit!=null){
			s.Orbit.Ships.Remove(s);
		}
	}
	
}
