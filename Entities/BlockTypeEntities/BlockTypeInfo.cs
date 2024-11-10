namespace Hiscraft.Entities.BlockTypeEntities
{
	internal static class BlockTypeInfo
	{

		internal static List<BlockType> noCoveringBlocks = new()
		{
			BlockType.Empty,
			BlockType.SemiGrass,
		};
		internal static List<BlockType> alwaysDrawBlocks = new()
		{
			BlockType.Coal,
			BlockType.Diamond,
		};
	}
}
