using UnityEngine;
using System.Collections;

namespace GameMap
{
 public class Draw {
		//private
		private Map map;
		private int m_height,m_width;
		private Transform land; //land prefab
		private Transform main_city; //main city prefab
		private Transform city; //city prefab
		private Transform road; //road prefab
		//constructor
		public Draw(Map gmap, int height, int width,Transform land_1,Transform main_castle,Transform castle,Transform way)
		{
			this.map = gmap;
			this.m_height = height;
			this.m_width  = width;
			this.land = land_1;
			this.main_city = main_castle;
			this.city = castle;
			this.road = way;
		}
		//actually draw map
		public void display()
		{
			Vector3 p = new Vector3(0,0,-1);
			for (int i=0;i<this.m_height;i++)
			{
				if (i==0)
					p.x = -10;
				for (int j=0;j<this.m_width;j++)
				{
					if (j==0)
						p.y = 0;
					switch (this.map.get_el(i,j))
					{
					case -1:UnityEngine.MonoBehaviour.Instantiate(land,p,Quaternion.identity);break;
					case -2:UnityEngine.MonoBehaviour.Instantiate(city,p,Quaternion.identity);break;
					case -3:UnityEngine.MonoBehaviour.Instantiate(main_city,p,Quaternion.identity);break;
					case -7:UnityEngine.MonoBehaviour.Instantiate(road,p,Quaternion.identity);break;
					};
					p.y -= 1.26f;
				}
				p.x += 1.26f;
			}
		}
 }
}