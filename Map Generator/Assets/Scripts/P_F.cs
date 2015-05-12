using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace path_finder 
{
	class GameObject
	{
		static public int EMPTY = -1;
		static public int CITY = -2;
		// city mark for pathfinder (Active City)
		static public int ACITY = -3;
		static public int WALL = -8;
		static public int ROAD = -7;
	}
	
	class Point
	{
		public int i;
		public int j;
		public Point(int ii, int jj)
		{
			this.i = ii;
			this.j = jj;
		}
		public Point(Point point)
		{
			this.i = point.i;
			this.j = point.j;
		}
	}
	
	class PathFinder
	{
		public int[,] final_field;
		public int[,] adj_matrix;
		
		// private attributes
		
		private int field_width;
		private int field_height;
		private int[,] raw_field;
		
		// coefficients for checking map cells
		private int[] di = { 0, 1, 0, -1 };
		private int[] dj = { -1, 0, 1, 0 };
		
		// public methods
		
		public PathFinder(int [,] raw_field, int height, int width)
		{
			this.field_width = width;
			this.field_height = height;
			
			this.raw_field = raw_field;
			this.final_field = new int[this.field_height, this.field_width];
			System.Array.Copy (this.raw_field, 0, this.final_field, 0, this.raw_field.Length);
		}
		
		public void buildPaths()
		{
			List<Point> cities = this.findCities ();
			this.adj_matrix = new int[cities.Count, cities.Count];
			
			//Console.WriteLine (cities.Count);
			//Point start = new Point(3, 2);
			//Point end = new Point(3, 6);
			
			// builds paths from current city to other cities
			for (int i = 0; i < cities.Count; ++i) {
				Point start = new Point(cities[i]);
				
				for (int j = i + 1; j < cities.Count; ++j) {
					Point end = new Point (cities[j]);
					
					// create temp field that will save cost for all paths
					int[,] cost_field = new int[this.field_height, this.field_width];
					System.Array.Copy (this.raw_field, 0, cost_field, 0, this.raw_field.Length);
					
					// make both cities on map as active
					cost_field [start.i, start.j] = GameObject.ACITY;
					cost_field [end.i, end.j] = GameObject.ACITY;
					
					int len = this.waveFindPath (cost_field, start, end);
					// now we can use len as edge between two vertices
					this.adj_matrix [i, j] = len;
					this.adj_matrix [j, i] = len;
					
					//Console.WriteLine("END FOUND ");
					//Console.WriteLine (len);
					
					List<Point> path = this.getPathCoords (cost_field, end);
					// put path on field
					foreach (Point point in path) {
						this.final_field [point.i, point.j] = GameObject.ROAD;
					}
					// put cities on field
					this.final_field[start.i, start.j] = GameObject.CITY;
					this.final_field[end.i, end.j] = GameObject.CITY;
				}
			}
		}

		public void printFinalField()
		{
			for (int i = 0; i < this.field_height; ++i) {
				for (int j = 0; j < this.field_width; ++j) {
					System.Console.Write (this.final_field [i, j]);
					System.Console.Write ("\t");
				}
				System.Console.WriteLine ();
			}
		}

		public void give_me_my_field(int[,]res_field)
		{
			for (int i=0;i<this.field_height;i++)
				for (int j=0;j<this.field_width;j++)
					res_field[i,j]=this.final_field[i,j];
		}
		
		// private methods
		
		private int waveFindPath(int [,] field, Point start, Point end)
		{
			List<Point> wave = new List<Point>();
			List<Point> oldWave = new List<Point>();
			
			oldWave.Add(start);
			int nstep = 0;
			field[start.i, start.j] = nstep;
			
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
						
						if (field[ni, nj] == GameObject.EMPTY || field[ni, nj] == GameObject.ACITY)
						{
							wave.Add(new Point(ni, nj));
							field[ni, nj] = nstep;
							if (ni == end.i && nj == end.j)
								return nstep;
						}
					}
				}
				oldWave = new List<Point>(wave);
			}
			return 0;
		}
		
		private List<Point> getPathCoords(int [,] field, Point end)
		{
			int i = end.i;
			int j = end.j;
			List<Point> wave = new List<Point> ();
			wave.Add (end);
			
			while(field[i, j] != 0)
			{
				for (int d = 0; d < 4; ++d) {
					int ni = i + this.di [d];
					int nj = j + this.dj [d];
					
					if (field [i, j] - 1 == field [ni, nj]) {
						i = ni;
						j = nj;
						wave.Add (new Point(i, j));
						break;
					}
				}
			}
			return wave;
		}
		
		private List<Point> findCities()
		{
			// find city mark on field and return it
			List<Point> cities = new List<Point>();
			
			for (int i = 0; i < this.field_height; ++i)
				for (int j = 0; j < this.field_width; ++j)
					if (this.raw_field [i, j] == GameObject.CITY)
						cities.Add (new Point (i, j));
			
			return cities;
		}
		
	}
}
