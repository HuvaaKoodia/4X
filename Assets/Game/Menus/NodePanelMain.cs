using UnityEngine;
using System.Collections;

public class NodePanelMain : MonoBehaviour {
	
	public UILabel text;
	
	NodeMain _node;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetNode(NodeMain node){
		_node=node;
		UpdateHud();
	}
	
	public void UpdateHud(){
		var data=_node.GetComponent<NodeMain>().Data;
		
		var str="Name: "+data.Name+"\n"+
			"Resources: "+data.Matter+"\n\n"+
			"Colony: ";
		
		if (data.HasColony()){
			str+=data.Colony.Faction.Name+"\n"+
				"Energy: "+data.Colony.Energy+"\\"+data.Colony.EnergyMax+"\n"+
				"Industry: "+data.Colony.Industry;
		}
		else{
			str+="None";
		}
		
		text.text=str;
	}
}

