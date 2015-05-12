using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameMap
{
	class MainClass : MonoBehaviour
	{
		public  Transform land; //land prefab
		public  Transform main_city; //main city prefab
		public  Transform city; //city prefab
		public  Transform road; //road prefab
		public  Transform carriage;  //caravan prefab

		public int map_height = 17;
		public int map_width = 11;
		public int city_num	 = 2;
		public int empty_radius = 2;
		public static List<Point> caravan_path;
		public static bool tell_ai_to_move = false;

		public static void test_fixedmap()
		{
			int map_height = 17;
			int map_width = 11;
			int city_num = 3;
			MapGenerator mg = new MapGenerator (map_height, map_width, city_num);
			Map game_map = mg.get_empty_map ();

			game_map.map [1, 1] = GameObject.CITY;
			game_map.map [2, 1] = GameObject.CITY;
			game_map.map [15, 9] = GameObject.CITY;
			game_map.map [5, 8] = GameObject.CITY;

			game_map.pprint_map ();
		}

		public void test_rndmap()
		{
			// for map size recomends use odd values

			// Create Map Generator object
			MapGenerator mg = new MapGenerator ();

			// then init it with map params
			mg.init_mapgenerator (map_height, map_width, city_num, empty_radius);

			// and generate game map
			mg.generate ();

			// get result
			Map game_map = mg.get_gamemap ();

			// also you can get Optimal path for caravan
			caravan_path = mg.get_mainpath();

			// DEBUG - print path coordinates
//			Console.WriteLine("Caravan path:");
//			foreach (var p in caravan_path)
//				Console.Write ("({0},{1}) ", p.i, p.j);
//			Console.Write ("\n\n");

			// and coordinates for roads on map
			List<List<Point>> roads = mg.get_allpaths();

			// DEBUG - print roads coordinates
//			Console.WriteLine("Roads:");
//			foreach (List<Point> road in roads) {
//				foreach (Point coord in road)
//					Console.Write ("({0},{1}) ", coord.i, coord.j);
//				Console.Write ("\n\n");
//			}

			//game_map.pprint_map ();
			Draw painter = new Draw (game_map,map_height,map_width,land,main_city,city,road);
			painter.display();

			Caravan cargo = new Caravan(map_height/2,map_width/2,carriage);
			cargo.caravan_place();
			//pf.greedy_path (0);
		}
		void Start()
		{
			test_rndmap();
		}
		public void turn_pass(bool flag)
		{
			tell_ai_to_move = flag;
		}
	}
}

