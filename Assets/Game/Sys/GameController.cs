using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	
	public WorldMain world_main;
	public CameraMain cam;
	public MenuMain Menu;
	public GameObject node_selection_circle,camera_selection_circle;
	
	public ShipData SelectedShip{
		get{
			return selected_ship;
		}
		set{
			selected_ship=value;
		}
	}
	
	ShipData selected_ship;
	
	NodeMain selected_node=null;
	Timer left_double_timer;
	bool is_left_double=false;
	
	// Use this for initialization
	void Start () {
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
				if (selected_ship!=null){
					if (node.Data!=selected_ship.Orbit){
						if (selected_ship.MoveTarget!=null){
							selected_ship.setMovement(null);
							world_main.removeShip(selected_ship);
						}
						//move
						selected_ship.setMovement(node.Data);
						world_main.createShip(selected_ship);
					}else{
						//cancel move
						selected_ship.setMovement(null);
						world_main.removeShip(selected_ship);
					}
				Menu.UpdateShipPanel();
				}
				//selected_node.Link(node);
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
	}

	void SelectNode (NodeMain node)
	{
		//DeselectNode();
		selected_node=node;
		//set hud
		Menu.SetNodePanel(node);
		node_selection_circle.SetActive(true);
		node_selection_circle.transform.position=node.transform.position;
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
}
