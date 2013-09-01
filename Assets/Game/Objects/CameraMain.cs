using UnityEngine;
using System.Collections;

public class CameraMain: MonoBehaviour {
	
	public Transform Target;
	public float start_distance=5,x_speed=1,y_speed=1,scroll_speed=4,distance_change_speed=4,position_change_speed=4;
	public float min_dis=1,max_dis=200;
	
	float x=0,y=0,new_distance,distance,zooming_speed;
	float saved_distance=0;
	bool distance_toggled=false,toggle_zooming;
	bool changing_pos=false,zooming=false;
	
	public float Distance{get{return distance;}}

	void Start (){
		setDistance(start_distance);
		
		x=transform.rotation.eulerAngles.y;
		y=transform.rotation.eulerAngles.x;
		
		zooming_speed=distance_change_speed;
	}

	void Update () {
		//input
	 	if (Input.GetButton("CameraRotate")){
			if (!changing_pos)
				Rotate();
		}
		
		var sw=Input.GetAxis("Mouse ScrollWheel");
		if (sw!=0){
			if (!toggle_zooming){
				setDistanceMulti(-sw*scroll_speed);
				if (new_distance>saved_distance){
					distance_toggled=false;
				}
			}
		}
		
		//position change
		if (zooming){
			var dis=new_distance-distance;
			distance+=dis*0.1f*Time.deltaTime*zooming_speed;
			if (Mathf.Abs(dis)<0.1f){
				zooming=toggle_zooming=false;
				distance=new_distance;
				zooming_speed=distance_change_speed;
			}
		}
		
		if (Target){
			//Move the camera to look at the target
			var position = Target.position+ transform.rotation * Vector3.forward*-distance;
			
			var dir=(position-transform.position);
			if (changing_pos){
				transform.position+= dir.normalized*Time.deltaTime*dir.magnitude*position_change_speed;
				if (dir.magnitude<0.1f){
					changing_pos=false;
				}
			}
			else{
				transform.position=position;
			}
		}
	}
	
	public void SetTarget(Transform target){
		Target=target;
		changing_pos=true;
	}
	
	void Rotate(){
		//Change the angles by the mouse movement
		x += Input.GetAxis("Mouse X") * x_speed * 0.02f;
		y -= Input.GetAxis("Mouse Y") * y_speed * 0.02f;
		
		y=Mathf.Clamp(y,-89,89);//DEV. 1 deg diff fixes hud swap
		
		//Rotate the camera correctly
		var rotation = Quaternion.Euler(y, x, 0);
		transform.rotation = rotation;
	}
	
	public void setDistance(float dist){
		dist=Mathf.Clamp(dist,min_dis,max_dis);
		new_distance=dist;
		zooming=true;
	}
	
	/// <summary>
	/// +||- multiplier
	/// </param>
	public void setDistanceMulti(float multiplier){
		setDistance(distance+distance*multiplier);
	}

	public void toggleDistance ()
	{
		toggle_zooming=true;
		zooming_speed=100;
		if (distance_toggled){
			distance_toggled=false;
			setDistance(saved_distance);
		}
		else{
			distance_toggled=true;
			saved_distance=Distance;
			setDistance(min_dis);
		}
	}
}

