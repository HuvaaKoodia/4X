using UnityEngine;
using System.Collections;

public class ColonyActionsPanel : MonoBehaviour {
	
	public ColonyData Colony{get;private set;}
	public MenuMain main_menu;
	public UILabel BuildShipLabel;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void BuildButton(){
		Colony.BuildShip();
		main_menu.UpdateHud();
	}

	public void setColony (ColonyData colony)
	{
		Colony=colony;
		BuildShipLabel.text="Build Ship (-"+Colony.Faction.ShipCost+"e)";
	}
	
	
}
