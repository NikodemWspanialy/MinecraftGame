using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Hiscraft.GraphicModels
{
	/// <summary>
	/// Vertex buffer obnect class
	/// </summary>
	internal class VBO
	{
		/// <summary>
		/// handler for openGL
		/// </summary>
		private int ID;
		/// <summary>
		/// constructor for uv coordinates
		/// </summary>
		/// <param name="data">list of uv positions</param>
		public VBO(List<Vector2> data)
		{
			ID = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
			GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector2.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
		}
		/// <summary>
		/// coinstructor for vertices of  
		/// </summary>
		/// <param name="data">list of verices positions</param>
		public VBO(List<Vector3> data)
		{
			ID = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
			GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector3.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
		}
		/// <summary>
		/// Use is binding this BVO to ArrayBuffer
		/// </summary>
		public void Use() { GL.BindBuffer(BufferTarget.ArrayBuffer, ID); }
		/// <summary>
		/// Unbind is clearing ArrayBuffer bind (set it to 0)
		/// </summary>
		public static void Unbind() { GL.BindBuffer(BufferTarget.ArrayBuffer, 0); }
		/// <summary>
		/// Delete VBO from graphics card
		/// </summary>
		public void Delete() { GL.DeleteBuffer(ID); }
	}
}
