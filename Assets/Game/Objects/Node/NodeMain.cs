using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeMain : MonoBehaviour {
	
	public GameObject graphics;
	List<NodeMain> links=new List<NodeMain>();
	public NodeData Data{get;private set;}
	public UILabel ship_amount_label;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
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
		if (Data.Ships.Count>0){
			number+=Data.Ships.Count;
		}
		ship_amount_label.text=number;
	}
}
