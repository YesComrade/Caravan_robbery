using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameMap
{
public class Caravan {
	//private-------------
	// on map coordinates
	private float actual_x,actual_y;
	// on massive coordinates
	private int masive_x,masive_y;
	private Transform caravan;
	//constructor
		public Caravan(int x,int y,Transform cargo)
	{
		this.masive_x = x;
		this.masive_y = y;
		this.caravan = cargo;
	}
		//convert massive coordinates to map
	 private void mass_to_map_coord(int a,int b)
		{
			this.actual_x = -10f+1.26f*a;
			this.actual_y = 0f-1.26f*b;
		}
		//place caracan at start point
	 public void caravan_place()
		{
			mass_to_map_coord(this.masive_x,this.masive_y);
			Vector3 vec  = new Vector3(this.actual_x,this.actual_y,-2);
			UnityEngine.Transform.Instantiate(caravan,vec,Quaternion.identity);
		}

 }
}
