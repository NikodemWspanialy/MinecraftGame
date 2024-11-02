using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.GraphicModels
{
	/// <summary>
	/// Shader program class
	/// </summary>
	internal class Shader
	{
		/// <summary>
		/// handler for OpenGL
		/// </summary>
		int ID;
		/// <summary>
		/// cotr -> loading shaders .vert and .frag
		/// </summary>
		/// <param name="vertPath">.vert shader path</param>
		/// <param name="fragpath">.frag shader path</param>
		public Shader(string vertPath, string fragpath)
		{

			string VertexShaderSource = File.ReadAllText(vertPath);
			string FragmentShaderSource = File.ReadAllText(fragpath);

			int VertexShader = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(VertexShader, VertexShaderSource);

			int FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(FragmentShader, FragmentShaderSource);

			GL.CompileShader(VertexShader);

			GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int success);
			if (success == 0)
			{
				string infoLog = GL.GetShaderInfoLog(VertexShader);
				Console.WriteLine(infoLog);
			}

			GL.CompileShader(FragmentShader);

			GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out success);
			if (success == 0)
			{
				string infoLog = GL.GetShaderInfoLog(FragmentShader);
				Console.WriteLine(infoLog);
			}

			ID = GL.CreateProgram();

			GL.AttachShader(ID, VertexShader);
			GL.AttachShader(ID, FragmentShader);

			GL.LinkProgram(ID);

			GL.GetProgram(ID, GetProgramParameterName.LinkStatus, out success);
			if (success == 0)
			{
				string infoLog = GL.GetProgramInfoLog(ID);
				Console.WriteLine(infoLog);
			}

			GL.DetachShader(ID, VertexShader);
			GL.DetachShader(ID, FragmentShader);
			GL.DeleteShader(FragmentShader);
			GL.DeleteShader(VertexShader);
		}
		/// <summary>
		/// Getter for ID
		/// </summary>
		/// <returns>shader program ID</returns>
		public int GetShaderId() => ID;
		/// <summary>
		/// Bind shader program in OpenGL
		/// </summary>
		public void Use()
		{
			GL.UseProgram(ID);
		}
		/// <summary>
		/// Set ShaderProgram in OpenGL to 0
		/// </summary>
		public void Unbind()
		{
			GL.UseProgram(0);
		}

		/// <summary>
		/// Delete shader program from graphics card
		/// </summary>
		/// <param name="disposing"></param>
		public void Delete()
		{
				GL.DeleteProgram(ID);
		}
	}
}
