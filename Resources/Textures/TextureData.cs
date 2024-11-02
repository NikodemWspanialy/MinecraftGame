
using OpenTK.Mathematics;
using Hiscraft.Entities;

namespace Hiscraft.Resources.Textures
{
    internal static class TextureData
    {
        public static Dictionary<BlockType, Dictionary<FacesEnum, Vector2>> blocksUV = new Dictionary<BlockType, Dictionary<FacesEnum, Vector2>>()
        {
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
