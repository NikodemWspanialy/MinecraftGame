using Hiscraft.GraphicModels;
using Hiscraft.Helpers;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.WorldModels
{
	internal class Game : GameWindow
	{
		// set of vertices to draw the triangle with (x,y,z) for each vertex

		Chunk chunk;
		Shader shader;
		Camera camera;

		float yRot = 0f;

		int width, height;
		public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
		{
			this.width = width;
			this.height = height;

			CenterWindow(new Vector2i(width, height));
		}
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

			chunk = new Chunk(0,0);
			shader = new Shader(FileHelper.GetShaderPath("Default.vert"), FileHelper.GetShaderPath("Default.frag"));

			GL.Enable(EnableCap.DepthTest);

			camera = new Camera(width, height, new Vector3(0f, 33.4f, 0f));
			CursorState = CursorState.Grabbed;
		}
		protected override void OnUnload()
		{
			base.OnUnload();
			chunk.Delete();
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

			chunk.Render(shader);

			Context.SwapBuffers();

			base.OnRenderFrame(args);
		}
		protected override void OnUpdateFrame(FrameEventArgs args)
		{
			MouseState mouse = MouseState;
			KeyboardState input = KeyboardState;

			base.OnUpdateFrame(args);
			camera.Update(input, mouse, args);
		}
		protected override void OnKeyDown(KeyboardKeyEventArgs e)
		{
			if (e.Key == Keys.Escape)
			{
				Close();
			}
		}


	}
}
