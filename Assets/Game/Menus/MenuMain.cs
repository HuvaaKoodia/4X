using UnityEngine;
using System.Collections;

public class MenuMain : MonoBehaviour {
	
	public GameController game_controller;
	public WorldMain world;
	public NodePanelMain node_panel;
	public ColonyActionsPanel colony_actions;
	public ShipPanel ship_panel;
	public UILabel turn_label;
	
	NodeMain _node;
	
	// Use this for initialization
	void Start () {
		//SetNodePanel(null);
		turn_label.text="Turn 1";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetNodePanel(NodeMain node){
		_node=node;
		if (node==null){
			node_panel.gameObject.SetActive(false);
			colony_actions.gameObject.SetActive(false);
			ship_panel.gameObject.SetActive(false);
		}
		else{
			node_panel.gameObject.SetActive(true);
			node_panel.SetNode(node);
			
			if (node.Data.hasShips()){
				ship_panel.gameObject.SetActive(true);
				ship_panel.setNode(node.Data);
			}
			else{
				ship_panel.gameObject.SetActive(false);
			}
			
			if (node.Data.HasColony()){
				colony_actions.gameObject.SetActive(true);
				colony_actions.setColony(node.Data.Colony);
			}
			else{
				colony_actions.gameObject.SetActive(false);
			}
		}
	}
	
	public void UpdateHud(){
		SetNodePanel(_node);
		ship_panel.UpdateHud();
		node_panel.UpdateHud();
		world.UpdateHud();
		
		game_controller.SelectedShip=null;
	}
	
	public void UpdateShipPanel(){
		ship_panel.UpdateHud();
	}
	
	public void NextTurn(){
		world.NextTurn();
		turn_label.text="Turn "+world.Data.Turn;
		
		UpdateHud();
	}
}
