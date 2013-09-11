using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipPanel : MonoBehaviour {
	
	public MenuMain Menu;
	public GameObject Contents,ShipItem_prefab;
	public ShipActionsPanel ship_actions_panel;
	public UIDraggablePanel drag_panel;
	
	NodeData _node;
	List<ShipItem> items=new List<ShipItem>();
	
	// Use this for initialization
	void Start () {
		ship_actions_panel.gameObject.SetActive(false);
	}
	
	public void setNode (NodeData node)
	{
		_node=node;
		UpdateHud();
	}
	
	public void UpdateHud(){
		if (!gameObject.activeSelf) return;
		
		int c=Contents.transform.childCount;
		items.Clear();
		for(int i=0;i<c;i++){
			Transform t = Contents.transform.GetChild(0);
			DestroyImmediate(t.gameObject);
		}
		
		int a=0;
		int selected_a=0;
		foreach(var s in _node.Ships){
			if (s.Faction.AI) continue;
			var si=Instantiate(ShipItem_prefab,Vector3.zero,Quaternion.identity) as GameObject;
			var sid=si.GetComponent<ShipItem>();
			sid.ship_panel=this;
			sid.setShip(s);
			
			si.transform.parent=Contents.transform;
			si.transform.localPosition=new Vector3(0,0,-1);
			si.transform.localScale=Vector3.one;
			a++;
			
			if (Menu.game_controller.IsSelected(s)){
				sid.Selected=true;
				selected_a++;
			}
			items.Add(sid);
		}
		
		Contents.GetComponent<UIGrid>().Reposition();
		
		if (a==0){
			gameObject.SetActive(false);
			return;
		}
		
		if (selected_a!=selected_amount){
			UpdateShipActions();
			selected_amount=selected_a;
		}
		
		if (_node.Ships.Count==0)
			drag_panel.ResetPosition();
	}
	
	public void UpdateShipActions(){
		List<ShipData> sels=new List<ShipData>();
		
		foreach(var s in _node.Ships){
			if (s.Faction.AI) continue;
	
			if (Menu.game_controller.IsSelected(s)){
				sels.Add(s);
			}
		}
		
		if (sels.Count>0){
			ship_actions_panel.gameObject.SetActive(true);
			ship_actions_panel.setShips(sels);
		}
		else{
			ship_actions_panel.gameObject.SetActive(false);
		}
	}
	
	public void MoveCommand(){
		//DEV. turn on move mode in game controller.
	}
	
	public void ColonizeCommand(ShipData ship){
		bool current=ship.ColonizingPlanet;
		
		foreach (var s in _node.Ships){
			if (!s.Faction.AI){
				s.ColonizePlanet(false);
			}
		}

		ship.ColonizePlanet(!current);
		UpdateHud();
	}
	
	int selected_amount=0;
	
	public void AddSelectedShip (ShipItem item)
	{
		if (InputHandler.GetControl()||InputHandler.GetShift()){
			Menu.game_controller.AddSelectedShip(item.Ship);
		}
		else{
			Menu.game_controller.SetSelectedShip(item.Ship);
			
			foreach (var i in items){
				if (i.Ship!=item.Ship)
					i.Selected=false;
			}
		}
		UpdateShipActions();
	}

	public void RemoveSelectedShip (ShipItem item)
	{
		if (InputHandler.GetControl()||InputHandler.GetShift()){
			Menu.game_controller.RemoveSelectedShip(item.Ship);
		}
		else{
			Menu.game_controller.SetSelectedShip(item.Ship);
			item.Selected=true;
			
			foreach (var i in items){
				if (i.Ship!=item.Ship)
					i.Selected=false;
			}
		}
		UpdateShipActions();
	}

	public void RemoveSelectedBuildItem (BuildItem item)
	{
		throw new System.NotImplementedException ();
	}

	public void AddSelectedBuildItem (BuildItem item)
	{
		throw new System.NotImplementedException ();
	}
}
