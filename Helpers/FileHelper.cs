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
		internal static Dictionary<FacesEnum, List<Vector2>> GetUVsFromBook(Dictionary<FacesEnum, Vector2> coords)
		{
			Dictionary<FacesEnum, List<Vector2>> faceUV = new Dictionary<FacesEnum, List<Vector2>>();

			foreach (var faceCoord in coords)
			{
				faceUV[faceCoord.Key] = new List<Vector2>()
				{
					new Vector2((faceCoord.Value.X+1f)/16f, (faceCoord.Value.Y+1f)/16f),
					new Vector2(faceCoord.Value.X/16f, (faceCoord.Value.Y+1f)/16f),
					new Vector2(faceCoord.Value.X/16f, faceCoord.Value.Y/16f),
					new Vector2((faceCoord.Value.X+1f)/16f, faceCoord.Value.Y/16f),
				};
			}

			return faceUV;
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
