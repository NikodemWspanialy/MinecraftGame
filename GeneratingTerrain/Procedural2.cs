using Hiscraft.Entities.BiomeTypeEntities;
using Hiscraft.Entities.BlockTypeEntities;
using Hiscraft.WorldModels;
using SimplexNoise;

namespace Hiscraft.GeneratingTerrain
{
	internal class Procedural2
	{
        public Procedural2()
        {
			Noise.Seed = WorldConst.SEED;
        }
        internal static BlockType Find(int x, int y, int z)
		{
			if(y == 0)
			{
				return BlockType.Bedrock;
			}
			
			int terrainHeight = GenerateTerrainHeight(x, z);
			
			BiomeType biome = GenerateBiome(x, z);

			int dirtHeight = GenerateDirtHeight(x, z);

			if(dirtHeight != 0 && y == terrainHeight)
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
			
			if(terrainHeight < WorldConst.HIGH / 3 && y < WorldConst.HIGH)
			{
				return BiomeTypeInfo.BiomesDefinedBlocks[biome][BiomeBlockType.WaterType];
			}
			
			if(y + 1 == terrainHeight)
			{
				var isTree = GenerateThreeOnSurface(x, z, biome);
				if(isTree == BlockType.Empty)
				return isTree != BlockType.Empty ? isTree : GenerateFirstSurfaceBlock(x,z,biome);
			}
			if(y + 2 == terrainHeight || y + 3 == terrainHeight)
			{
				return GenerateThreeOnSurface(x, z, biome);
			}
			return BlockType.Empty;
		}

		internal static BlockType ShowAllBlocks(int x, int y, int z)
		{
			var enums = Enum.GetValues<BlockType>();

			if(z != 0 || y != 0){
				return BlockType.Empty;
			}
			if (x % 2 != 0) 
				return BlockType.Empty;
			if (x / 2 >= enums.Length || x < 0)
				return BlockType.Empty;
			return enums[x / 2];
		}
		#region private funcs
		private static int GenerateTerrainHeight(int x, int z)
		{
			return WorldConst.HIGH / 2;
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
