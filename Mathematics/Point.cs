using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.Mathematics
{
	/// <summary>
	/// Class of point
	/// </summary>
	internal class Point : IComparable<Point>
	{
		/// <summary>
		/// X coordinate.
		/// </summary>
		public float X;

		/// <summary>
		/// Y coordinate.
		/// </summary>
		public float Y;

		/// <summary>
		/// Method from ICoparable interface.
		/// </summary>
		/// <param name="other"></param>
		/// <returns>bool for comparing rules</returns>
		public int CompareTo(Point? other)
		{
			return X.CompareTo(other?.Y);
		}

		/// <summary>
		/// ToString methods overrided.
		/// </summary>
		/// <returns>point as string</returns>
		public override string ToString()
		{
			return $"X: {X}, Y: {Y}";
		}
	}
}
