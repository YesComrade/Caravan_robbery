using System;
using System.Collections.Generic;

namespace GameMap
{
	class PathFinder 
	{
		/* TODO
		 * 1. pass map on Path Finder
		 * 2. find roads berween cities and save it in list (don't put paths on final map)
		 * 3. using adj_matrix find optimal path and save it
		 * 4. remove some paths except optimal
		 * 5. put all paths on Map
		 * 
		 * Warning! build_paths returns class attribute
		 */

		// private attributes

		// Active City mark for algorithm
		static private int ACTIVE_CITY = -42;

		private Map final_map;
		private int map_height;
		private int map_width;
		private int[,] raw_map;

		private List<Point> cities;
		private int[,] adj_matrix;
		private List<Point> main_path;
		private Dictionary<Point, List<Point>> paths = new Dictionary<Point, List<Point>>();

		// coefficients for checking map cells
		private int[] di = { 0, 1, 0, -1 };
		private int[] dj = { -1, 0, 1, 0 };

		// **** public methods

		// Constructor
		public PathFinder()
		{
		}

		public PathFinder(Map game_map, List<Point> city_list)
		{
			this.init_pathfinder (game_map, city_list);
		}

		public Map get_finalmap()
		{
			return this.final_map;
		}

		public List<Point> get_mainpath()
		{
			return this.main_path;
		}

		public List<List<Point>> get_allpaths()
		{
			List<List<Point>> all_paths = new List<List<Point>> ();
			//foreach(var pair in this.paths) {
			//	all_paths.Add (pair.Value);
			//}
			for (int i = 0; i < this.cities.Count; ++i)
				for (int j = i + 1; j < this.cities.Count; ++j)
					all_paths.Add (this.paths[new Point(i, j)]);
			return all_paths;
		}

		public void init_pathfinder(Map game_map, List<Point> city_list)
		{
			this.cities = city_list;
			this.final_map = new Map (game_map.height, game_map.width);
			this.map_height = game_map.height;
			this.map_width = game_map.width;

			this.raw_map = game_map.map;
			//this.final_map = new int[this.map_height, this.map_width];
			Array.Copy (this.raw_map, 0, this.final_map.map, 0, this.raw_map.Length);
		}

		public void printAdjMaxtrix()
		{
			for (int i = 0; i < this.adj_matrix.GetLength (0); ++i) {
				for (int j = 0; j < this.adj_matrix.GetLength (1); ++j) {
					Console.Write (this.adj_matrix [i, j]);
					Console.Write ("\t");
				}
				Console.WriteLine ();
			}
		}

		public void gen_allpaths()
		{
			this.adj_matrix = new int[this.cities.Count, this.cities.Count];

			//Console.WriteLine (cities.Count);
			//Point start = new Point(3, 2);
			//Point end = new Point(3, 6);

			// builds paths from current city to other cities
			for (int i = 0; i < this.cities.Count; ++i) {
				Point start = new Point(this.cities[i]);

				for (int j = i + 1; j < this.cities.Count; ++j) {
					Point end = new Point (this.cities[j]);

					// create temp map that will save cost for all paths
					int[,] cost_map = new int[this.map_height, this.map_width];
					Array.Copy (this.raw_map, 0, cost_map, 0, this.raw_map.Length);
					//Array.Copy (this.final_map, 0, cost_map, 0, this.final_map.Length);

					// make both cities on map as active
					cost_map [start.i, start.j] = ACTIVE_CITY;
					cost_map [end.i, end.j] = ACTIVE_CITY;

					int len = this.wave_findpath (cost_map, start, end);
					// now we can use len as cost for edge on graph between two vertices
					this.adj_matrix [i, j] = len;
					this.adj_matrix [j, i] = len;

					//Console.WriteLine("END FOUND ");
					//Console.WriteLine (len);

					// get shortest path using min cost values on costs map (that create algorithm)
					List<Point> path = this.get_pathcoords (cost_map, end);

					// save path
					this.paths.Add(new Point(j, i), path);
					// create path copy
					List<Point> reversed_path = path.ConvertAll (p => new Point(p.i, p.j));
					// ... and reverse it
					reversed_path.Reverse ();
					this.paths.Add(new Point(i, j), reversed_path);

					// Draw Final map
					// put path on map
					foreach (Point point in path) {
						this.final_map.map [point.i, point.j] = GameObject.ROAD;
					}
					// put cities on map
					this.final_map.map [start.i, start.j] = GameObject.CITY;
					this.final_map.map [end.i, end.j] = GameObject.CITY;
				}
			}
		}

		// **** private methods

		private int wave_findpath(int [,] map, Point start, Point end)
		{
			List<Point> wave = new List<Point>();
			List<Point> oldWave = new List<Point>();

			oldWave.Add(start);
			int nstep = 0;
			map[start.i, start.j] = nstep;

			while(oldWave.Count > 0)
			{
				++nstep;
				wave.Clear ();

				foreach(Point point in oldWave)
				{
					for (int d = 0; d < 4; ++d)
					{
						int ni = point.i + this.di[d];
						int nj = point.j + this.dj[d];

						if (map[ni, nj] == GameObject.EMPTY || map[ni, nj] == ACTIVE_CITY)
						{
							wave.Add(new Point(ni, nj));
							map[ni, nj] = nstep;
							if (ni == end.i && nj == end.j)
								return nstep;
						}
					}
				}
				oldWave = new List<Point>(wave);
			}
			return 0;
		}

		private List<Point> get_pathcoords(int [,] map, Point end)
		{
			int i = end.i;
			int j = end.j;
			List<Point> wave = new List<Point> ();
			wave.Add (end);

			while(map[i, j] != 0)
			{
				for (int d = 0; d < 4; ++d) {
					int ni = i + this.di [d];
					int nj = j + this.dj [d];

					if (map [i, j] - 1 == map [ni, nj]) {
						i = ni;
						j = nj;
						wave.Add (new Point(i, j));
						break;
					}
				}
			}
			return wave;
		}

		// Algorithms for finding shortest paths

		public void gen_main_greedypath(int start_point)
		{
			this.main_path = new List<Point> ();
			List<int> visited = new List<int> ();
			visited.Add (start_point);

			int curr_index = start_point;

			for (int i = 0; i < cities.Count-1; ++i) {
				// find index of point with min cost
				int min_cost = int.MaxValue;
				int min_index = 0;

				// get all not visited neighbors and save their cost and index
				for (int ni = 0; ni < cities.Count; ++ni) {
					if (visited.IndexOf (ni) == -1) {
						int path_cost = adj_matrix [curr_index, ni];
						if (path_cost < min_cost) {
							min_cost = path_cost;
							min_index = ni;
						}
					}
				}

				// Add coords to Main Path using graph edge
				//Console.WriteLine ("Point {0}, {1}", curr_index, min_index);
				foreach (Point tile in this.paths[new Point(curr_index, min_index)]) {
					//Console.WriteLine ("{0}, {1}", tile.i, tile.j);
					main_path.Add (new Point(tile.i, tile.j));
				}
				visited.Add (min_index);
				curr_index = min_index;
			}
		}
	}
}
