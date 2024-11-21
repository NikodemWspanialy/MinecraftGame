using Hiscraft.Entities.BlockTypeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.Entities.BiomeTypeEntities
{
	internal static class BiomeTypeInfo
	{
		internal static readonly Dictionary<BiomeType, Dictionary<BiomeBlockType, BlockType>> BiomesDefinedBlocks = new Dictionary<BiomeType, Dictionary<BiomeBlockType, BlockType>>
		{
			{
				BiomeType.Forest,
				new()
				{
					{ BiomeBlockType.SurfaceType, BlockType.Grass },
					{ BiomeBlockType.TreeType, BlockType.Trunk },
					{ BiomeBlockType.LeavesType, BlockType.Leaves },
					{ BiomeBlockType.SpecialBlockType, BlockType.Pumpkin},
					{ BiomeBlockType.WaterType, BlockType.Water},
				}
			},
			{
				BiomeType.Polar,
				new()
				{
					{ BiomeBlockType.SurfaceType, BlockType.Snow },
					{ BiomeBlockType.TreeType, BlockType.DarkTrunk },
					{ BiomeBlockType.LeavesType, BlockType.SnowLeaves },
					{ BiomeBlockType.SpecialBlockType, BlockType.Pumpkin},
					{ BiomeBlockType.WaterType, BlockType.Ice},
				}
			},
			{
				BiomeType.Desert,
				new()
				{
					{ BiomeBlockType.SurfaceType, BlockType.Sand },
					{ BiomeBlockType.TreeType, BlockType.Cactus },
					{ BiomeBlockType.LeavesType, BlockType.Empty },
					{ BiomeBlockType.SpecialBlockType, BlockType.Empty},
					{ BiomeBlockType.WaterType, BlockType.Water},
				}
			},
			{
				BiomeType.Savanna,
				new()
				{
					{ BiomeBlockType.SurfaceType, BlockType.SavannaGrass},
					{ BiomeBlockType.TreeType, BlockType.SavannaTrunk },
					{ BiomeBlockType.LeavesType, BlockType.SavannaLeaves },
					{ BiomeBlockType.WaterType, BlockType.Water},
					{ BiomeBlockType.SpecialBlockType, BlockType.Pumpkin},
				}
			}
		};
	}
}
