using UnityEngine;
using System.Collections;

public class ShipData{
	
	public static int ship_number;//DEV.temp
	
	public string Name{get;private set;}
	public FactionData Faction{get;private set;}
	
	ShipMain ship_obj;
	
	public bool AI{get{return Faction.AI;}}
	
	public ShipData(NodeData orbit,FactionData f){
		ship_number++;
		Name="Ship "+ship_number;
		on_orbit=orbit;
		Faction=f;
		f.Ships.Add(this);
	}
	
	public void Update(){
		//fuel?
		if (Moving){
			LeaveOrbit();
			position+=direction*speed;
			travel_length-=speed;
			if (travel_length<=0){
				Moving=false;
				EnterOrbit(target);
			}
		}
	}
	//logic DEV.reloc.
	
	public Vector3 Position{
		get{
			if (Moving){
				return position;
			}
			return on_orbit.Node.transform.position;
		}
	}
	public Vector3 TargetPosition{
		get;private set;
	}
	
	public NodeData MoveTarget{get{return target;}}
	
	public Vector3 Direction{
		get{
			return direction;
		}
	}
	
	bool moving_out=false,moving_stop=false;
	
	public bool MovingOut(){
		return moving_out;
	}
	public bool Moving{get;private set;}
	
	public bool StoppedMoving(){
		if (moving_stop){
			moving_stop=false;
			return true;
		}
		return false;
	}
	
	NodeData on_orbit,origin,target;
	float speed=1.34f,travel_length;
	Vector3 position,direction;
	
	public void setMovement(NodeData target){
		this.target=target;
		if (target==null){
			//cancel move
			Moving=moving_out=false;
			return;
		}
		origin=on_orbit;
		
		Moving=moving_out=true;
		
		Vector3 pos=origin.Node.transform.position,tpos=target.Node.transform.position;
		var dock_dis=1.5f;
		
		var dir_vec=origin.Node.transform.TransformDirection(tpos-pos).normalized;
		
		position=pos+dir_vec*dock_dis;
		TargetPosition=tpos-dir_vec*dock_dis;
		
		direction=TargetPosition-position;
		travel_length=direction.magnitude;
		direction=direction.normalized;
	}

	public void LeaveOrbit ()
	{
		if (on_orbit==null) return;
		moving_out=false;
		on_orbit.Ships.Remove(this);
		on_orbit=null;
	}
	
	public void EnterOrbit(NodeData node){
		on_orbit=node;
		on_orbit.Ships.Add(this);
		moving_stop=true;
		
		if (AI){
			//Faction.
		}
	}
	
	public NodeData Orbit{get{return on_orbit;}}
		
	public ShipMain ShipObj(){
		return ship_obj;
	}
	public void ShipObj(ShipMain ship){
		ship_obj=ship;
	}
	
	//colonize
	
	public bool ColonizingPlanet{get{return colonize_next_turn;}}
	
	bool colonize_next_turn=false;
	
	public void ColonizePlanet()
	{
		colonize_next_turn=true;
	}

	public void Destroy ()
	{
		if (Orbit!=null){
			Orbit.Ships.Remove(this);
		}
		Faction.Ships.Remove(this);
	}
}
