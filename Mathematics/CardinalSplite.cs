using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.Mathematics
{
	/// <summary>
	/// Class of cardinal function in program.
	/// </summary>
	internal class CardinalSplite
	{
		/// <summary>
		/// List of fold function points.
		/// </summary>
		List<Point> points;
		public CardinalSplite(List<Point> points)
		{
			this.points = points;
			sort();
		}

		/// <summary>
		/// Constructor without parameters.
		/// </summary>
		public CardinalSplite()
		{
			points = [];
			sort();
		}
		/// <summary>
		/// methods adding points to points collection.
		/// </summary>
		/// <param name="point">point to add to funtion</param>
		public void AddPoint(Point point)
		{
			points.Add(point);

		}
		/// <summary>
		/// Private method sorting list.
		/// </summary>
		private void sort() => points.Sort((p1, p2) => p1.CompareTo(p2));

		/// <summary>
		/// Method reurning list of points.
		/// </summary>
		/// <returns>List of points</returns>
		public IList<Point> GetPoints()
		{
			return points;
		}

		/// <summary>
		/// Calculate value for value in range 0 - 1.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public float GetValue(float x)
		{
			if (x < points[0].X || x > points[points.Count - 1].X)
			{
				return 0f;
			}
			for (int i = 1; i < points.Count; ++i)
			{
				if (points[i].X >= x)
				{
					float offsetX = (x - points[i - 1].X) / (points[i].X - points[i - 1].X);
					var offsetY = (points[i].Y - points[i - 1].Y);
					var offset = offsetY * offsetX;
					return points[i - 1].Y + offset;
				}
			}
			return 0f;
		}
	}
}
