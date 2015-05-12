using UnityEngine;
using System.Collections;

public class Camera_Control : MonoBehaviour {
	// Use this for initialization
	private Vector3 vec;
	private float border = 10;
	private float camspeed = 0.3f;
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		vec = Input.mousePosition;
		if (vec.x<0+border)
		{
			Vector3 temp = Camera.main.transform.position;
			temp.x -= camspeed;
			Camera.main.transform.position = temp;
		}
		else if (vec.x>Screen.width-border)
		{
			Vector3 temp = Camera.main.transform.position;
			temp.x += camspeed;
			Camera.main.transform.position = temp;
		}
		else if (vec.y<0+border)
		{
			Vector3 temp = Camera.main.transform.position;
			temp.y -= camspeed;
			Camera.main.transform.position = temp;
		}
		else if (vec.y>Screen.height-border)
		{
			Vector3 temp = Camera.main.transform.position;
			temp.y += camspeed;
			Camera.main.transform.position = temp;
		}
		if (Input.GetAxis("Mouse ScrollWheel")<0)
			if (Camera.main.orthographicSize<=10)
			Camera.main.orthographicSize ++;
	    if (Input.GetAxis("Mouse ScrollWheel")>0)
			if (Camera.main.orthographicSize>=3)
			Camera.main.orthographicSize--;
	}
}
