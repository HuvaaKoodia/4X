using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeMain : MonoBehaviour {
	
	public GameObject graphics;
	List<NodeMain> links=new List<NodeMain>();
	public NodeData Data{get;private set;}
	public UILabel ship_amount_label;
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
	
	public void ChangeColor(Color c){
		Subs.ChangeColor(graphics.transform,c);
	}
	
	public void Link(NodeMain node){
		if (links.Contains(node)){
			links.Remove(node);
			return;
		}
		links.Add(node);
	}
	
	public void setData(NodeData data){
		Data=data;
	}
	
	public void UpdateHud(){
		string number="";
		Dictionary<FactionData,int> ships=new Dictionary<FactionData, int>();
		foreach (var s in Data.Ships){
			if (ships.ContainsKey(s.Faction)){
				ships[s.Faction]++;
			}
			else{
				ships.Add(s.Faction,1);
			}
		}
		if (ships.Count!=0){
			foreach(var n in ships){
				number+="["+Subs.ColorToHex(n.Key.Color_)+"]"+n.Value+"\n";
			}
		}
		ship_amount_label.text=number;
	}
}
