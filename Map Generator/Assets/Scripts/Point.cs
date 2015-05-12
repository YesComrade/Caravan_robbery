using System;

namespace GameMap
{
	public class Point 
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

		// override std methods for using Point as a Dictionary key
		// https://stackoverflow.com/questions/6999191/use-custom-object-as-dictionary-key
		public override int GetHashCode()
		{
			return this.i.GetHashCode () ^ this.j.GetHashCode ();
		}
		public override bool Equals(object obj) {
			return Equals(obj as Point);
		}
		public bool Equals(Point obj) {
			return obj != null && obj.i == this.i && obj.j == this.j;
		}
	}
}

