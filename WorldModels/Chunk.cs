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
using Hiscraft.Entities.BlockTypeEntities;
using Hiscraft.Threads;
using System.Diagnostics;

namespace Hiscraft.WorldModels
{
	/// <summary>
	/// chunk is main class in that keep blocks, but also graphics pipelines, it is responsible for optimization faces to draw
	/// </summary>
	internal class Chunk
	{
		public bool IsReady = false;
		#region private fields
		private Block[,,] blocks;
		private List<Vector3> chunkVertices;
		private List<Vector2> chunkTextureUVs;
		private List<uint> chunkIndices;

		private List<Vector3> chunkWaterVertices;
		private List<Vector2> chunkWaterTextureUVs;
		private List<uint> chunkWaterIndices;


		private Vector3 position;
		private uint indexCount;
		private uint indexWaterCount;
		private Vector2i chunkPosition;
		#endregion

		#region public properties
		/// <summary>
		/// chunk position getter
		/// </summary>
		public Vector2i ChunkPosition { get { return chunkPosition; } }
		#endregion

		#region private graphics fields
		private VAO chunkVAO;
		private VBO chunkVertexVBO;
		private VBO chunkUVVBO;
		private EBO chunkEBO;

		private VAO chunkWaterVAO;
		private VBO chunkVertexWaterVBO;
		private VBO chunkWaterUVVBO;
		private EBO chunkWaterEBO;

		private Texture texture;
		#endregion

		#region constructor
		public Chunk(int positionX, int positionZ)
		{
			Stopwatch sw = Stopwatch.StartNew();
			chunkPosition = new Vector2i(positionX, positionZ);
			this.position = new Vector3(positionX * WorldConst.CHUNK_SIZE, 0, positionZ * WorldConst.CHUNK_SIZE);

			blocks = new Block[WorldConst.CHUNK_SIZE, WorldConst.HIGH, WorldConst.CHUNK_SIZE];
			chunkVertices = new List<Vector3>();
			chunkTextureUVs = new List<Vector2>();
			chunkIndices = new List<uint>();

			chunkWaterIndices = new List<uint>();
			chunkWaterVertices = new List<Vector3>();
			chunkWaterTextureUVs = new List<Vector2>();

			GenerateBlocks();
			PreparePipelines();
			sw.Stop();
			ConsoleWriter.Write($"Creating chunk {chunkPosition.X}|{chunkPosition.Y} in time {sw.ElapsedMilliseconds}", ConsoleColor.Red, ConsoleColor.Green);
		}
		#endregion

		#region private functions

