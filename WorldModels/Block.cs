using Hiscraft.Entities;
using OpenTK.Mathematics;
using Hiscraft.Helpers;
using Hiscraft.Resources.Textures;

namespace Hiscraft.WorldModels
{
	/// <summary>
	/// Block class represents a block  entity, it is hangle by chunk
	/// zblock keeps it own position, texture uv and faces vertices
	/// </summary>
	internal class Block
	{
		#region Private fields
		/// <summary>
		/// block unique position
		/// </summary>
		private Vector3 position;
		/// <summary>
		/// block type from enum
		/// </summary>
		private BlockType type;
		/// <summary>
		/// faces handle all faces (6) with its own position (transformed by position) and uv coord from texture book
		/// </summary>
		private Dictionary<FacesEnum, Face> faces;
		#endregion

		#region Constructor
		/// <summary>
		/// Only one ctor for Block class
		/// </summary>
		/// <param name="position"> blocks unique position</param>
		/// <param name="blockType">block type from enum</param>
		public Block(Vector3 position, BlockType blockType = BlockType.Empty)
		{
			type = blockType;
			this.position = position;

			if (blockType != BlockType.Empty)
			{
				var blockUV = FileHelper.GetUVsFromBook(TextureData.blocksUV[blockType]);

				faces = new()
				{
					{FacesEnum.FRONT, new Face {
						vertices = MoveToPosition(FacesVertexCollections.BlocksVertexCollection[FacesEnum.FRONT]),
						uv = blockUV[FacesEnum.FRONT]
						}
					},
					{FacesEnum.BACK, new Face {
						vertices = MoveToPosition(FacesVertexCollections.BlocksVertexCollection[FacesEnum.BACK]),
						uv = blockUV[FacesEnum.BACK]
						}
					},
					{FacesEnum.LEFT, new Face {
					vertices = MoveToPosition(FacesVertexCollections.BlocksVertexCollection[FacesEnum.LEFT]),
					uv = blockUV[FacesEnum.LEFT]
						} 
					},
					{FacesEnum.RIGHT, new Face {
					vertices = MoveToPosition(FacesVertexCollections.BlocksVertexCollection[FacesEnum.RIGHT]),
					uv = blockUV[FacesEnum.RIGHT]
						} 
					},
					{FacesEnum.TOP, new Face {
					vertices = MoveToPosition(FacesVertexCollections.BlocksVertexCollection[FacesEnum.TOP]),
					uv = blockUV[FacesEnum.TOP]
						} 
					},
					{FacesEnum.BOTTOM, new Face {
					vertices = MoveToPosition(FacesVertexCollections.BlocksVertexCollection[FacesEnum.BOTTOM]),
					uv = blockUV[FacesEnum.BOTTOM]
						} 
					},
				};
			}
		}
		#endregion

		#region private funcs
		private List<Vector3> MoveToPosition(List<Vector3> vertices)
		{
			List<Vector3> transformedVertices = new List<Vector3>();
			foreach (var vert in vertices)
			{
				transformedVertices.Add(vert + position);
			}
			return transformedVertices;
		}
		#endregion

		#region public funcs
		public Face GetFace(FacesEnum face)
		{
			return faces[face];
		}
		#endregion
	}
}
