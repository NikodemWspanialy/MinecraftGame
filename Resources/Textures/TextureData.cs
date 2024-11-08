using OpenTK.Mathematics;
using Hiscraft.Entities;
using Hiscraft.Entities.BlockTypeEntities;

namespace Hiscraft.Resources.Textures
{
    /// <summary>
    /// static class that hadle texture data
    /// </summary>
    internal static class TextureData
    {
        /// <summary>
        /// blockUV keeps uvs for every block type
        /// </summary>
        public static Dictionary<BlockType, Dictionary<FacesEnum, Vector2>> blocksUV = new Dictionary<BlockType, Dictionary<FacesEnum, Vector2>>()
        {
			{BlockType.Bedrock, new Dictionary<FacesEnum, Vector2>()
				{
					{FacesEnum.FRONT, new Vector2(1f, 14f) },
					{FacesEnum.LEFT, new Vector2(1f, 14f) },
					{FacesEnum.RIGHT, new Vector2(1f, 14f) },
					{FacesEnum.BACK, new Vector2(1f, 14f) },
					{FacesEnum.TOP, new Vector2(1f, 14f) },
					{FacesEnum.BOTTOM, new Vector2(1f, 14f) },
				}
			},
			{BlockType.Dirt, new Dictionary<FacesEnum, Vector2>()
                {
                    {FacesEnum.FRONT, new Vector2(2f, 15f) },
                    {FacesEnum.LEFT, new Vector2(2f, 15f) },
                    {FacesEnum.RIGHT, new Vector2(2f, 15f) },
                    {FacesEnum.BACK, new Vector2(2f, 15f) },
                    {FacesEnum.TOP, new Vector2(2f, 15f) },
                    {FacesEnum.BOTTOM, new Vector2(2f, 15f) },
                }
            },
			{BlockType.Grass, new Dictionary<FacesEnum, Vector2>()
				{
					{FacesEnum.FRONT, new Vector2(3f, 15f) },
					{FacesEnum.LEFT, new Vector2(3f, 15f) },
					{FacesEnum.RIGHT, new Vector2(3f, 15f) },
					{FacesEnum.BACK, new Vector2(3f, 15f) },
					{FacesEnum.TOP, new Vector2(7f, 13f) },
					{FacesEnum.BOTTOM, new Vector2(2f, 15f) },
				}
			},
			{BlockType.Water, new Dictionary<FacesEnum, Vector2>()
				{
					{FacesEnum.FRONT, new Vector2(14f, 15f) },
					{FacesEnum.LEFT, new Vector2(14f, 15f) },
					{FacesEnum.RIGHT, new Vector2(14f, 15f) },
					{FacesEnum.BACK, new Vector2(14f, 15f) },
					{FacesEnum.TOP, new Vector2(14f, 15f) },
					{FacesEnum.BOTTOM, new Vector2(14f, 15f) },
				}
			},
			{BlockType.Pumpkin, new Dictionary<FacesEnum, Vector2>()
                {
                    {FacesEnum.FRONT, new Vector2(6f, 8f) },
                    {FacesEnum.LEFT, new Vector2(6f, 8f) },
                    {FacesEnum.RIGHT, new Vector2(6f, 8f) },
                    {FacesEnum.BACK, new Vector2(6f, 8f) },
                    {FacesEnum.TOP, new Vector2(6f, 9f) },
                    {FacesEnum.BOTTOM, new Vector2(6f, 8f) },
                }
            },
			{BlockType.Coal, new Dictionary<FacesEnum, Vector2>()
				{
					{FacesEnum.FRONT, new Vector2(2f, 13f) },
					{FacesEnum.LEFT, new Vector2(2f, 13f) },
					{FacesEnum.RIGHT, new Vector2(2f, 13f) },
					{FacesEnum.BACK, new Vector2(2f, 13f) },
					{FacesEnum.TOP, new Vector2(2f, 13f) },
					{FacesEnum.BOTTOM, new Vector2(2f, 13f) },
				}
			},
			{BlockType.Diamond, new Dictionary<FacesEnum, Vector2>()
				{
					{FacesEnum.FRONT, new Vector2(2f, 12f) },
					{FacesEnum.LEFT, new Vector2(2f, 12f) },
					{FacesEnum.RIGHT, new Vector2(2f, 12f) },
					{FacesEnum.BACK, new Vector2(2f, 12f) },
					{FacesEnum.TOP, new Vector2(2f, 12f) },
					{FacesEnum.BOTTOM, new Vector2(2f, 12f) },
				}
			},
			{BlockType.Snow, new Dictionary<FacesEnum, Vector2>()
				{
					{FacesEnum.FRONT, new Vector2(4f, 11f) },
					{FacesEnum.LEFT, new Vector2(4f, 11f) },
					{FacesEnum.RIGHT, new Vector2(4f, 11f) },
					{FacesEnum.BACK, new Vector2(4f, 11f) },
					{FacesEnum.TOP, new Vector2(2f, 11f) },
					{FacesEnum.BOTTOM, new Vector2(2f, 15f) },
				}
			},
			{BlockType.Sand, new Dictionary<FacesEnum, Vector2>()
				{
					 {FacesEnum.FRONT, new Vector2(2f, 14f) },
					{FacesEnum.LEFT, new Vector2(2f, 14f) },
					{FacesEnum.RIGHT, new Vector2(2f, 14f) },
					{FacesEnum.BACK, new Vector2(2f, 14f) },
					{FacesEnum.TOP, new Vector2(2f, 14f) },
					{FacesEnum.BOTTOM, new Vector2(2f, 14f) },
				}
			},
			{BlockType.Stone, new Dictionary<FacesEnum, Vector2>()
				{
					 {FacesEnum.FRONT, new Vector2(1f, 15f) },
					{FacesEnum.LEFT, new Vector2(1f, 15f) },
					{FacesEnum.RIGHT, new Vector2(1f, 15f) },
					{FacesEnum.BACK, new Vector2(1f, 15f) },
					{FacesEnum.TOP, new Vector2(1f, 15f) },
					{FacesEnum.BOTTOM, new Vector2(1f, 15f) },
				}
			},
			{BlockType.Empty, new Dictionary<FacesEnum, Vector2>()
                {
                    {FacesEnum.FRONT, new Vector2(3f, 15f) },
                    {FacesEnum.LEFT, new Vector2(3f, 15f) },
                    {FacesEnum.RIGHT, new Vector2(3f, 15f) },
                    {FacesEnum.BACK, new Vector2(3f, 15f) },
                    {FacesEnum.TOP, new Vector2(7f, 13f) },
                    {FacesEnum.BOTTOM, new Vector2(3f, 15f) },
                }
            }
        };
    }
}
