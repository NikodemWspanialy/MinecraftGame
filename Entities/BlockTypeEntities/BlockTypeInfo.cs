namespace Hiscraft.Entities.BlockTypeEntities
{
	/// <summary>
	/// Class handling all block types information.
	/// </summary>
	internal static class BlockTypeInfo
	{
		/// <summary>
		/// Static list of block, which is not covering all block.
		/// </summary>
		internal static List<BlockType> noCoveringBlocks = new()
		{
			BlockType.Empty,
			BlockType.SemiGrass,
			BlockType.Cactus,
		};

		/// <summary>
		/// Static list of special blocks, that should be draw in every case
		/// </summary>
		internal static List<BlockType> alwaysDrawBlocks = new()
		{
			BlockType.Coal,
			BlockType.Diamond,
			BlockType.Gold,
			BlockType.Redstone,
		};

		/// <summary>
		/// Static dictionary, where block type size is defined.
		/// </summary>
		internal static Dictionary<BlockType, BlockSizeClassifier> BlockSizeCollections = new Dictionary<BlockType, BlockSizeClassifier>()
		{
			{BlockType.SemiGrass, BlockSizeClassifier.Semi },
			{BlockType.Cactus, BlockSizeClassifier.Cactus },
			{BlockType.Dirt, BlockSizeClassifier.Normal},
			{BlockType.Stone, BlockSizeClassifier.Normal},
			{BlockType.Bedrock, BlockSizeClassifier.Normal },
			{BlockType.Grass, BlockSizeClassifier.Normal },
			{BlockType.Water, BlockSizeClassifier.Normal },
			{BlockType.Sand, BlockSizeClassifier.Normal },
			{BlockType.Snow, BlockSizeClassifier.Normal },
			{BlockType.Coal, BlockSizeClassifier.Normal },
			{BlockType.Diamond, BlockSizeClassifier.Normal },
			{BlockType.Pumpkin, BlockSizeClassifier.Normal },
			{BlockType.Trunk, BlockSizeClassifier.Normal },
			{BlockType.Leaves, BlockSizeClassifier.Normal },
			{BlockType.DarkTrunk, BlockSizeClassifier.Normal },
			{BlockType.SnowLeaves, BlockSizeClassifier.Normal },
			{BlockType.Ice, BlockSizeClassifier.Normal},
			{BlockType.SavannaTrunk, BlockSizeClassifier.Normal },
			{BlockType.SavannaLeaves, BlockSizeClassifier.Normal },
			{BlockType.Gold, BlockSizeClassifier.Normal },
			{BlockType.Iron, BlockSizeClassifier.Normal },
			{BlockType.Redstone, BlockSizeClassifier.Normal },
			{BlockType.Empty, BlockSizeClassifier.Normal },
			{BlockType.SavannaGrass, BlockSizeClassifier.Normal },
		};
	}
}
