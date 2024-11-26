using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.Threads
{
	/// <summary>
	/// Static class handling lockers and queue of actions.
	/// </summary>
	internal static class ThreadManager
	{
		/// <summary>
		/// Static locker for using action queue.
		/// </summary>
		internal static object locker = new();
		/// <summary>
		/// Static queue of actions passed to main thread.
		/// </summary>
		internal static Queue<Action> ActionToInvokeByMainThreadQueue = new();
		/// <summary>
		/// Static locker for lists which all chunk.
		/// </summary>
		internal static object lockerAllList = new();
		/// <summary>
		/// Static locker for lists of chunk to render.
		/// </summary>
		internal static object lockerRenderList = new();	
	}
}
