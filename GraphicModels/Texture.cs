using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.GraphicModels
{
	internal class Texture
	{
		int ID;
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
		public void Use()
		{
			GL.BindTexture(TextureTarget.Texture2D, ID);
		}
		public int GetId() => ID;

		public void Unbind()
		{
			GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		public void Delete()
		{
			GL.DeleteTexture(ID);
		}

	}
}
