using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.Mathematics
{
	internal class Point : IComparable<Point>
	{
		public float X;
		public float Y;

		public int CompareTo(Point? other)
		{
			return X.CompareTo(other?.Y);
		}
		public override string ToString()
		{
			return $"X: {X}, Y: {Y}";
		}
	}
}
