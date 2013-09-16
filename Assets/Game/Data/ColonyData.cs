using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColonyData{
	
	WorldData world;
	
	public NodeData Node{get;private set;}
	public FactionData Faction{get;private set;}
	
	public List<BuildItemData> BuildItems{get;private set;}
	
	public int Energy{
		get{return energy;}
		set{energy=Mathf.Clamp(value,0,energy_max);}
	}
	public int EnergyMax{
		get{return energy_max;}
		private set{energy_max=value;}
	}
	
	public int Industry{
		get{return industry;}
		set{industry=Mathf.Clamp(value,0,industry_max);}
	}
	public int IndustryMax{
		get{return industry_max;}
		private set{industry_max=value;}
	}
	
	//public List<ShipData> Ships{get;private set;}
	int energy=0,energy_max=20,energy_recharge=4,energy_consumption=2;
	int industry=0,industry_max=40,industry_recharge=2;
	
	public ColonyData(WorldData w,FactionData f,NodeData node,bool first_colony){
		Node=node;
		setFaction(f);
		
		world=w;
		BuildItems=new List<BuildItemData>();
		
		//Ships=new List<ShipData>();
		if (first_colony){
			Energy=20;
			Industry=industry_max;
		}
	}
	
	public void setFaction(FactionData faction){
		Faction=faction;
		faction.AddColony(this);
		Node.Node.ChangeColor(Faction.Color_);
	}
	public void Update(){
		//colony maintenance
		Energy-=energy_consumption;
		Node.Matter-=energy_consumption;
		
		//building industry 
		if (industry<industry_max){
			var dif=industry_max-industry;
			dif=Mathf.Min(dif,industry_recharge);
			dif=Mathf.Min(dif,energy);
			Industry+=dif;
			Node.Matter-=dif;
			Energy-=dif;
		}
		
		
		
		//production
		if (BuildItems.Count>0){
			var spent=BuildItems[0].Build(Industry);
			Node.Matter-=spent;
			
			if (BuildItems[0].Ready){
				CreateShip();
				BuildItems.RemoveAt(0);
			}
		}
		
		//energy generation
		Energy+=energy_recharge;
	}

	public void BuildShip ()
	{
		BuildItems.Add(new BuildItemData(this,100));
	}
	
	public void CreateShip(){
		var s=new ShipData(Node,Faction);
		Node.Ships.Add(s);
		world.createShip(s);
	}

	/*
	public bool hasShips ()
	{
		return Ships.Count>0;
	}*/
}
