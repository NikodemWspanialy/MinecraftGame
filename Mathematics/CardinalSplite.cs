using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.Mathematics
{
	internal class CardinalSplite
	{
		List<Point> points;
		public CardinalSplite(List<Point> points)
		{
			this.points = points;
			sort();
		}
		public CardinalSplite()
		{
			points = [];
			sort();
		}

		public void AddPoint(Point point)
		{
			points.Add(point);

		}
		private void sort()
		{
			points.Sort((p1, p2) => p1.CompareTo(p2));

		}

		public IList<Point> GetPoints()
		{
			return points;
		}
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
