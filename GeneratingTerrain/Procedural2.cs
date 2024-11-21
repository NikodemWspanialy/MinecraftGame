using Hiscraft.Entities.BiomeTypeEntities;
using Hiscraft.Entities.BlockTypeEntities;
using Hiscraft.GeneratingTerrain.Collections;
using Hiscraft.WorldModels;
using OpenTK.Mathematics;
using SimplexNoise;
using System.Runtime.CompilerServices;

namespace Hiscraft.GeneratingTerrain
{
	internal class Procedural2
	{
		private static Dictionary<Vector2, int> terrainHeight = [];

		private static int seed = 0;
		private static readonly float Scale = 0.005f;
		private static readonly float DetailScale = 0.1f;
		private static float continetalnessScale;
		private static float erosionScale;
		private static float peakScale;

		/// <summary>
		/// Pass seed to perlen noices
		/// </summary>
		/// <param name="seeder"></param>
		internal static void Seed(int seeder)
		{
			seed = seeder;
			Noise.Seed = seed;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="seeder"></param>
		/// <param name="c">Scale for continentalness</param>
		/// <param name="e">Scale for erosions</param>
		/// <param name="pv">Scale for Peaks nad valleys</param>
		internal static void Seed(int seeder, float c, float e, float pv)
		{
			seed = seeder;
			continetalnessScale = c;
			erosionScale = e;
			peakScale = pv;
			Noise.Seed = seed;
		}
		/// <summary>
		/// Main static method for finding blocks
		/// </summary>
		/// <param name="x">coord X</param>
		/// <param name="y">coord Y</param>
		/// <param name="z">coord Z</param>
		/// <returns>BlockType on coords</returns>
		internal static BlockType Find(int x, int y, int z)
		{
			if (y == 0)
			{
				return BlockType.Bedrock;
			}

			int terrainHeight = GenerateTerrainHeight(x, z);

			BiomeType biome = GenerateBiome(x, z);

			int dirtHeight = GenerateDirtHeight(x, z);

			if (dirtHeight != 0 && y == terrainHeight)
			{
				return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.SurfaceType];
			}

			if (terrainHeight - dirtHeight <= y && terrainHeight > y)
			{
				return GenerateSurfaceLayerType(x, z, biome);
			}

			if (y < terrainHeight)
			{
				return GenerateUnderGroundLayer(x, z, y);
			}

			if (terrainHeight < WorldConst.WATER && y < WorldConst.WATER)
			{
				return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.WaterType];
			}

			if (y + 1 == terrainHeight)
			{
				var isTree = GenerateThreeOnSurface(x, z, biome);
				if (isTree == BlockType.Empty)
					return isTree != BlockType.Empty ? isTree : GenerateFirstSurfaceBlock(x, z, biome);
			}
			if (y + 2 == terrainHeight || y + 3 == terrainHeight)
			{
				return GenerateThreeOnSurface(x, z, biome);
			}
			return BlockType.Empty;
		}
		#region Addision methods
		/// <summary>
		/// Draw all blocks every single block from BlockType  -> infinity
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <returns></returns>
		internal static BlockType ShowAllBlocks(int x, int y, int z)
		{
			var enums = Enum.GetValues<BlockType>();

			if (z != 0 || y != 0)
			{
				return BlockType.Empty;
			}
			if (x % 2 != 0)
				return BlockType.Empty;
			if (x / 2 >= enums.Length || x < 0)
				return BlockType.Empty;
			return enums[x / 2];
		}


		internal static BlockType GenOnlyTerrainHeight(int x, int y, int z)
		{
			int height = GenerateTerrainHeight(x, z);
			if (height >= y)
			{
				return BlockType.Grass;
			}
			else
			{
				return BlockType.Empty;
			}
		}
		#endregion

		#region private funcs
		private static int GenerateTerrainHeight(int x, int z)
		{
			int Height;
			if (terrainHeight.TryGetValue((x, z), out Height))
			{
				return Height;
			}
			float cont = Noise.CalcPixel2D(x, z, continetalnessScale) / 256f;
			float eros = Noise.CalcPixel2D(x, z, erosionScale) / 256f;
			float peak = Noise.CalcPixel2D(x, z, peakScale) / 256f;

			float contintalness = CardinalCollections.Continentalness.GetValue(cont);
			float erosion = CardinalCollections.Erosion.GetValue(eros);
			float PV = CardinalCollections.Peak.GetValue(peak);

			float heightScale = (contintalness + erosion + PV) / 3;
			Height = (int)(WorldConst.HIGH * heightScale);

			terrainHeight.Add((x, z), Height);

			return Height;

		}
		private static BiomeType GenerateBiome(int x, int z)
		{
			return BiomeType.Forest;
		}
		private static int GenerateDirtHeight(int x, int z)
		{
			return 3;
		}
		private static BlockType GenerateSurfaceLayerType(int x, int z, BiomeType biome)
		{
			return BlockType.Dirt;
		}
		private static BlockType GenerateUnderGroundLayer(int x, int y, int z)
		{
			return BlockType.Stone;
		}
		private static BlockType GenerateThreeOnSurface(int x, int z, BiomeType biome)
		{
			return BlockType.Empty;
		}

		private static BlockType GenerateFirstSurfaceBlock(int x, int y, BiomeType biome)
		{
			return BlockType.Empty;
		}
		#endregion
	}
}
