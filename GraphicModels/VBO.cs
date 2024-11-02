﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.GraphicModels
{
	internal class VBO
	{
		private int ID;
		public VBO(List<Vector2> data)
		{
			ID = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
			GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector2.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
		}
		public VBO(List<Vector3> data)
		{
			ID = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
			GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector3.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
		}

		public void Use() { GL.BindBuffer(BufferTarget.ArrayBuffer, ID); }
		public static void Unbind() { GL.BindBuffer(BufferTarget.ArrayBuffer, 0); }
		public void Delete() { GL.DeleteBuffer(ID); }
	}
}
