using Hiscraft.GeneratingTerrain;
using Hiscraft.GraphicModels;
using Hiscraft.Helpers;
using Hiscraft.Threads;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace Hiscraft.WorldModels
{
	internal class Game : GameWindow
	{

		#region game elements
		/// <summary>
		/// list of all chunks generated
		/// </summary>
		private List<Chunk> allChunks;

		/// <summary>
		/// list of chunks rendnered every frame
		/// </summary>
		private List<Chunk> renderChunks;

		/// <summary>
		/// camera object
		/// </summary>
		private Camera camera;

		/// <summary>
		/// field handing last player chunk to call when it changed
		/// </summary>
		private Vector2i lastPlayerChunk = new Vector2i(0, 0);
		#endregion

		#region privates fields
		/// <summary>
		/// Width and height of window.
		/// </summary>
		private int width, height;

		/// <summary>
		/// Field for Shader program for every object.
		/// </summary>
		private Shader shader;

		/// <summary>
		/// Field for Texure for every object.
		/// </summary>
		private Texture texture;
		#endregion


		#region ctor
		/// <summary>
		/// Constructor with 2 parameter.
		/// </summary>
		/// <param name="width">window width</param>
		/// <param name="height">window height</param>
		public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
		{
			this.width = width;
			this.height = height;
			renderChunks = new List<Chunk>();
			allChunks = new List<Chunk>();

			CenterWindow(new Vector2i(width, height));

			ProceduralGeneration.Prepare(
				seeder: WorldConst.SEED,
				continetalness: WorldConst.CONTINENTALNESS,
				erosion: WorldConst.EROSIONS,
				peakAndValley: WorldConst.PEAKS_AND_VALLEYS,
				details: WorldConst.DETAILS_CONGESTION,
				tree: WorldConst.THREES_SCALE,
				water: WorldConst.WATER,
				highLevel: WorldConst.HIGH,
				naturalResourcesRate: WorldConst.NATURAL_RESOURCES

				);
		}
		#endregion

		#region private methods to managment game
		/// <summary>
		/// Initialize first chunks.
		/// </summary>
		private void InitChunks()
		{
			ConsoleWriter.Write("Chunks initialization", ConsoleColor.Red, ConsoleColor.Yellow);
			var newRenderMap = new List<Chunk>();
			Stopwatch stopwatch = Stopwatch.StartNew();
			for (int x = -WorldConst.CHUNK_OFFSET; x <= +WorldConst.CHUNK_OFFSET; ++x)
			{
				for (int z = -WorldConst.CHUNK_OFFSET; z <= +WorldConst.CHUNK_OFFSET; ++z)
				{
					stopwatch.Restart();
					var chunk = allChunks.FirstOrDefault(ch => ch.ChunkPosition.X == x && ch.ChunkPosition.Y == z);
					if (chunk is not null)
					{
						renderChunks.Add(chunk);
					}
					else
					{
						chunk = new Chunk(x, z);
						renderChunks.Add(chunk);
						allChunks.Add(chunk);
					}
					stopwatch.Stop();
					ConsoleWriter.Write($"initialize chunk x = {x}|z = {z} generating time = {stopwatch.ElapsedMilliseconds} ms");
				}
			}
			ConsoleWriter.Write("End of initialize components", ConsoleColor.Red, ConsoleColor.Yellow);
		}

		/// <summary>
		/// Prepare new chunks after changing a camera position, as argument passed center chunk (camera position).
		/// </summary>
		private async Task PrepareChunks(int chankX, int chankZ)
		{
			await Task.Yield();
			ConsoleWriter.Write($"NEW CENTER IN {chankX}|{chankZ}", ConsoleColor.Blue, ConsoleColor.Red);


			for (int x = chankX - WorldConst.CHUNK_OFFSET; x <= chankX + WorldConst.CHUNK_OFFSET; ++x)
			{
				for (int z = chankZ - WorldConst.CHUNK_OFFSET; z <= chankZ + WorldConst.CHUNK_OFFSET; ++z)
				{
					lock (ThreadManager.lockerRenderList)
					{
						if (null != renderChunks.FirstOrDefault(c => c.ChunkPosition.X == x && c.ChunkPosition.Y == z))
						{
							continue;
						}
					}
					Chunk? chunk;
					lock (ThreadManager.lockerAllList)
					{
						chunk = allChunks.FirstOrDefault(c => c.ChunkPosition.X == x && c.ChunkPosition.Y == z);
					}
					if (chunk is null)
					{
						Stopwatch sw = Stopwatch.StartNew();
						await Task.Run(() =>
						{
							ConsoleWriter.Write($"Chunk <{x},{z}> was generating...", fontColor: ConsoleColor.Black, backgroundColor: ConsoleColor.Yellow);
							chunk = new Chunk(x, z);
							lock (ThreadManager.lockerAllList)
							{
								allChunks.Add(chunk);
							}
							lock (ThreadManager.lockerRenderList)
							{
								renderChunks.Add(chunk);
							}
							sw.Stop();
							ConsoleWriter.Write($"Chunk <{x},{z}> was generated in {sw.ElapsedMilliseconds}", fontColor: ConsoleColor.Black, backgroundColor: ConsoleColor.Yellow);
						});
					}
					else
					{
						lock (ThreadManager.lockerRenderList)
						{
							renderChunks.Add(chunk);
							ConsoleWriter.Write($"Chunk <{x},{z}> was successfully again added to render list", fontColor: ConsoleColor.Black, backgroundColor: ConsoleColor.Blue);
						}
					}
				}
			}
			IEnumerable<Chunk>? chunkThatExist = [];
			lock (ThreadManager.lockerAllList)
			{
				chunkThatExist = allChunks.Where(c => c.ChunkPosition.X < chankX - WorldConst.CHUNK_OFFSET
								|| c.ChunkPosition.X > chankX + WorldConst.CHUNK_OFFSET
								|| c.ChunkPosition.Y < chankZ - WorldConst.CHUNK_OFFSET
								|| c.ChunkPosition.Y > chankZ + WorldConst.CHUNK_OFFSET);
			}
			if (chunkThatExist.Count() != 0)
			{
				lock (ThreadManager.lockerRenderList)
				{
					renderChunks.RemoveAll(c => chunkThatExist.Contains(c));
				}
			}
		}

		/// <summary>
		/// Check camera position chunk and call creating new chunk.
		/// </summary>
		private void CheckChunk()
		{
			var cameraPos = camera.Position;
			int ChunkX = ((int)cameraPos.X) / WorldConst.CHUNK_SIZE;
			int ChunkY = ((int)cameraPos.Z) / WorldConst.CHUNK_SIZE;
			if (lastPlayerChunk.X != ChunkX || lastPlayerChunk.Y != ChunkY)
			{
				PrepareChunks(ChunkX, ChunkY);
				lastPlayerChunk.X = ChunkX;
				lastPlayerChunk.Y = ChunkY;

			}
			return;
		}

		#endregion
		#region everride methods
		protected override void OnResize(ResizeEventArgs e)
		{
			base.OnResize(e);
			GL.Viewport(0, 0, e.Width, e.Height);
			this.width = e.Width;
			this.height = e.Height;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			InitChunks();

			shader = new Shader(FileHelper.GetShaderPath("Default.vert"), FileHelper.GetShaderPath("Default.frag"));
			shader.Use();
			texture = new Texture(FileHelper.GetTexturePath(WorldConst.TEXTURE_BOOK));
			texture.Use();
			GL.Enable(EnableCap.DepthTest);

			camera = new Camera(width, height, new Vector3(0f, 33.4f, 0f));
			CursorState = CursorState.Grabbed;
		}
		protected override void OnUnload()
		{
			base.OnUnload();
			foreach (var chunk in allChunks)
			{
				chunk?.Delete();
			}
			shader.Unbind();
			shader.Delete();
			texture.Unbind();
			texture.Delete();
		}
		protected override void OnRenderFrame(FrameEventArgs args)
		{
			GL.ClearColor(0.3f, 0.3f, 1f, 1f);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


			Matrix4 model = Matrix4.Identity;
			Matrix4 view = camera.GetViewMatrix();
			Matrix4 projection = camera.GetProjectionMatrix();


			int modelLocation = GL.GetUniformLocation(shader.GetShaderId(), "model");
			int viewLocation = GL.GetUniformLocation(shader.GetShaderId(), "view");
			int projectionLocation = GL.GetUniformLocation(shader.GetShaderId(), "projection");

			GL.UniformMatrix4(modelLocation, true, ref model);
			GL.UniformMatrix4(viewLocation, true, ref view);
			GL.UniformMatrix4(projectionLocation, true, ref projection);
			lock (ThreadManager.lockerRenderList)
			{
				foreach (var chunk in renderChunks)
				{
					if (chunk.IsReady)
					{

						chunk.Render();
					}
				}
			}
			Context.SwapBuffers();
		}

		protected override void OnUpdateFrame(FrameEventArgs args)
		{
			lock (ThreadManager.locker)
			{
				Action? action;
				if (ThreadManager.ActionToInvokeByMainThreadQueue.TryDequeue(out action))
				{
					action();
				}
			}
			MouseState mouse = MouseState;
			KeyboardState input = KeyboardState;

			base.OnUpdateFrame(args);
			camera.Update(input, mouse, args);
			if (WorldConst.GENERATE_CHUNK)
			{
				CheckChunk();
			}
		}

		/// <summary>
		/// override onKeyDown
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyDown(KeyboardKeyEventArgs e)
		{
			if (e.Key == Keys.Escape)
			{
				Close();
			}
			if (e.Key == Keys.F)
			{
				if (!IsFullscreen)
				{
					WindowState = WindowState.Fullscreen;
				}
				else
				{
					WindowState = WindowState.Normal;
				}
			}
		}

		#endregion
	}
}
