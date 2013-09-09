using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldMain : MonoBehaviour {
	
	public GameObject ship_prefab;
	public WorldData Data{get;private set;}
	
	List<ShipMain> ships=new List<ShipMain>();
	// Use this for initialization
	void Start () {
		Data=new WorldData();
		Data.world_main=this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void NextTurn(){
		Data.Update();
		
		List<ShipData> remove_list=new List<ShipData>();
		
		foreach(var s in Data.Ships){
			if (s.AI&&s.Moving&&s.ShipObj()==null){
				createShip(s);	
			}
			if (s.StoppedMoving()){
				removeShip(s);
			}
			if (s.ColonizingPlanet){
				if (s.Orbit.HasColony()){
					//some one took it first. DEV. check for this in before hand. Conflict or diplomatic solution
					s.ColonizePlanet(false);
				}
				else{
					remove_list.Add(s);
					s.Orbit.SetColony(new ColonyData(Data,s.Faction,s.Orbit,false));
				}
			}
		}
		foreach (var s in remove_list){
			Data.RemoveShip(s);
		}
		
		foreach (var s in ships){
			s.UpdateTurn();
		}
	}
	
	public void createShip(ShipData s){
		var so=Instantiate(ship_prefab,s.Position,Quaternion.identity) as GameObject;
		var sm=so.GetComponent<ShipMain>();
		sm.SetData(s,this);
		ships.Add(sm);
	}
	public void removeShip(ShipData s){
		var sm=s.ShipObj();
		if (sm==null) return;
		ships.Remove(sm);
		s.ShipObj(null);
		Destroy(sm.gameObject);
	}

	public void UpdateHud()
	{		
		foreach (var n in Data.Nodes){
			n.Node.UpdateHud();
		}
	}
}
