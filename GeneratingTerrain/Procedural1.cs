using Hiscraft.Entities.BlockTypeEntities;
using Hiscraft.WorldModels;
using SimplexNoise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Hiscraft.GeneratingTerrain
{
	internal static class Procedural1
	{
		private const float Scale = 0.005f; // Skalowanie szumu Simplex, kontroluje wygładzenie terenu
		internal static BlockType Find(int x, int y, int z)
		{
			if (y == 0)
				return BlockType.Bedrock;

			int terrainHeight = (int)((Noise.CalcPixel2D(x, z, Scale) / 255.0f) * WorldConst.HIGH);

			if (y < terrainHeight - 4)
			{
				return BlockType.Stone; 
			}
			else if (y == terrainHeight)
			{
				return BlockType.Grass;
			}
			else if (y < terrainHeight)
			{
				return BlockType.Dirt; 
			}
			else if (y < 32)
			{
				return BlockType.Water;
			}
			else
			{
				return BlockType.Empty;
			}
		}
	}
}
