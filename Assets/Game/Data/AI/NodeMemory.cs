using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeMemory{
	
	public NodeMemory(NodeData n){
		Node=n;
		//ShipsTravelling=new List<ShipData>();
	}
	
	public bool Known=false;
	public NodeData Node;
	//public List<ShipData> ShipsTravelling;
	public bool colonization_target=false;
	
	public bool ColonizationTarget(){
		return colonization_target;
	}
}
