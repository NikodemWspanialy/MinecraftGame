using Hiscraft.Entities.BlockTypeEntities;
using Hiscraft.WorldModels;
using SimplexNoise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.GeneratingTerrain
{
	internal static class Procedural1
	{
		private const float Scale = 0.005f;
		private const float DetailScale = 0.1f;
		internal static BlockType Find(int x, int y, int z)
		{
			if (y == 0)
				return BlockType.Bedrock;

			int terrainHeight = (int)((Noise.CalcPixel2D(x, z, Scale) / 255.0f) * WorldConst.HIGH);

			if (y == 0)
				return BlockType.Bedrock;

			if (y < terrainHeight - 8)
				return GenerateGroundLayer(x, y, z);

			if (y < terrainHeight - 3)
				return BlockType.Stone;

			if (y < terrainHeight)
				return BlockType.Dirt;

			if (y == terrainHeight)
			{
				if (terrainHeight < 12)
					return BlockType.Sand;

				if (terrainHeight > 45)
					return BlockType.Snow;



				return BlockType.Grass;
			}
			if (terrainHeight + 1 == y)
				return GenerateRareBlocksOnSurfaces(x, z);

			if (terrainHeight < 10 && y < 10)
				return BlockType.Water;

			return BlockType.Empty;
		}
		/// <summary>
		/// Generated rare orbs and stone
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <returns></returns>
		private static BlockType GenerateGroundLayer(int x, int y, int z)
		{
			float oreNoise = Noise.CalcPixel3D(x, y, z , DetailScale) / 255.0f;

			if (oreNoise > 0.9877f && y < WorldConst.HIGH / 2)
				return BlockType.Diamond;

			if (oreNoise > 0.92f && y < WorldConst.HIGH / 2)
				return BlockType.Coal;

			return BlockType.Stone;
		}

		private static BlockType GenerateRareBlocksOnSurfaces(int x, int z)
		{
			float detailNoise = Noise.CalcPixel2D(x, z, DetailScale) / 255.0f;

			if (detailNoise > 0.9399f)
				return BlockType.Pumpkin;

			return BlockType.Empty;
		}
	}
}
