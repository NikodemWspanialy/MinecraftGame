using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hiscraft.Entities;
using Hiscraft.GraphicModels;
using Hiscraft.WorldModels;
using Hiscraft.GeneratingTerrain;
using System.Diagnostics.CodeAnalysis;
using Hiscraft.Helpers;

namespace Hiscraft.WorldModels
{
	/// <summary>
	/// chunk is main class in that keep blocks, but also graphics pipelines, it is responsible for optimization faces to draw
	/// </summary>
	internal class Chunk
	{
		#region private fields
		private Block[,,] blocks;
		private List<Vector3> chunkVertices;
		private List<Vector2> chunTextureUVs;
		private List<uint> chunkIndices;
		private Vector3 position;
		private uint indexCount;
		#endregion
		
		#region private graphics fields
		private VAO chunkVAO;
		private VBO chunkVertexVBO;
		private VBO chunkUVVBO;
		private EBO chunkEBO;
		private Texture texture;
		#endregion
		
		#region const for chunk -> to extract to options class

		const int SIZE = 16;
		const int HIGH = 32;
		#endregion
		
		#region constructor
		public Chunk(int positionX, int positionZ)
		{
			this.position = new Vector3(positionX * SIZE, 0, positionZ * SIZE);

			blocks = new Block[SIZE, HIGH, SIZE];
			chunkVertices = new List<Vector3>();
			chunTextureUVs = new List<Vector2>();
			chunkIndices = new List<uint>();

			GenerateBlocks();
			PreparePipelines();
		}
		#endregion

		#region private functions

		/// <summary>
		/// this function itterating for every block in chunk and calling procedral method to find block type
		/// </summary>
		private void GenerateBlocks()
		{
			for (int x = (int)position.X; x < (int)position.X + SIZE; x++)
			{
				for (int y = 0; y < HIGH; y++)
				{
					for (int z = 0; z < (int)position.Z + SIZE; z++)
					{
						var type = Procedural1.Find(x, y, z);
						if (type != BlockType.Empty)
						{
							Block block = new Block(new Vector3(x, y, z), type);
							blocks[x, y, z] = block;
						}
					}
				}
			}
			foreach (var block in blocks)
			{
				if (block != null)
				{
					AddFacesToDraw(block);
				}
			}
		}
		/// <summary>
		/// optimalize amount of facesto draw, and adding them to lists of verticles and uvs
		/// </summary>
		/// <param name="block"></param>
		private void AddFacesToDraw(Block block)
		{
			var leftFaceData = block.GetFace(FacesEnum.LEFT);
			chunkVertices.AddRange(leftFaceData.vertices);
			chunTextureUVs.AddRange(leftFaceData.uv);
			var rightFaceData = block.GetFace(FacesEnum.RIGHT);
			chunkVertices.AddRange(rightFaceData.vertices);
			chunTextureUVs.AddRange(rightFaceData.uv);

			var frontFaceData = block.GetFace(FacesEnum.FRONT);
			chunkVertices.AddRange(frontFaceData.vertices);
			chunTextureUVs.AddRange(frontFaceData.uv);

			var backFaceData = block.GetFace(FacesEnum.BACK);
			chunkVertices.AddRange(backFaceData.vertices);
			chunTextureUVs.AddRange(backFaceData.uv);

			var topFaceData = block.GetFace(FacesEnum.TOP);
			chunkVertices.AddRange(topFaceData.vertices);
			chunTextureUVs.AddRange(topFaceData.uv);

			var bottomFaceData = block.GetFace(FacesEnum.BOTTOM);
			chunkVertices.AddRange(bottomFaceData.vertices);
			chunTextureUVs.AddRange(bottomFaceData.uv);


			AddIndices(6);
		}
		/// <summary>
		/// adding indices of square devided to 2 triangles in path 0,1,2,2,3,0
		/// </summary>
		/// <param name="amtFaces"></param>
		private void AddIndices(int amtFaces)
		{
			for (int i = 0; i < amtFaces; i++)
			{
				chunkIndices.Add(0 + indexCount);
				chunkIndices.Add(1 + indexCount);
				chunkIndices.Add(2 + indexCount);
				chunkIndices.Add(2 + indexCount);
				chunkIndices.Add(3 + indexCount);
				chunkIndices.Add(0 + indexCount);

				indexCount += 4;
			}
		}
		/// <summary>
		/// creating pipelines for openGL
		/// </summary>
		private void PreparePipelines()
		{
			chunkVAO = new VAO();
			chunkVAO.Use();

			chunkVertexVBO = new VBO(chunkVertices);
			chunkVertexVBO.Use();
			chunkVAO.ConnectVBO(0, 3, chunkVertexVBO);

			chunkUVVBO = new VBO(chunTextureUVs);
			chunkUVVBO.Use();
			chunkVAO.ConnectVBO(1, 2, chunkUVVBO);

			chunkEBO = new EBO(chunkIndices);

			texture = new Texture(FileHelper.GetTexturePath("TextureBook.png"));
		} // take data and process it for rendering
		#endregion

		#region public functions

		/// <summary>
		/// Function that draw the whole chunk
		/// </summary>
		/// <param name="program">as a parameter it takes shader prgoram</param>
		public void Render(Shader shader) 
		{
			//bind pipeliens
			shader.Use();
			chunkVAO.Use();
			chunkEBO.Use();
			texture.Use();
			//draw
			GL.DrawElements(PrimitiveType.Triangles, chunkIndices.Count, DrawElementsType.UnsignedInt, 0);
			//unbind pipelines
			chunkEBO.Unbind();
			chunkVAO.Unbind();
			texture.Unbind();

		}

		/// <summary>
		/// Delete method is deleting all graphics pipelines, textures 
		/// </summary>
		public void Delete()
		{
			chunkVAO.Delete();
			chunkVertexVBO.Delete();
			chunkUVVBO.Delete();
			chunkEBO.Delete();
			texture.Delete();
		}
		#endregion
	}
}