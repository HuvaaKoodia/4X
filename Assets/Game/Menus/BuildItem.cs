using UnityEngine;
using System.Collections;

public class BuildItem : MonoBehaviour{
	
	public UILabel name_label,time_label;
	public UISprite background_spr;
	public ShipPanel ship_panel;
	public UIButton This;
	
	BuildItemData _item;
	
	public BuildItemData Item{get{return _item;}}
	
	bool selected=false;
	
	public bool Selected{get{return selected;}
		set{
			selected=value;
			MenuMain.SetSelected(value,This,background_spr);
	}}
	
	public void setBuildItem(BuildItemData item){
		_item=item;
		name_label.text="Ship.";
		time_label.text=item.TurnsLeft+" turns.";
	}
	
	void UpdateHud(){
		
	}
	
	void OnClick(){
		Selected=!selected;
		if (!selected){
			ship_panel.RemoveSelectedBuildItem(this);
		}
		else{
			ship_panel.AddSelectedBuildItem(this);
		}
	}
}
