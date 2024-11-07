using Hiscraft.Entities;
using Hiscraft.WorldModels;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.Helpers
{
	/// <summary>
	/// static class provides methods that help using files
	/// </summary>
	internal static class FileHelper
	{
		/// <summary>
		/// GetUVsFromBook is cutting out required texture from texture book
		/// </summary>
		/// <param name="coords"></param>
		/// <returns></returns>
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
		/// <summary>
		/// GetTexturePat add to texture name path
		/// </summary>
		/// <param name="name">texture name</param>
		/// <returns>texture full path</returns>
		internal static string GetTexturePath(string name)
		{

			return WorldConst.texturesPathDEBUG + name;
		}
		/// <summary>
		/// GetShaderPath add to shader name path
		/// </summary>
		/// <param name="name">shader name</param>
		/// <returns>shader full path</returns>
		internal static string GetShaderPath(string name)
		{
			return WorldConst.ShadersPathDEBUG + name;
		}
	}
}
