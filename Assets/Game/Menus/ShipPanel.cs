using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum QueueType{Ships,Builds};

public class ShipPanel : MonoBehaviour {
	
	public MenuMain Menu;
	public GameObject DataPanel,Contents,TabButtons,ShipItem_prefab,BuildItem_prefab;
	public ShipActionsPanel ship_actions_panel;
	public UIDraggablePanel drag_panel;
	
	NodeData _node;
	List<ShipItem> items=new List<ShipItem>();
	List<BuildItem> b_items=new List<BuildItem>();
	
	QueueType type=QueueType.Ships;
	
	// Use this for initialization
	void Start () {
		ship_actions_panel.gameObject.SetActive(false);
	}
	
	public void setNode (NodeData node)
	{
		if (node==null){
			_node=null;
			TabButtons.SetActive(false);
			DataPanel.SetActive(false);
			return;
		}
		TabButtons.SetActive(true);
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
		if (type==QueueType.Ships&&_node.hasShips()){
			
			foreach(var s in _node.Ships){
				if (s.Faction.AI) continue;
				var si=Instantiate(ShipItem_prefab,Vector3.zero,Quaternion.identity) as GameObject;
				var sid=si.GetComponent<ShipItem>();
				sid.ship_panel=this;
				sid.setShip(s);
				
				si.transform.parent=Contents.transform;
				si.transform.localPosition=new Vector3(0,0,-1);
				si.transform.localScale=Vector3.one;
				
				
				if (Menu.game_controller.IsSelected(s)){
					sid.Selected=true;
					selected_a++;
				}
				items.Add(sid);
				a++;
			}
			
			if (selected_a!=selected_amount){
				UpdateShipActions();
				selected_amount=selected_a;
			}
		}
		else if (type==QueueType.Builds&&_node.hasBuilds()){
			if (_node.HasColony()&&!_node.Colony.Faction.AI){
				foreach(var b in _node.Colony.BuildItems){
					var go=Instantiate(BuildItem_prefab,Vector3.zero,Quaternion.identity) as GameObject;
					var bi=go.GetComponent<BuildItem>();
					bi.ship_panel=this;
					bi.setBuildItem(b);
					
					go.transform.parent=Contents.transform;
					go.transform.localPosition=new Vector3(0,0,-1);
					go.transform.localScale=Vector3.one;
					
					//if (Menu.game_controller.IsSelected(b)){
					//	bi.Selected=true;
					//	selected_a++;
					//}
					b_items.Add(bi);
					a++;
				}
			}
		}
		else{
			drag_panel.ResetPosition();
			DataPanel.SetActive(false);
		}
		
		Contents.GetComponent<UIGrid>().Reposition();
		
		if (a>0){
			DataPanel.SetActive(true);
			return;
		}
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
	
	public void SetState(QueueType type){
		this.type=type;
		
		UpdateHud();
	}
	
	public void BuildQueueButton(){
		SetState(QueueType.Builds);
	}
	
	public void ShipQueueButton(){
		SetState(QueueType.Ships);
	}

	public void BuildPressed ()
	{
		if (!DataPanel.activeSelf){
			SetState(QueueType.Builds);
		}
	}
}
