using Hiscraft.GraphicModels;
using Hiscraft.Helpers;
using Hiscraft.Threads;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Diagnostics;

namespace Hiscraft.WorldModels
{
	internal class Game : GameWindow
	{

		#region game elements
		private List<Chunk> allChunks;
		private List<Chunk> renderChunks;
		private Camera camera;
		private Vector2i lastPlayerChunk = new Vector2i(0, 0);
		#endregion

		#region privates fields
		private int width, height;
		private Shader shader;
		#endregion


		#region ctor
		public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
		{
			this.width = width;
			this.height = height;
			renderChunks = new List<Chunk>();
			allChunks = new List<Chunk>();

			CenterWindow(new Vector2i(width, height));
		}
		#endregion

		#region private methods to managment game
		/// <summary>
		/// Initialize first chunks
		/// </summary>
		/// <param name="chankX"></param>
		/// <param name="chankZ"></param>
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
		/// Prepare new chunks after changing a camera position 
		/// </summary>
		private async Task PrepareChunks(int chankX, int chankZ)
		{
			await Task.Yield();
			ConsoleWriter.Write($"Change chunk, so new chunks are preparing with center in {chankX}|{chankZ}", ConsoleColor.Blue, ConsoleColor.Yellow);
			for (int x = chankX - WorldConst.CHUNK_OFFSET; x <= chankX + WorldConst.CHUNK_OFFSET; ++x)
			{
				for (int z = chankZ - WorldConst.CHUNK_OFFSET; z <= chankZ + WorldConst.CHUNK_OFFSET; ++z)
				{
					Thread.Sleep(50);
					await Task.Run(() =>
					{
						int copiedX = x;
						int copiedZ = z;
						Chunk chunk = null!;
						lock (ThreadManager.lockerAllList)
						{
							chunk = allChunks.FirstOrDefault(c => c.ChunkPosition.X == copiedX && c.ChunkPosition.Y == copiedZ);
						}
						if (chunk is null)
						{
							chunk = new Chunk(copiedX, copiedZ);
							lock (ThreadManager.lockerAllList)
							{
								allChunks.Add(chunk);
							}
							//ConsoleWriter.Write($"Creating chunk {copiedX}|{copiedZ}", ConsoleColor.Red, ConsoleColor.Green);
							lock (ThreadManager.lockerRenderList)
							{
								renderChunks.Add(chunk);
							}
						}
						else
						{
							ConsoleWriter.Write($"Chunk exists already {copiedX}|{copiedZ}", ConsoleColor.Blue, ConsoleColor.Green);
							//docelowo tez dodawanie ale to musialbym najpierw usuwac XDDD
						}
					});
				}
			}

		}

		/// <summary>
		/// check camera position chunk and call creating new chunk 
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
			shader.Delete();

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

						chunk.Render(shader);
					}
				}
			}
			Context.SwapBuffers();
		}
		protected override void OnUpdateFrame(FrameEventArgs args)
		{
			lock (ThreadManager.locker)
			{
				Action action;
				var result = ThreadManager.ActionToInvokeByMainThreadQueue.TryDequeue(out action);
				if (result)
				{
					action?.Invoke();
				}
			}
			MouseState mouse = MouseState;
			KeyboardState input = KeyboardState;

			base.OnUpdateFrame(args);
			camera.Update(input, mouse, args);
			CheckChunk();
		}
		protected override void OnKeyDown(KeyboardKeyEventArgs e)
		{
			if (e.Key == Keys.Escape)
			{
				Close();
			}
		}

		#endregion
	}
}
