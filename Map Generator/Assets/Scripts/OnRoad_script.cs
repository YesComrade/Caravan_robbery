using UnityEngine;
using System.Collections;

public class OnRoad_script : MonoBehaviour {
	public Transform road;
	public Transform dude;
	void Start () 
	{
	}
	void Update () 
	{
	}
	void OnMouseDown()
	{
		int coord_x = 0,coord_y = 0;
		float current_coord_x = road.position.x;
		float current_coord_y = road.position.y;
		while (current_coord_x >= -8.76)
		{
			current_coord_x -= 1.26f;
			coord_x++;
		}
		while(current_coord_y <= -1.26)
		{
			current_coord_y +=1.26f;
			coord_y++;
		}
		coord_y++;
		//Debug.Log(coord_x+" "+coord_y);
		//Vector3 vec = new Vector3(-8.76f+1.26f*coord_x,-1.26f-1.25f*coord_y,-1);
		Vector3 vec = new Vector3(-10f+1.26f*coord_x,0f-1.26f*coord_y,-1);
	//	switch(check_place(coord_x,coord_y))
	//	{
	//	case 1:vec.x += 1.26f;break;
	//	case 2:vec.x -= 1.26f;break;
	//	case 3:vec.y -= 1.26f;break;
	//	case 4:vec.y += 1.26f;break;
	//	}
		Instantiate(dude,vec,Quaternion.identity);
	}
	int check_place(int x,int y)
	{
		if (Map.game_map[x+1,y]!=-2)
			return 1;
		else if (Map.game_map[x-1,y]!=-2)
			return 2;
		else if (Map.game_map[x,y-1]!=-2)
			return 3;
		else if (Map.game_map[x,y+1]!=2)
			return 4;
		return -1;
	}
}
