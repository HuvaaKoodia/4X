using UnityEngine;
using System.Collections;

public class PositionRelativeToCamera : MonoBehaviour {
	
	public Transform camera,Orbit,Target;
	public Vector3 offset;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var q=Quaternion.LookRotation(camera.position-Orbit.position,Vector3.forward);
		var pos=Orbit.position+q*Vector3.forward;
		Target.position=pos;
	}
}
