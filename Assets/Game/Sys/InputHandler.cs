using UnityEngine;
using System.Collections;

public class InputHandler{
	
	public static bool GetMouseClicked(int index){
		if (UICamera.hoveredObject!=null) return false;
		return Input.GetMouseButtonDown(index);
	}
}
