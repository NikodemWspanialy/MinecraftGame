using Hiscraft.Entities.BiomeTypeEntities;
using Hiscraft.Entities.BlockTypeEntities;
using Hiscraft.GeneratingTerrain.Collections;
using OpenTK.Mathematics;
using SimplexNoise;

namespace Hiscraft.GeneratingTerrain
{
	/// <summary>
	/// Class which is rensonse for procedural world generating.
	/// </summary>
	internal class ProceduralGeneration
	{
		/// <summary>
		/// Dictionary od terrain heigh for speed up calculating.
		/// </summary>
		private static Dictionary<Vector2, int> terrainHeight = [];

		/// <summary>
		/// Seed for world, make it original for every world passed by user.
		/// </summary>
		private static int seed;

		/// <summary>
		/// Scale of use and impact of continentalness in generating by user.
		/// </summary>
		private static float continetalnessScale;

		/// <summary>
		/// Scale of use and impact of erosion in generating by user.
		/// </summary>
		private static float erosionScale;

		/// <summary>
		/// Scale of use and impact of peak and valley in generating by user.
		/// </summary>
		private static float peakScale;

		/// <summary>
		/// Scale of tree generation density by user.
		/// </summary>
		private static float treeScale;

		/// <summary>
		/// Scale of other elements generating on the world surface by user.
		/// </summary>
		private static float congestionScale;

		/// <summary>
		/// World high by user.
		/// </summary>
		private static int high;

		/// <summary>
		/// Scale of water generating by user.
		/// </summary>
		private static int waterScale;

		/// <summary>
		/// Scale of natural resources generating underground by user.
		/// </summary>
		private static int naturalResources;

		/// <summary>
		/// Private encalculated continentalness offset.
		/// </summary>
		private static int continentalnessOffset = 1;

		/// <summary>
		/// Private encalculated erosion offset.
		/// </summary>
		private static int erosionOffset = 2;

		/// <summary>
		/// Private encalculated peak and valley offset.
		/// </summary>
		private static int peakOffset = 3;

		/// <summary>
		/// Private encalculated dirt level offset.
		/// </summary>
		private static int dirtLevelOffset = 4;

		/// <summary>
		/// Private encalculated biome offset.
		/// </summary>
		private static int biomeOffset = 5;

		/// <summary>
		/// Private encalculated entiy offset.
		/// </summary>
		private static int entityOffset = 6;

		/// <summary>
		/// Private encalculated diamond offset.
		/// </summary>
		private static int diamondOffset = 7;

		/// <summary>
		/// Private encalculated gold offset.
		/// </summary>
		private static int goldOffset = 8;

		/// <summary>
		/// Private encalculated redstone offset.
		/// </summary>
		private static int redstoneOffset = 9;

		/// <summary>
		/// seed all values, nust be called before Genering blocks.
		/// </summary>
		/// <param name="seeder"></param>
		/// <param name="continetalness">Scale for continentalness</param>
		/// <param name="erosion">Scale for erosions</param>
		/// <param name="peakAndValley">Scale for Peaks nad valleys</param>
		/// <param name="tree">Scale for trees</param>
		/// <param name="details">Scale for other details</param>
		/// <param name="highLevel"> max render level</param>
		/// <param name="water">water rate</param>
		/// <param name="naturalResourcesRate">natural resources rate</param>
		internal static void Prepare(int seeder = 0, float continetalness = 0.0045f, float erosion = 0.0016f, float peakAndValley = 0.01f, float tree = 0.01f, float details = 0.01f, int highLevel = 64, int water = 12, int naturalResourcesRate = 5)
		{
			seed = seeder;
			continetalnessScale = continetalness;
			erosionScale = erosion;
			peakScale = peakAndValley;
			treeScale = tree;
			high = highLevel;
			ProceduralGeneration.waterScale = water;
			congestionScale = details;
			naturalResources = naturalResourcesRate;
			Noise.Seed = seed;

			continentalnessOffset *= seed;
			erosionOffset *= seed;
			peakOffset *= seed;
			dirtLevelOffset *= seed;
			biomeOffset *= seed;
			entityOffset *= seed;
			diamondOffset *= seed;
			goldOffset *= seed;
			redstoneOffset *= seed;
		}

