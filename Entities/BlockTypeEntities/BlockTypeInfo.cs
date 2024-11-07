using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.Entities.BlockTypeEntities
{
	internal static class BlockTypeInfo
	{

		internal static List<BlockType> noCoveringBlocks = new()
		{
			BlockType.Empty,
		};
		internal static List<BlockType> alwaysDrawBlocks = new()
		{
			BlockType.Coal,
			BlockType.Diamond,
		};
	}
}
