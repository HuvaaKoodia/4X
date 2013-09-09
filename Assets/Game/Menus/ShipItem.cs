using UnityEngine;
using System.Collections;

public class ShipItem : MonoBehaviour {
	
	public UILabel name_label,action_label;
	public UISprite background_spr;
	public ShipPanel ship_panel;
	public UIButton This;
	
	ShipData _ship;
	
	public ShipData Ship{get{return _ship;}}
	
	bool selected=false;
	
	public bool Selected{get{return selected;}
		set{
			selected=value;
			if (value){
				This.defaultColor=This.hover=background_spr.color=Color.red;
				This.UpdateColor(true,true);
			}
			else{
				This.defaultColor=This.hover=background_spr.color=Color.white;
				This.UpdateColor(true,true);
			}
	}}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setShip(ShipData ship){
		_ship=ship;
		name_label.text=ship.Name;
		string action="";
		if (ship.MovingOut()){
			action="Moving";
		}
		if (ship.ColonizingPlanet){
			action="Colonizing";
		}
		action_label.text=action;
	}
	
	void OnClick(){
		Selected=!selected;
		if (!selected){
			ship_panel.RemoveSelectedShip(_ship);
		}
		else{
			ship_panel.AddSelectedShip(_ship);
		}
	}
}