		/// <summary>
		/// this function itterating for every block in chunk and calling procedral method to find block type
		/// </summary>
		private void GenerateBlocks()
		{
			int XtoINT = (int)position.X;
			int ZtoINT = (int)position.Z;
			for (int x = 0; x < WorldConst.CHUNK_SIZE; x++)
			{
				for (int y = 0; y < WorldConst.HIGH; y++)
				{
					for (int z = 0; z < WorldConst.CHUNK_SIZE; z++)
					{
						var type = Procedural1.Find(XtoINT + x, y, ZtoINT + z);
						if (type != BlockType.Empty)
						{
							Block block = new Block(new Vector3(XtoINT + x, y, ZtoINT + z), type);
							blocks[x, y, z] = block;
						}
					}
				}
			}
			AddFacesToDraw();
		}
		/// <summary>
		/// itterating for every block, and calling for it FindFaces to draw
		/// </summary>
		private void AddFacesToDraw()
		{
			chunkIndices.Clear();
			chunkVertices.Clear();
			chunkTextureUVs.Clear();

			chunkWaterTextureUVs.Clear();
			chunkWaterIndices.Clear();
			chunkWaterVertices.Clear();

			foreach (var block in blocks)
			{
				if (block != null)
				{
					if (block.BlockType == BlockType.Water)
					{
						FindFacesToDrawWater(block);
					}
					else
					{
						FindFacesToDraw(block);
					}
				}
			}
		}
		/// <summary>
		/// optimalize amount of facesto draw, and adding them to lists of verticles and uvs
		/// </summary>
		/// <param name="block"></param>
		private void FindFacesToDraw(Block block)
		{
			var x = (int)block.Position.X - (int)position.X;
			var y = (int)block.Position.Y;
			var z = (int)block.Position.Z - (int)position.Z;
			int faceCounter = 0;
			bool DrawAllFaces = ShouldBeAlwaysDraw(x, y, z);

			if (DrawAllFaces || x == 0 || ShouldBeDrawAround(x - 1, y, z))
			{
				var leftChunkFace = block.GetFace(FacesEnum.LEFT);
				chunkVertices.AddRange(leftChunkFace.vertices);
				chunkTextureUVs.AddRange(leftChunkFace.uv);
				++faceCounter;
			}
			if (DrawAllFaces || x == WorldConst.CHUNK_SIZE - 1 || ShouldBeDrawAround(x + 1, y, z))
			{
				var rightChunkFace = block.GetFace(FacesEnum.RIGHT);
				chunkVertices.AddRange(rightChunkFace.vertices);
				chunkTextureUVs.AddRange(rightChunkFace.uv);
				++faceCounter;
			}
			if (DrawAllFaces || z == WorldConst.CHUNK_SIZE - 1 || ShouldBeDrawAround(x, y, z + 1))
			{
				var frontChunkFace = block.GetFace(FacesEnum.FRONT);
				chunkVertices.AddRange(frontChunkFace.vertices);
				chunkTextureUVs.AddRange(frontChunkFace.uv);
				++faceCounter;
			}
			if (DrawAllFaces || z == 0 || ShouldBeDrawAround(x, y, z - 1))
			{
				var backChunkFace = block.GetFace(FacesEnum.BACK);
				chunkVertices.AddRange(backChunkFace.vertices);
				chunkTextureUVs.AddRange(backChunkFace.uv);
				++faceCounter;
			}

			if (DrawAllFaces || y == WorldConst.HIGH - 1 || ShouldBeDrawAround(x, y + 1, z))
			{
				var topChunkFace = block.GetFace(FacesEnum.TOP);
				chunkVertices.AddRange(topChunkFace.vertices);
				chunkTextureUVs.AddRange(topChunkFace.uv);
				++faceCounter;
			}
			if (DrawAllFaces || y == 0 || ShouldBeDrawAround(x, y - 1, z))
			{

				var bottomChunkFace = block.GetFace(FacesEnum.BOTTOM);
				chunkVertices.AddRange(bottomChunkFace.vertices);
				chunkTextureUVs.AddRange(bottomChunkFace.uv);
				++faceCounter;
			}


			AddIndices(faceCounter);
		}

