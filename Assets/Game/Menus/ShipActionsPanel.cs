using UnityEngine;
using System.Collections;

public class ShipActionsPanel : MonoBehaviour {
	
	public ShipPanel ship_panel;
	public GameObject move_button,colonize_button;
	
	
	ShipData _ship;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setShip(ShipData ship){
		_ship=ship;
		
		if (ship.Orbit.Colony==null){
			colonize_button.SetActive(true);
		}
		else{
			colonize_button.SetActive(false);
		}
	}
		
	public void MovePressed(){
		ship_panel.MoveShip(_ship);
	}
	
	public void ColonizePressed(){
		ship_panel.ColonizePlanet(_ship);
	}
}
