using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.GraphicModels
{
	internal class EBO
	{
		public int ID;
		public EBO(List<uint> data)
		{
			ID = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
			GL.BufferData(BufferTarget.ElementArrayBuffer, data.Count * sizeof(uint), data.ToArray(), BufferUsageHint.StaticDraw);
		}
		public void Use() { GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID); }
		public void Unbind() { GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0); }
		public void Delete() { GL.DeleteBuffer(ID); }
	}
}
