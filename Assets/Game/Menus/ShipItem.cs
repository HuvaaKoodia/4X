using UnityEngine;
using System.Collections;

public class ShipItem : MonoBehaviour {
	
	public UILabel name_label,action_label;
	public ShipPanel ship_panel;
	
	ShipData _ship;
	
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
		ship_panel.SetSelectedShip(_ship);
	}
}
