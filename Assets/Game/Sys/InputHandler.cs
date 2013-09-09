using UnityEngine;
using System.Collections;

public class InputHandler{
	
	public static bool GetMouseClicked(int index){
		if (UICamera.hoveredObject!=null) return false;
		return Input.GetMouseButtonDown(index);
	}

	public static bool GetControl()
	{
		return Input.GetKey(KeyCode.LeftControl)||Input.GetKey(KeyCode.RightControl);
	}
	public static bool GetShift()
	{
		return Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift);
	}
}
