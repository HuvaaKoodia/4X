using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIMemoryData{
	
	FactionData faction;
	
	public Dictionary<NodeData,NodeMemory> KnownNodes;
	
	public AIMemoryData(FactionData fac){
		faction=fac;
		KnownNodes=new Dictionary<NodeData, NodeMemory>();
	}
}
