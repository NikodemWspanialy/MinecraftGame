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
					{ BiomeBlockType.SurfaceType, BlockType.Dirt },
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
					{ BiomeBlockType.TreeType, BlockType.Trunk },
					{ BiomeBlockType.LeavesType, BlockType.Leaves },
					{ BiomeBlockType.SpecialBlockType, BlockType.Pumpkin},
					{ BiomeBlockType.WaterType, BlockType.Water},
				}
			},
			{
				BiomeType.Desert,
				new()
				{
					{ BiomeBlockType.SurfaceType, BlockType.Sand },
					{ BiomeBlockType.TreeType, BlockType.Trunk },
					{ BiomeBlockType.LeavesType, BlockType.Empty },
					{ BiomeBlockType.SpecialBlockType, BlockType.Pumpkin},
					{ BiomeBlockType.WaterType, BlockType.Water},
				}
			},
			{
				BiomeType.Savanna,
				new()
				{
					{ BiomeBlockType.SurfaceType, BlockType.Coal},
					{ BiomeBlockType.TreeType, BlockType.Trunk },
					{ BiomeBlockType.LeavesType, BlockType.Leaves },
					{ BiomeBlockType.WaterType, BlockType.Water},
					{ BiomeBlockType.SpecialBlockType, BlockType.Pumpkin},
				}
			}
		};
	}
}
