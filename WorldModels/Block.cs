using Hiscraft.Entities;
using OpenTK.Mathematics;
using Hiscraft.Helpers;
using Hiscraft.Resources.Textures;
using Hiscraft.Entities.BlockTypeEntities;

namespace Hiscraft.WorldModels
{
	/// <summary>
	/// Block class represents a block  entity, it is hangle by chunk.
	/// Block keeps it own position, texture uv and faces vertices.
	/// </summary>
	internal class Block
	{
		#region Private fields
		/// <summary>
		/// block unique position.
		/// </summary>
		private Vector3 position;
		/// <summary>
		/// Block type from enum.
		/// </summary>
		private BlockType type;
		/// <summary>
		/// Faces handle all faces (6) with its own position (transformed by position) and uv coord from texture book.
		/// </summary>
		private Dictionary<FacesEnum, Face> faces;
		#endregion

		#region Public prop

		/// <summary>
		/// Position property only for getting.
		/// </summary>
		public Vector3 Position { get { return position; } }

		/// <summary>
		/// Block type property only for getting.
		/// </summary>
		public BlockType BlockType { get { return type; } }
		#endregion

		#region Constructor
		/// <summary>
		/// Only one ctor for Block class.
		/// </summary>
		/// <param name="position"> blocks unique position</param>
		/// <param name="blockType">block type from enum</param>
		public Block(Vector3 position, BlockType blockType = BlockType.Empty)
		{
			type = blockType;
			this.position = position;

			if (blockType != BlockType.Empty)
			{
				var size = BlockTypeInfo.BlockSizeCollections[BlockType];
				switch (size)
				{
					case BlockSizeClassifier.Normal:
						PrepareFaces_Normal();
						break;
					case BlockSizeClassifier.Semi:
						PrepareFaces_Semi();
						break;
					case BlockSizeClassifier.Cactus:
						PrepareFaces_Cactus();
						break;
					default:
						PrepareFaces_Normal();
						break;
				}
			}
		}
		#endregion

		#region private funcs
		/// <summary>
		/// Functions that prepare faces to draw of block for normal block.
		/// </summary>
		private void PrepareFaces_Normal()
		{
			var blockUV = FileHelper.GetBlockUVsFromBook(TextureData.blocksUV[type]);
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
		/// <summary>
		/// Functions that prepare faces to draw of block for semi block.
		/// </summary>
		private void PrepareFaces_Semi()
		{
			var blockUV = FileHelper.GetSemiBlockUVsFromBook(TextureData.blocksUV[type]);
			faces = new()
					{
					{FacesEnum.FRONT, new Face {
						vertices = MoveToPosition(FacesVertexCollections.SemiBlocksVertexCollection[FacesEnum.FRONT]),
						uv = blockUV[FacesEnum.FRONT]
						}
					},
					{FacesEnum.BACK, new Face {
						vertices = MoveToPosition(FacesVertexCollections.SemiBlocksVertexCollection[FacesEnum.BACK]),
						uv = blockUV[FacesEnum.BACK]
						}
					},
					{FacesEnum.LEFT, new Face {
					vertices = MoveToPosition(FacesVertexCollections.SemiBlocksVertexCollection[FacesEnum.LEFT]),
					uv = blockUV[FacesEnum.LEFT]
						}
					},
					{FacesEnum.RIGHT, new Face {
					vertices = MoveToPosition(FacesVertexCollections.SemiBlocksVertexCollection[FacesEnum.RIGHT]),
					uv = blockUV[FacesEnum.RIGHT]
						}
					},
					{FacesEnum.TOP, new Face {
					vertices = MoveToPosition(FacesVertexCollections.SemiBlocksVertexCollection[FacesEnum.TOP]),
					uv = blockUV[FacesEnum.TOP]
						}
					},
					{FacesEnum.BOTTOM, new Face {
					vertices = MoveToPosition(FacesVertexCollections.SemiBlocksVertexCollection[FacesEnum.BOTTOM]),
					uv = blockUV[FacesEnum.BOTTOM]
						}
					},
					};
		}
		/// <summary>
		/// Functions that prepare faces to draw of block for cactus.
		/// </summary>
		private void PrepareFaces_Cactus()
		{
			var blockUV = FileHelper.GetCactusUVsFromBook(TextureData.blocksUV[type]);
			faces = new()
					{
					{FacesEnum.FRONT, new Face {
						vertices = MoveToPosition(FacesVertexCollections.CactusVertexCollection[FacesEnum.FRONT]),
						uv = blockUV[FacesEnum.FRONT]
						}
					},
					{FacesEnum.BACK, new Face {
						vertices = MoveToPosition(FacesVertexCollections.CactusVertexCollection[FacesEnum.BACK]),
						uv = blockUV[FacesEnum.BACK]
						}
					},
					{FacesEnum.LEFT, new Face {
					vertices = MoveToPosition(FacesVertexCollections.CactusVertexCollection[FacesEnum.LEFT]),
					uv = blockUV[FacesEnum.LEFT]
						}
					},
					{FacesEnum.RIGHT, new Face {
					vertices = MoveToPosition(FacesVertexCollections.CactusVertexCollection[FacesEnum.RIGHT]),
					uv = blockUV[FacesEnum.RIGHT]
						}
					},
					{FacesEnum.TOP, new Face {
					vertices = MoveToPosition(FacesVertexCollections.CactusVertexCollection[FacesEnum.TOP]),
					uv = blockUV[FacesEnum.TOP]
						}
					},
					{FacesEnum.BOTTOM, new Face {
					vertices = MoveToPosition(FacesVertexCollections.CactusVertexCollection[FacesEnum.BOTTOM]),
					uv = blockUV[FacesEnum.BOTTOM]
						}
					},
					};
		}
		/// <summary>
		/// Position moving position to correct coordinates.
		/// </summary>
		/// <param name="vertices"></param>
		/// <returns></returns>
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
		/// <summary>
		/// Methods that return faces of block.
		/// </summary>
		/// <param name="face">face from face enum</param>
		/// <returns>Face collections</returns>
		public Face GetFace(FacesEnum face)
		{
			return faces[face];
		}
		#endregion
	}
}
