using UnityEngine;
using System.Collections;
namespace GameMap
{
public class crime_check : MonoBehaviour {
	
		public Collider2D caravan;
		public Object dude;

		void OnTriggerExit2D (Collider2D coll)
		{
			if (coll = caravan)
			{
				Caravan_Control.gold-=10;
				Destroy(dude);
			}
		}
}
}