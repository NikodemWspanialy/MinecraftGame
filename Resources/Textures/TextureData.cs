
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
