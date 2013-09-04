using UnityEngine;
using System.Collections;

public class ShipPanel : MonoBehaviour {
	
	public MenuMain Menu;
	public GameObject Contents,ShipItem_prefab;
	public ShipActionsPanel ship_actions_panel;
	public UIDraggablePanel drag_panel;
	
	NodeData _node;
	
	// Use this for initialization
	void Start () {
		ship_actions_panel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setNode (NodeData node)
	{
		_node=node;
		UpdateHud();
	}
	
	public void UpdateHud(){
		if (!gameObject.activeSelf) return;
		
		if (Menu.game_controller.SelectedShip==null)
			ship_actions_panel.gameObject.SetActive(false);
		
		int c=Contents.transform.childCount;
		for(int i=0;i<c;i++){
			Transform t = Contents.transform.GetChild(0);
			DestroyImmediate(t.gameObject);
		}
		
		foreach(var s in _node.Ships){
			if (s.Faction.AI) continue;
			var si=Instantiate(ShipItem_prefab,Vector3.zero,Quaternion.identity) as GameObject;
			var sid=si.GetComponent<ShipItem>();
			sid.ship_panel=this;
			sid.setShip(s);
			
			si.transform.parent=Contents.transform;
			si.transform.localPosition=new Vector3(0,0,-1);
			si.transform.localScale=Vector3.one;
		}
		Contents.GetComponent<UIGrid>().Reposition();
		
		if (_node.Ships.Count==0)
			drag_panel.ResetPosition();
	}
	
	public void MoveShip(ShipData ship){
		Menu.game_controller.SelectedShip=ship;
	}
	public void ColonizePlanet(ShipData ship){
		ship.ColonizePlanet();
		Menu.UpdateShipPanel();
	}

	public void SetSelectedShip (ShipData ship)
	{
		ship_actions_panel.gameObject.SetActive(true);
		ship_actions_panel.setShip(ship);
	}
	
	public void OnEnable(){
		
	}
}
