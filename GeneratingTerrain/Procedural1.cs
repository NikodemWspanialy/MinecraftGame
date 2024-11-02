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
		private const float Scale = 0.05f; // Skalowanie szumu Simplex, kontroluje wygładzenie terenu
		private const int MaxHeight = 64; // Maksymalna wysokość terenu
		internal static BlockType Find(int x, int y, int z)
		{
			if (y == 0)
				return BlockType.Bedrock;

			// Ustal wysokość terenu na podstawie szumu Simplex
			int terrainHeight = (int)((Noise.CalcPixel2D(x, z, Scale) / 255.0f) * MaxHeight);

			// Ustal typ bloku na podstawie wysokości terenu i aktualnej wysokości y
			if (y < terrainHeight - 4)
			{
				return BlockType.Stone; // Głębsze warstwy to kamień
			}
			else if (y < terrainHeight)
			{
				return BlockType.Dirt; // Warstwa ziemi
			}
			else
			{
				return BlockType.Empty; // Pusta przestrzeń nad ziemią
			}
		}
	}
}