		/// <summary>
		/// Main static method for finding blocks.
		/// </summary>
		/// <param name="x">coord X</param>
		/// <param name="y">coord Y</param>
		/// <param name="z">coord Z</param>
		/// <returns>BlockType on passed coords</returns>
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
				return GenerateSurface(x, z, biome);
				return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.SurfaceType];
			}

			if (terrainHeight - dirtHeight <= y && terrainHeight > y)
			{
				return BlockType.Dirt;
			}

			if (y < terrainHeight)
			{
				return GenerateUnderGroundLayer(x, z, y);
			}

			return GenerateEntitiesOnSurface(x, y, z, terrainHeight, biome);
		}

		#region Additional methods
		/// <summary>
		/// Draw all blocks every single block from BlockType  -> infinity.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <returns>BlockType on passed coords</returns>
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

		/// <summary>
		/// Show terrain HiGh algorithm, draw only stone and grass on top layer .
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <returns>BlockType on passed coords</returns>
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

		#region private functions
		/// <summary>
		/// Generain terrain height calculated from param. Calculated with noises functions
		/// </summary>
		/// <param name="x">Coord X</param>
		/// <param name="z">Coord Z</param>
		/// <returns>Height of world in passed coordinates</returns>
		private static int GenerateTerrainHeight(int x, int z)
		{
			int Height;
			if (terrainHeight.TryGetValue((x, z), out Height))
			{
				return Height;
			}
			float cont = Noise.CalcPixel2D(x + continentalnessOffset, z - continentalnessOffset, continetalnessScale) / 256f;
			float eros = Noise.CalcPixel2D(x + erosionOffset, z - erosionOffset, erosionScale) / 256f;
			float peak = Noise.CalcPixel2D(x + peakOffset, z - peakOffset, peakScale) / 256f;

			float contintalness = CardinalCollections.Continentalness.GetValue(cont);
			float erosion = CardinalCollections.Erosion.GetValue(eros);
			float PV = CardinalCollections.Peak.GetValue(peak);

			float heightScale = (contintalness + erosion + PV) / 3;
			Height = (int)(high * heightScale);

			terrainHeight.Add((x, z), Height);

			return Height;

		}
		/// <summary>
		/// Generate biome, calculating base on noises.
		/// </summary>
		/// <param name="x">Coord X</param>
		/// <param name="z">Coord Z</param>
		/// <returns>Biome type in passed coordinates</returns>
		private static BiomeType GenerateBiome(int x, int z)
		{
			float noise = Noise.CalcPixel2D(x + biomeOffset, z - biomeOffset, 0.0001f) / 256f;
			switch (noise)
			{
				case < 0.20f:
					return BiomeType.Desert;
				case < 0.5f:
					return BiomeType.Forest;
				case < 0.7f:
					return BiomeType.Savanna;
				default:
					return BiomeType.Polar;

			}

		}

		/// <summary>
		/// Generate top levels of world. Base on noises.
		/// </summary>
		/// <param name="x">Coord X</param>
		/// <param name="z">Coord Z</param>
		/// <param name="biome">Type of biome</param>
		/// <returns>Block type calculated, specify for passed coordinates and biome type</returns>
		private static BlockType GenerateSurface(int x, int z, BiomeType biome)
		{

			float peak = Noise.CalcPixel2D(x, z, peakScale) / 256f;
			float PV = CardinalCollections.Peak.GetValue(peak);

			if (PV <= (0.2f*(waterScale/10f)))
			{
				return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.WaterType];
			}
			else
			{
				return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.SurfaceType];
			}
		}

		/// <summary>
		/// Generating number of top levels dirt layers.
		/// </summary>
		/// <param name="x">Coord X</param>
		/// <param name="z">Coord Z</param>
		/// <returns>number of top levels dirt layers</returns>
		private static int GenerateDirtHeight(int x, int z)
		{
			return (int)Math.Floor(Noise.CalcPixel2D(x + dirtLevelOffset, z - dirtLevelOffset, 0.05f) / 256f * 7);
		}

		/// <summary>
		/// Genenera underground world, based on noises.
		/// </summary>
		/// <param name="x">Coord X</param>
		/// <param name="y">Coord Y</param>
		/// <param name="z">Coord Z</param>
		/// <returns>Block type calculated from coordinates</returns>
		private static BlockType GenerateUnderGroundLayer(int x, int y, int z)
		{
			var value = Noise.CalcPixel3D(x, y, z, 0.02f) / 256f;
			if(value < 0.15f)
			{
				return BlockType.Empty;
			}
			value = Noise.CalcPixel3D(x + diamondOffset, y - diamondOffset, z, 0.1f)/256f;
			
			if(value > (0.9788f - naturalResources/1000f))
			{
				return BlockType.Diamond;
			}

			value = Noise.CalcPixel3D(x + goldOffset, y - goldOffset, z, 0.1f) / 256f;
			if(value > (0.9777f - naturalResources / 1000f))
			{
				return BlockType.Gold;
			}
			value = Noise.CalcPixel3D(x + redstoneOffset, y - redstoneOffset, z, 0.1f) / 256f;
			if (value > (0.9767f - naturalResources / 1000f))
			{
				return BlockType.Redstone;
			}
			value = Noise.CalcPixel3D(x , y , z, 0.05f) / 256f;
			if (value > (0.9267f - naturalResources / 100f))
			{
				return BlockType.Coal;
			}
			return BlockType.Stone;
		}

		/// <summary>
		/// Generating special blocks on wirld surface.
		/// </summary>
		/// <param name="x">Coord X</param>
		/// <param name="y">Coord Y</param>
		/// <param name="z">Coord Z</param>
		/// <param name="terrainHeight"> High of world</param>
		/// <param name="biome">iome in passed coords</param>
		/// <returns>Block type for coordinates, calculated with noises.</returns>
		private static BlockType GenerateEntitiesOnSurface(int x, int y, int z, int terrainHeight, BiomeType biome)
		{
			if (y - 1 == terrainHeight)
			{
				var isTree = GenerateThreeOnSurface(x, z, biome);
				return isTree != BlockType.Empty ? isTree : GenerateFirstSurfaceBlock(x, z, biome);
			}
			if (y - 2 == terrainHeight || y - 3 == terrainHeight)
			{
				return GenerateThreeOnSurface(x, z, biome);
			}
			if (y - 4 == terrainHeight)
			{

				var isTree = GenerateThreeOnSurface(x, z, biome);
				if (isTree != BlockType.Empty)
				{
					return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.TreeType];
				}
				isTree = GenerateThreeOnSurface(x + 1, z, biome);
				if (isTree != BlockType.Empty)
				{
					return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.LeavesType];
				}
				isTree = GenerateThreeOnSurface(x - 1, z, biome);
				if (isTree != BlockType.Empty)
				{
					return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.LeavesType];
				}
				isTree = GenerateThreeOnSurface(x, z + 1, biome);
				if (isTree != BlockType.Empty)
				{
					return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.LeavesType];
				}
				isTree = GenerateThreeOnSurface(x, z - 1, biome);
				if (isTree != BlockType.Empty)
				{
					return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.LeavesType];
				}
			}
			if (y - 5 == terrainHeight)
			{
				var isTree = GenerateThreeOnSurface(x, z, biome);
				if (isTree != BlockType.Empty)
				{
					return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.LeavesType];
				}
			}

			return BlockType.Empty;
		}

		/// <summary>
		/// Generating tree on world. Calculating bases on noises.
		/// </summary>
		/// <param name="x">Coord X</param>
		/// <param name="z">Coord Z</param>
		/// <param name="biome">Biome type</param>
		/// <returns>Block type calculated for coordinates.</returns>
		private static BlockType GenerateThreeOnSurface(int x, int z, BiomeType biome)
		{
			int value = (int)(100 * (Math.Round(Noise.CalcPixel2D(x, z, 0.5f) / 256f, 2)));
			if (value == 10)
			{
				return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.TreeType];
			}
			else
			{
				return BlockType.Empty;
			}
		}

		/// <summary>
		/// Generate top level of ground. Base on noises. Finding water.
		/// </summary>
		/// <param name="x">Coord X</param>
		/// <param name="z">Coord Z</param>
		/// <param name="biome">Biome type</param>
		/// <returns>Block type calculated for coordinates.</returns>
		private static BlockType GenerateFirstSurfaceBlock(int x, int z, BiomeType biome)
		{
			int value = (int)(100 * (Math.Round(Noise.CalcPixel2D(x + entityOffset, z - entityOffset, 0.5f) / 256f, 2)));
			if (value == 8 && BlockType.Water != GenerateSurface(x, z, biome))
			{
					return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.SpecialBlockType];
			}
			else
			{
				return BlockType.Empty;
			}
		}
		#endregion
	}
}
