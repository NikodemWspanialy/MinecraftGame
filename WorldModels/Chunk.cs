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
	internal class Chunk
	{
		private Block[,,] blocks;
		public List<Vector3> chunkVerts;
		public List<Vector2> chunkUVs;
		public List<uint> chunkIndices;

		const int SIZE = 16;
		const int HIGH = 32;
		public Vector3 position;

		public uint indexCount;

		VAO chunkVAO;
		VBO chunkVertexVBO;
		VBO chunkUVVBO;
		EBO chunkEBO;

		Texture texture;
		public Chunk(int positionX, int positionZ)
		{
			this.position = new Vector3(positionX * SIZE, 0, positionZ * SIZE);

			blocks = new Block[SIZE, HIGH, SIZE];
			chunkVerts = new List<Vector3>();
			chunkUVs = new List<Vector2>();
			chunkIndices = new List<uint>();

			GenBlocks();
			BuildChunk();
		}

		public void GenBlocks()
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
		private void AddFacesToDraw(Block block)
		{
			var leftFaceData = block.GetFace(Faces.LEFT);
			chunkVerts.AddRange(leftFaceData.vertices);
			chunkUVs.AddRange(leftFaceData.uv);
			var rightFaceData = block.GetFace(Faces.RIGHT);
			chunkVerts.AddRange(rightFaceData.vertices);
			chunkUVs.AddRange(rightFaceData.uv);

			var frontFaceData = block.GetFace(Faces.FRONT);
			chunkVerts.AddRange(frontFaceData.vertices);
			chunkUVs.AddRange(frontFaceData.uv);

			var backFaceData = block.GetFace(Faces.BACK);
			chunkVerts.AddRange(backFaceData.vertices);
			chunkUVs.AddRange(backFaceData.uv);

			var topFaceData = block.GetFace(Faces.TOP);
			chunkVerts.AddRange(topFaceData.vertices);
			chunkUVs.AddRange(topFaceData.uv);

			var bottomFaceData = block.GetFace(Faces.BOTTOM);
			chunkVerts.AddRange(bottomFaceData.vertices);
			chunkUVs.AddRange(bottomFaceData.uv);


			AddIndices(6);
		}
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
		private void BuildChunk()
		{
			chunkVAO = new VAO();
			chunkVAO.Use();

			chunkVertexVBO = new VBO(chunkVerts);
			chunkVertexVBO.Use();
			chunkVAO.ConnectVBO(0, 3, chunkVertexVBO);

			chunkUVVBO = new VBO(chunkUVs);
			chunkUVVBO.Use();
			chunkVAO.ConnectVBO(1, 2, chunkUVVBO);

			chunkEBO = new EBO(chunkIndices);

			texture = new Texture(FileHelper.GetTexturePath("TextureBook.png"));
		} // take data and process it for rendering
		public void Render(Shader program) // drawing the chunk
		{
			program.Use();
			chunkVAO.Use();
			chunkEBO.Use();
			texture.Use();
			GL.DrawElements(PrimitiveType.Triangles, chunkIndices.Count, DrawElementsType.UnsignedInt, 0);
		}

		public void Delete()
		{
			chunkVAO.Delete();
			chunkVertexVBO.Delete();
			chunkUVVBO.Delete();
			chunkEBO.Delete();
			texture.Delete();
		}
	}
}