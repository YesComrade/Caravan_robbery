using System;

namespace GameMap
{
	public class Map 
	{
		public int height;
		public int width;
		public int[,] map;

		public Map(int height, int width)
		{
			this.height = height;
			this.width = width;
			this.map = new int[this.height, this.width];
		}

		public int get_el(int a,int b)
		{
			return map[a,b];
		}

		public void print_map()
		{
			for (int i = 0; i < this.height; ++i) {
				for (int j = 0; j < this.width; ++j) {
					Console.Write (this.map [i, j]);
					Console.Write ("\t");
				}
				Console.WriteLine ();
			}
		}

		public void pprint_map()
		{
			for (int i = 0; i < this.height; ++i) {
				for (int j = 0; j < this.width; ++j) {
					var tile = this.map [i, j];
					if (tile == GameObject.EMPTY)
						Console.Write (".");
					else if (tile == GameObject.CITY)
						Console.Write ("[]");
					else if (tile == GameObject.ROAD)
						Console.Write ("=");
					else if (tile == GameObject.WALL)
						Console.Write ("8");
					else if (tile == GameObject.MAIN_CITY)
						Console.Write ("[+]");
					else
						Console.Write ("err");
					Console.Write ("\t");
				}
				Console.WriteLine ();
			}
		}

	}
}

