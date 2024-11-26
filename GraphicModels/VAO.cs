using OpenTK.Graphics.OpenGL4;

namespace Hiscraft.GraphicModels
{

	/// <summary>
	/// Vertex array object class.
	/// </summary>
	internal class VAO
	{
		/// <summary>
		/// Handler for OpenGL.
		/// </summary>
		public int ID;
		/// <summary>
		/// Constructor creating nad binding VAO to Vertex Array.
		/// </summary>
		public VAO()
		{
			ID = GL.GenVertexArray();
			GL.BindVertexArray(ID);
		}
		/// <summary>
		/// Connect VBO is connecting passed VBO with VAO.
		/// </summary>
		/// <param name="location"></param>
		/// <param name="size"></param>
		/// <param name="vbo"></param>
		public void ConnectVBO(int location, int size, VBO vbo)
		{
			Use();
			vbo.Use();
			GL.VertexAttribPointer(location, size, VertexAttribPointerType.Float, false, 0, 0);
			GL.EnableVertexAttribArray(location);
			Unbind();
		}
		/// <summary>
		/// Bind VAO to vertex array.
		/// </summary>
		public void Use() { GL.BindVertexArray(ID); }
		/// <summary>
		/// Unbind VAO (setting VertexArray bind to 0).
		/// </summary>
		public void Unbind() { GL.BindVertexArray(0); }
		/// <summary>
		/// Delete VAO in OpenGL.
		/// </summary>
		public void Delete() { GL.DeleteVertexArray(ID); }
	}
}
