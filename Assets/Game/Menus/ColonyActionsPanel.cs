using UnityEngine;
using System.Collections;

public class ColonyActionsPanel : MonoBehaviour {
	
	public ColonyData Colony{get;private set;}
	public MenuMain main_menu;
	public UILabel BuildShipLabel;
	public ShipPanel Ship_panel;
	
	void BuildButton(){
		Colony.BuildShip();
		Ship_panel.BuildPressed();
		main_menu.UpdateHud();
	}

	public void setColony (ColonyData colony)
	{
		Colony=colony;
		BuildShipLabel.text="Build Ship (-"+Colony.Faction.ShipCost+"e)";
	}
	
	
}