		private void FindFacesToDrawWater(Block block)
		{
			var x = (int)block.Position.X - (int)position.X;
			var y = (int)block.Position.Y;
			var z = (int)block.Position.Z - (int)position.Z;
			int faceCounter = 0;

			if ( x == 0 || ShouldBeDrawAround(x - 1, y, z))
			{
				var leftChunkFace = block.GetFace(FacesEnum.LEFT);
				chunkWaterVertices.AddRange(leftChunkFace.vertices);
				chunkWaterTextureUVs.AddRange(leftChunkFace.uv);
				++faceCounter;
			}
			if ( x == WorldConst.CHUNK_SIZE - 1 || ShouldBeDrawAround(x + 1, y, z))
			{
				var rightChunkFace = block.GetFace(FacesEnum.RIGHT);
				chunkWaterVertices.AddRange(rightChunkFace.vertices);
				chunkWaterTextureUVs.AddRange(rightChunkFace.uv);
				++faceCounter;
			}
			if ( z == WorldConst.CHUNK_SIZE - 1 || ShouldBeDrawAround(x, y, z + 1))
			{
				var frontChunkFace = block.GetFace(FacesEnum.FRONT);
				chunkWaterVertices.AddRange(frontChunkFace.vertices);
				chunkWaterTextureUVs.AddRange(frontChunkFace.uv);
				++faceCounter;
			}
			if ( z == 0 || ShouldBeDrawAround(x, y, z - 1))
			{
				var backChunkFace = block.GetFace(FacesEnum.BACK);
				chunkWaterVertices.AddRange(backChunkFace.vertices);
				chunkWaterTextureUVs.AddRange(backChunkFace.uv);
				++faceCounter;
			}

			if ( y == WorldConst.HIGH - 1 || ShouldBeDrawAround(x, y + 1, z))
			{
				var topChunkFace = block.GetFace(FacesEnum.TOP);
				chunkWaterVertices.AddRange(topChunkFace.vertices);
				chunkWaterTextureUVs.AddRange(topChunkFace.uv);
				++faceCounter;
			}
			if ( y == 0 || ShouldBeDrawAround(x, y - 1, z))
			{

				var bottomChunkFace = block.GetFace(FacesEnum.BOTTOM);
				chunkWaterVertices.AddRange(bottomChunkFace.vertices);
				chunkWaterTextureUVs.AddRange(bottomChunkFace.uv);
				++faceCounter;
			}


			AddWaterIndices(faceCounter);
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
		/// adding indices of water devided to 2 triangles in path 0,1,2,2,3,0
		/// </summary>
		/// <param name="amtFaces"></param>
		private void AddWaterIndices(int amtFaces)
		{
			for (int i = 0; i < amtFaces; i++)
			{
				chunkWaterIndices.Add(0 + indexCount);
				chunkWaterIndices.Add(1 + indexCount);
				chunkWaterIndices.Add(2 + indexCount);
				chunkWaterIndices.Add(2 + indexCount);
				chunkWaterIndices.Add(3 + indexCount);
				chunkWaterIndices.Add(0 + indexCount);

				indexWaterCount += 4;
			}
		}

		/// <summary>
		/// check block if it is null or no covering whole block
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="z">Z</param>
		/// <returns>should be draw block next to or not</returns>
		private bool ShouldBeDrawAround(int x, int y, int z)
		{
			if (blocks[x, y, z] is null) { return true; }
			if (BlockTypeInfo.noCoveringBlocks.Contains(blocks[x, y, z].BlockType)) { return true; }
			return false;
		}

		/// <summary>
		/// Check if block should be always draw
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <returns></returns>
		private bool ShouldBeAlwaysDraw(int x, int y, int z)
		{
			if (BlockTypeInfo.alwaysDrawBlocks.Contains(blocks[x, y, z].BlockType)) { return true; }
			return false;
		}
		/// <summary>
		/// creating pipelines for openGL
		/// </summary>
		private void PreparePipelines()
		{
			lock (ThreadManager.locker)
			{
				ThreadManager.ActionToInvokeByMainThreadQueue.Enqueue(() =>
				{
					Stopwatch stopwatch = Stopwatch.StartNew();
					chunkVAO = new VAO();
					chunkVAO.Use();

					chunkVertexVBO = new VBO(chunkVertices);
					chunkVertexVBO.Use();
					chunkVAO.ConnectVBO(0, 3, chunkVertexVBO);

					chunkUVVBO = new VBO(chunkTextureUVs);
					chunkUVVBO.Use();
					chunkVAO.ConnectVBO(1, 2, chunkUVVBO);

					chunkEBO = new EBO(chunkIndices);

					texture = new Texture(FileHelper.GetTexturePath("TextureBookUpdate.png"));

					chunkWaterVAO = new VAO();
					chunkWaterVAO.Use();

					chunkVertexWaterVBO = new VBO(chunkWaterVertices);
					chunkVertexWaterVBO.Use();
					chunkWaterVAO.ConnectVBO(0, 3, chunkVertexWaterVBO);

					chunkWaterUVVBO = new VBO(chunkWaterTextureUVs);
					chunkWaterUVVBO.Use();
					chunkWaterVAO.ConnectVBO(1, 2, chunkWaterUVVBO);

					chunkWaterEBO = new EBO(chunkWaterIndices);

					stopwatch.Stop();
					ConsoleWriter.Write($"Prepare chunks pipelines for {position.X}|{position.Z} in time {stopwatch.ElapsedMilliseconds}", ConsoleColor.White, ConsoleColor.Blue);
					IsReady = true;
				});
			}
		}
		#endregion

		#region public functions

		/// <summary>
		/// Function that draw the whole chunk
		/// </summary>
		/// <param name="program">as a parameter it takes shader prgoram</param>
		public void Render(Shader shader, Shader waterShader)
		{
			//bind pipeliens
			lock (ThreadManager.locker)
			{
				shader.Use();
				chunkVAO.Use();
				chunkEBO.Use();
				texture.Use();
				//draw
				GL.DrawElements(PrimitiveType.Triangles, chunkIndices.Count, DrawElementsType.UnsignedInt, 0);
				//unbind pipelines
				chunkEBO.Unbind();
				chunkVAO.Unbind();

				waterShader.Use();
				chunkWaterVAO.Use();
				chunkWaterEBO.Use();

				GL.DrawElements(PrimitiveType.Triangles, chunkWaterIndices.Count, DrawElementsType.UnsignedInt, 0);

				chunkWaterVAO.Unbind();
				chunkWaterEBO.Unbind();
				texture.Unbind();
			}

		}

		/// <summary>
		/// Delete method is deleting all graphics pipelines, textures 
		/// </summary>
		public void Delete()
		{
			chunkVAO?.Delete();
			chunkVertexVBO?.Delete();
			chunkUVVBO?.Delete();
			chunkEBO?.Delete();
			texture?.Delete();
		}
		#endregion
	}
}