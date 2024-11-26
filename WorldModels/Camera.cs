using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Hiscraft.WorldModels
{
	/// <summary>
	/// Class for main camera.
	/// </summary>
	internal class Camera
	{
		#region private fields
		private float width;
		private float height;
		private Vector3 position;
		private bool firstMove = true;
		/// <summary>
		/// Property of last position of camera.
		/// </summary>
		public Vector2 lastPos;

		private Vector3 up = Vector3.UnitY;
		private Vector3 front = -Vector3.UnitZ;
		private Vector3 right = Vector3.UnitX;
		private float pitch;
		private float yaw = -90.0f;
		#endregion

		#region const to extract from class
		private float SPEED = 8f;
		private float SENSITIVITY = 180f;
		#endregion

		#region properties
		/// <summary>
		/// position getter.
		/// </summary>
		public Vector3 Position { get { return position; } }

		#endregion

		#region constructor
		/// <summary>
		/// constructor for camera class.
		/// </summary>
		/// <param name="width"> screen width</param>
		/// <param name="height">screen height</param>
		/// <param name="position">start camera/main charactrer position</param>
		public Camera(float width, float height, Vector3 position)
		{
			this.width = width;
			this.height = height;
			this.position = position;
		}
		#endregion

		#region public functions

		/// <summary>
		/// Create for view matrix.
		/// </summary>
		/// <returns>view matrix4</returns>
		public Matrix4 GetViewMatrix()
		{
			return Matrix4.LookAt(position, position + front, up);
		}
		/// <summary>
		/// Creare projection matrix.
		/// </summary>
		/// <returns>projection matrix4</returns>
		public Matrix4 GetProjectionMatrix()
		{
			return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), width / height, 0.1f, 100.0f);
		}

		/// <summary>
		/// Update camera position to controle state.
		/// </summary>
		/// <param name="keyboardState">keyboard action</param>
		/// <param name="mouseState">mouse action</param>
		/// <param name="e">event form OpenTK to prevent lags</param>
		public void Update(KeyboardState keyboardState, MouseState mouseState, FrameEventArgs e)
		{

			if (keyboardState.IsKeyDown(Keys.W))
			{
				position += front * SPEED * (float)e.Time;
			}
			if (keyboardState.IsKeyDown(Keys.A))
			{
				position -= right * SPEED * (float)e.Time;
			}
			if (keyboardState.IsKeyDown(Keys.S))
			{
				position -= front * SPEED * (float)e.Time;
			}
			if (keyboardState.IsKeyDown(Keys.D))
			{
				position += right * SPEED * (float)e.Time;
			}

			if (keyboardState.IsKeyDown(Keys.Space))
			{
				position.Y += SPEED * (float)e.Time;
			}
			if (keyboardState.IsKeyDown(Keys.LeftShift))
			{
				position.Y -= SPEED * (float)e.Time;
			}

			if (firstMove)
			{
				lastPos = new Vector2(mouseState.X, mouseState.Y);
				firstMove = false;
			}
			else
			{
				var deltaX = mouseState.X - lastPos.X;
				var deltaY = mouseState.Y - lastPos.Y;
				lastPos = new Vector2(mouseState.X, mouseState.Y);

				yaw += deltaX * SENSITIVITY * (float)e.Time;
				pitch -= deltaY * SENSITIVITY * (float)e.Time;
			}
			UpdateVectors();
		}
		#endregion

		#region private function
		/// <summary>
		/// Updating vectors.
		/// </summary>
		private void UpdateVectors()
		{
			if (pitch > 89.0f)
			{
				pitch = 89.0f;
			}
			if (pitch < -89.0f)
			{
				pitch = -89.0f;
			}


			front.X = MathF.Cos(MathHelper.DegreesToRadians(pitch)) * MathF.Cos(MathHelper.DegreesToRadians(yaw));
			front.Y = MathF.Sin(MathHelper.DegreesToRadians(pitch));
			front.Z = MathF.Cos(MathHelper.DegreesToRadians(pitch)) * MathF.Sin(MathHelper.DegreesToRadians(yaw));

			front = Vector3.Normalize(front);

			right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
			up = Vector3.Normalize(Vector3.Cross(right, front));
		}
		#endregion
	}
}
