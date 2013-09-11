using UnityEngine;
using System.Collections;

public class BuildItem : MonoBehaviour{
	
	public UILabel name_label,time_label;
	public UISprite background_spr;
	public ShipPanel ship_panel;
	public UIButton This;
	
	ShipData _ship;//DEV.Temp build item
	
	public ShipData Ship{get{return _ship;}}
	
	bool selected=false;
	
	public bool Selected{get{return selected;}
		set{
			selected=value;
			MenuMain.SetSelected(value,This,background_spr);
	}}

	public void setBuildItem(ShipData ship){
		_ship=ship;
		name_label.text=ship.Name;
		time_label.text="5 turns.";
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
