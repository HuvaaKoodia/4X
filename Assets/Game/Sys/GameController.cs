using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	
	public WorldMain world_main;
	public CameraMain cam;
	public MenuMain Menu;
	public GameObject node_selection_circle,camera_selection_circle;
	
	List<ShipData> selected_ships;
	
	NodeMain selected_node=null;
	Timer left_double_timer;
	bool is_left_double=false;
	
	// Use this for initialization
	void Start () {
		selected_ships=new List<ShipData>();
		
		left_double_timer=new Timer(250,OnLeftDoubleClick);
		
		var n=world_main.Data.player_faction.CapitalColony.Node.Node;
		setCameraTarget(n);
		SelectNode(n);
		
		Menu.UpdateHud();
		
		
	}

	// Update is called once per frame
	void Update () {
		left_double_timer.Update();
		
		if (InputHandler.GetMouseClicked(0)){
			var node=RaycastNode();
			
			if (node!=null){
				if (selected_node==node){
					if (cam.Target==node.transform){
						if (is_left_double){
							cam.toggleDistance();
						}
					}
					else{
						setCameraTarget(node);
					}
				}
				else{
					SelectNode(node);
				}
			}
			else{
				DeselectNode();
			}
			
			left_double_timer.Active=true;
			left_double_timer.Reset();
			is_left_double=true;
		}
		
		if (InputHandler.GetMouseClicked(1)){
			var node=RaycastNode();
			
			if (node!=null){
				bool update_hud=false;
				foreach(var s in selected_ships){
					if (node.Data!=s.Orbit){
						if (s.MoveTarget!=null){
							s.setMovement(null);
							world_main.removeShip(s);
						}
						//move
						s.setMovement(node.Data);
						world_main.createShip(s);
					}else{
						//cancel move
						s.setMovement(null);
						world_main.removeShip(s);
					}
					update_hud=true;
				}
				
				if (update_hud){
					Menu.UpdateShipPanel();
				}
			}
		}
	}
	
	NodeMain RaycastNode(){
		int mask=1<<LayerMask.NameToLayer("Node");
		RaycastHit info;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out info,500f,mask))
			return info.collider.gameObject.GetComponent<NodeMain>();
		return null;
	}

	void DeselectNode ()
	{
		if (selected_node==null) return;
		selected_node=null;
		node_selection_circle.SetActive(false);
		Menu.SetNodePanel(null);
		ClearSelectedShips();
	}

	void SelectNode (NodeMain node)
	{
		if (selected_node==node) return;
		//DeselectNode();
		selected_node=node;
		//set hud
		Menu.SetNodePanel(node);
		node_selection_circle.SetActive(true);
		node_selection_circle.transform.position=node.transform.position;
		
		ClearSelectedShips();
	}

	void setCameraTarget (NodeMain node)
	{
		cam.SetTarget(node.transform);
		camera_selection_circle.SetActive(true);
		camera_selection_circle.transform.position=node.transform.position;
	}
	
	public void NextTurn(){}
	
	//events
	void OnLeftDoubleClick(){
		left_double_timer.Active=false;
		left_double_timer.Reset();
		is_left_double=false;
	}

	public bool HasSelectedShips ()
	{
		return selected_ships.Count>0;
	}

	public void ClearSelectedShips ()
	{
		selected_ships.Clear();
	}

	public void AddSelectedShip (ShipData ship)
	{
		if (!selected_ships.Contains(ship)){
			selected_ships.Add(ship);
			Menu.UpdateShipPanel();
		}
	}

	public void RemoveSelectedShip (ShipData ship)
	{
		selected_ships.Remove(ship);
		Menu.UpdateShipPanel();
	}

	public bool IsSelected (ShipData s)
	{
		return selected_ships.Contains(s);
	}

	public void SetSelectedShip (ShipData ship)
	{
		ClearSelectedShips();
		AddSelectedShip(ship);
	}
}
