using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.GraphicModels
{
	/// <summary>
	/// Elements buffer object | similar to IBO
	/// </summary>
	internal class EBO
	{
		/// <summary>
		/// handler for OpenGL
		/// </summary>
		public int ID;
		/// <summary>
		/// cotr for ebo -> ready to use
		/// </summary>
		/// <param name="data"></param>
		public EBO(List<uint> data)
		{
			ID = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
			GL.BufferData(BufferTarget.ElementArrayBuffer, data.Count * sizeof(uint), data.ToArray(), BufferUsageHint.StaticDraw);
		}
		/// <summary>
		/// bind ebo to Element array buffer
		/// </summary>
		public void Use() { GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID); }
		/// <summary>
		/// set Element array buffer to 0
		/// </summary>
		public void Unbind() { GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0); }
		/// <summary>
		/// Delete Ebo from graphics card
		/// </summary>
		public void Delete() { GL.DeleteBuffer(ID); }
	}
}
