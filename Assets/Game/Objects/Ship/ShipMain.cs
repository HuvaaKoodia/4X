using UnityEngine;
using System.Collections;

public class ShipMain : MonoBehaviour {
	
	public ShipData Data{get;private set;}
	public GameObject graphics; 
	public LineRenderer line_render;
	
	WorldMain world_main;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update (){

	}
	
	public void SetData(ShipData data,WorldMain world){
		Data=data;
		Data.ShipObj(this);
		
		world_main=world;
		
		UpdateTurn();
	}
	
	public void UpdateTurn(){
		line_render.SetPosition(0,Data.Position);
		line_render.SetPosition(1,Data.TargetPosition);
		
		transform.position=Data.Position;
		graphics.transform.LookAt(Data.TargetPosition);
		graphics.transform.rotation*=Quaternion.Euler(new Vector3(90,0,0));
	}

	public void RemoveFromWorld ()
	{
		world_main.removeShip(Data);
	}
}
