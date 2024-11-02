using Hiscraft.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.GeneratingTerrain
{
	internal static class Procedural1
	{
		internal static BlockType Find(int x, int y, int z)
		{
			if (y < 25) return BlockType.Stone;
			if (y < 30) return BlockType.Dirt;
			return BlockType.Empty;
		}
	}
}
