using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipActionsPanel : MonoBehaviour {
	
	public ShipPanel ship_panel;
	public GameObject move_button,colonize_button;
	
	List<ShipData> _ships=new List<ShipData>();
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
	
	public void setShips(List<ShipData> selected_ships){
		_ships=selected_ships;
		//DEV. TEMP stuff
		
		var ship=_ships[0];
		
		colonize_button.SetActive(false);
		if (ship.Orbit.Colony==null){
			if (_ships.Count==1)
				colonize_button.SetActive(true);
		}
	}
		
	public void MovePressed(){
		ship_panel.MoveCommand();
	}
	
	public void ColonizePressed(){
		ship_panel.ColonizeCommand(_ships[0]);
	}
}
