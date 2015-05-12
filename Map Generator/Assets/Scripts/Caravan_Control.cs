using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameMap
{
public class Caravan_Control : MonoBehaviour {
	
	public Transform caravan;
	public float speed = 0.09f;
	public static int gold = 100;

	private List<Point> way;
	private int masive_x,masive_y;
	private int iterator = 1;
	//private int p_x=0, p_y=0;
	private int curr_x,curr_y;
	private bool on_the_way = false;
	private float travel_count = 0f;
	private string direction = "NONE";
	
	 void Update()
	{
		if (MainClass.tell_ai_to_move)
		{
//			int road_x = way[iterator].i;
//			int road_y = way[iterator].j;
//			Debug.Log("Way.x"+road_x);
//			Debug.Log("Way.y"+road_y);
//			// AHTUNG!!!!!!  КОСТЫЛЬ!!!!  оставь надежду, всяк сюда входящий
//			if (iterator+1<way.Count)
//			{
//
//			 if (p_x>way[iterator+1].i&&p_y<way[iterator].j||p_x<way[iterator+1].i&&p_y<way[iterator].j)
//			   map_to_mas_coord(caravan.position.x,caravan.position.y,2);
//
//			else if (p_y>way[iterator+1].j&&p_x>way[iterator].i||p_y<way[iterator+1].j&&p_x>way[iterator].i)
//				map_to_mas_coord(caravan.position.x,caravan.position.y,1);
//
//			 else
//			   map_to_mas_coord(caravan.position.x,caravan.position.y,0);
//
//			}
//			else map_to_mas_coord(caravan.position.x,caravan.position.y,0);
//			//------------------------------------------------------------
//			Debug.Log("mas.x"+masive_x);
//			Debug.Log("mas.y"+masive_y);
//			Vector3 temp = caravan.position;
//			if (road_x>masive_x)
//				temp.x += speed;
//			else if (road_x<masive_x)
//				temp.x -=speed;
//			else if (road_y>masive_y)
//				temp.y -= speed;
//			else if (road_y<masive_y)
//				temp.y += speed;
//			else if (road_x==masive_x&&road_y==masive_y&&iterator<way.Count-1)
//				iterator++;
//			caravan.position = temp;
//				p_x = masive_x;
//				p_y = masive_y;
//			Debug.Log("GOLD :"+gold);


				if(!on_the_way)
					direction = GPS ();
				mover(direction);
		}
	}
		//convert map coordinates to massive
		private void map_to_mas_coord(float a, float b,int flag)
		{
			int coord_x=0,coord_y=0;
			float current_coord_x = a;
			float current_coord_y = b;
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
			if (flag == 0)
			coord_y++;
			if (flag == 1)
			coord_x--;
			this.masive_x = coord_x;
			this.masive_y = coord_y;
		}

		void Start()
		{
			this.way = MainClass.caravan_path;
			this.curr_x = way[0].i;
			this.curr_y = way[0].j;
		}

		private string GPS ()
		{
			if (iterator<way.Count-1)
			{
				
				if (way[iterator].i>curr_x)
				{
					iterator++;
					curr_x++;
					on_the_way = true;
					return "RIGHT";
				}
				else if (way[iterator].i<curr_x)
				{
					iterator++;
					curr_x--;
					on_the_way = true;
					return "LEFT";
				}
				else if (way[iterator].j>curr_y)
				{
					iterator++;
					curr_y++;
					on_the_way = true;
					return "DOWN";
				}
				else if (way[iterator].j<curr_y)
				{
					iterator++;
					curr_y--;
					on_the_way = true;
					return "UP";
				}
				else if (way[iterator].i==curr_x&&way[iterator].j==curr_y)
					iterator++;
			}
			return "NONE";
		}

		private void mover (string dir)
		{
			Vector3 temp = caravan.position;
			if (travel_count<=1.26f)
			switch(dir)
			{
				case "RIGHT":temp.x += speed;travel_count += speed;caravan.position = temp; break;
				case "LEFT":temp.x -= speed;travel_count += speed;caravan.position = temp; break;
				case "DOWN":temp.y -= speed;travel_count += speed;caravan.position = temp; break;
				case "UP":temp.y += speed;travel_count += speed;caravan.position = temp; break;
			}
			else
			{
				on_the_way = false;
				travel_count = 0f;
			}
		}

}
}
