using Hiscraft.Entities;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.Helpers
{
	internal static class FileHelper
	{
		internal static Dictionary<Faces, List<Vector2>> GetUVsFromBook(Dictionary<Faces, Vector2> coords)
		{
			Dictionary<Faces, List<Vector2>> faceData = new Dictionary<Faces, List<Vector2>>();

			foreach (var faceCoord in coords)
			{
				faceData[faceCoord.Key] = new List<Vector2>()
				{
					new Vector2((faceCoord.Value.X+1f)/16f, (faceCoord.Value.Y+1f)/16f),
					new Vector2(faceCoord.Value.X/16f, (faceCoord.Value.Y+1f)/16f),
					new Vector2(faceCoord.Value.X/16f, faceCoord.Value.Y/16f),
					new Vector2((faceCoord.Value.X+1f)/16f, faceCoord.Value.Y/16f),
				};
			}

			return faceData;
		}
		internal static string GetTexturePath(string name)
		{
			return "../../../Resources/Textures/" + name;
		}
		internal static string GetShaderPath(string name)
		{
			return "../../../Resources/Shaders/" + name;
		}
	}
}
