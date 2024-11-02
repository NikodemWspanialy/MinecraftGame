
using OpenTK.Mathematics;
using Hiscraft.Entities;

namespace Hiscraft.Resources.Textures
{
    internal static class TextureData
    {
        public static Dictionary<BlockType, Dictionary<Faces, Vector2>> blockTypeUVCoord = new Dictionary<BlockType, Dictionary<Faces, Vector2>>()
        {
            {BlockType.Dirt, new Dictionary<Faces, Vector2>()
                {
                    {Faces.FRONT, new Vector2(2f, 15f) },
                    {Faces.LEFT, new Vector2(2f, 15f) },
                    {Faces.RIGHT, new Vector2(2f, 15f) },
                    {Faces.BACK, new Vector2(2f, 15f) },
                    {Faces.TOP, new Vector2(2f, 15f) },
                    {Faces.BOTTOM, new Vector2(2f, 15f) },
                }
            },
            {BlockType.Stone, new Dictionary<Faces, Vector2>()
                {
                    {Faces.FRONT, new Vector2(1f, 15f) },
                    {Faces.LEFT, new Vector2(1f, 15f) },
                    {Faces.RIGHT, new Vector2(1f, 15f) },
                    {Faces.BACK, new Vector2(1f, 15f) },
                    {Faces.TOP, new Vector2(1f, 15f) },
                    {Faces.BOTTOM, new Vector2(1f, 15f) },
                }
            },
            {BlockType.Empty, new Dictionary<Faces, Vector2>()
                {
                    {Faces.FRONT, new Vector2(3f, 15f) },
                    {Faces.LEFT, new Vector2(3f, 15f) },
                    {Faces.RIGHT, new Vector2(3f, 15f) },
                    {Faces.BACK, new Vector2(3f, 15f) },
                    {Faces.TOP, new Vector2(7f, 13f) },
                    {Faces.BOTTOM, new Vector2(3f, 15f) },
                }
            }
        };
    }
}
