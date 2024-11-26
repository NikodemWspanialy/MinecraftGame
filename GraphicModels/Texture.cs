using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.GraphicModels
{
	/// <summary>
	/// Texture class.
	/// </summary>
	internal class Texture
	{
		/// <summary>
		/// Hadler for OpenGL.
		/// </summary>
		int ID;
		/// <summary>
		/// Constructor loading the image, ready to use.
		/// </summary>
		/// <param name="imagePath">path to image</param>
		/// <param name="textureUnit">specified the texture unit -> at this moment application use only unit = 0</param>
		public Texture(string imagePath, TextureUnit textureUnit = TextureUnit.Texture0)
		{
			ID = GL.GenTexture();
			Use();

			//some library stuff
			StbImage.stbi_set_flip_vertically_on_load(1);
			//load image
			ImageResult image = ImageResult.FromStream(File.OpenRead(imagePath), ColorComponents.RedGreenBlueAlpha);
			GL.ActiveTexture(TextureUnit.Texture0);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
		}
		/// <summary>
		/// Bind texture for OpenGL.
		/// </summary>
		public void Use()
		{
			GL.BindTexture(TextureTarget.Texture2D, ID);
		}
		/// <summary>
		/// Getter for ID.
		/// </summary>
		/// <returns>ID of texture</returns>
		public int GetId() => ID;

		/// <summary>
		/// set texture bind to 0 in OpenGL.
		/// </summary>
		public void Unbind()
		{
			GL.BindTexture(TextureTarget.Texture2D, 0);
		}
		/// <summary>
		/// Delete texture form graphic card.
		/// </summary>
		public void Delete()
		{
			GL.DeleteTexture(ID);
		}

	}
}
