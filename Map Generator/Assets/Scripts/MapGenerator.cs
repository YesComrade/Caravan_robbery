using System;
using System.Collections.Generic;

namespace GameMap
{
	public class MapGenerator 
	{
		private System.Random rand = new System.Random ();
		private PathFinder pathfinder = new PathFinder();

		private int map_height;
		private int map_width;
		private int city_num;
		private int empty_radius;

		private Map game_map;
		private Point main_city;
		private List<Point> cities;

		public MapGenerator()
		{
		}

		public MapGenerator(int height, int width, int city_num, int empty_radius=0)
		{
			this.init_mapgenerator (height, width, city_num, empty_radius);
		}

		public void init_mapgenerator(int height, int width, int city_num, int empty_radius=0)
		{
			this.map_height = height;
			this.map_width = width;
			this.city_num = city_num;
			this.empty_radius = empty_radius;
			this.main_city = new Point (this.map_height/2, this.map_width/2);
			//this.init_map ();
		}

		public void generate()
		{
			// create new object for raw
			this.init_map ();

			// get random coords for cities except main city (was placed early)
			this.cities = this.random_coords();

			foreach(Point city in this.cities)
			{
				//Console.WriteLine ("{0}, {1}", city.i, city.j);
				this.game_map.map [city.i, city.j] = GameObject.CITY;
			}

			// place paths
			// create city list with main city
			List<Point> city_list = this.cities.ConvertAll(point => new Point(point.i, point.j));
			// Main City must be last element in list, for Main Path finding
			city_list.Add (this.main_city);

			this.pathfinder.init_pathfinder(this.game_map, city_list);
			this.pathfinder.gen_allpaths ();
			this.game_map = this.pathfinder.get_finalmap ();
			// place Main City marker on center (path finder don't know about it)
			this.place_maincity ();

			// ****** get short path
			this.pathfinder.gen_main_greedypath(city_list.Count-1);

			//return this.game_map;
		}

		public Map get_gamemap()
		{
			return this.game_map;
		}

		public List<Point> get_mainpath()
		{
			return this.pathfinder.get_mainpath ();
		}

		public List<List<Point>> get_allpaths()
		{
			return this.pathfinder.get_allpaths ();
		}

		public Map get_empty_map()
		{
			return this.game_map;
		}

		private void init_map()
		{
			this.game_map = new Map (this.map_height, this.map_width);

			// init map with empty values
			for (int i = 0; i < this.game_map.height; ++i)
				for (int j = 0; j < this.game_map.width; ++j)
					this.game_map.map [i, j] = GameObject.EMPTY;

			// draw borders
			for (int i = 0; i < this.game_map.height; ++i) {
				this.game_map.map [i, 0] = GameObject.WALL;
				this.game_map.map [i, this.game_map.width-1] = GameObject.WALL;
			}
			for (int i = 0; i < this.game_map.width; ++i) {
				this.game_map.map [0, i] = GameObject.WALL;
				this.game_map.map [game_map.height-1, i] = GameObject.WALL;
			}

			this.place_maincity ();
		}

		private void place_maincity()
		{
			// Place Main City on center
			this.game_map.map[this.main_city.i, this.main_city.j] = GameObject.MAIN_CITY;
		}

		// return true if current coord placed on empty region
		// that don't have any other point on specific radius
		private bool has_neighbors(Point curr_coord, List<Point> ncoords, int tabu_radius)
		{
			// Create copy of neighbors coordinates ...
			List<Point> coords = ncoords.ConvertAll(point => new Point(point.i, point.j));
			// ... and add Main Point
			coords.Add (this.main_city);

			foreach (Point ncoord in coords) {
				for (int radius = 0; radius <= tabu_radius; radius++) {
					if (curr_coord.i + radius == ncoord.i
						|| curr_coord.i - radius == ncoord.i
					    || curr_coord.j + radius == ncoord.j
						|| curr_coord.j - radius == ncoord.j)
						return true;
				}
			}
			return false;
		}

		private List<Point> random_coords()
		{
			List<Point> coords = new List<Point> ();
			Point rnd_point;
			for (int i = 0; i < city_num; ++i) {
				rnd_point = new Point (0, 0);

				// try generate new coordinates while it does not matches requirements
				do {
					rnd_point.i = this.rand.Next (1, this.map_height-1);
					rnd_point.j = this.rand.Next (1, this.map_width-1);
				} while( has_neighbors(rnd_point, coords, this.empty_radius) );

				coords.Add (rnd_point);
			}
			return coords;
		}
	//
	}
}

