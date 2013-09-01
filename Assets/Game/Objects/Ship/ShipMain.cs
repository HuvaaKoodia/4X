using UnityEngine;
using System.Collections;

public class ShipMain : MonoBehaviour {
	
	public ShipData Data{get;private set;}
	public GameObject graphics; 
	public LineRenderer line_render;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update (){

	}
	
	public void SetData(ShipData data){
		Data=data;
		Data.ShipObj(this);
		
		UpdateTurn();
	}
	
	public void UpdateTurn(){
		line_render.SetPosition(0,Data.Position);
		line_render.SetPosition(1,Data.TargetPosition);
		
		transform.position=Data.Position;
		graphics.transform.LookAt(Data.TargetPosition);
		graphics.transform.rotation*=Quaternion.Euler(new Vector3(90,0,0));
	}
}
