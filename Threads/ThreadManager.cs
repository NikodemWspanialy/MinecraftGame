using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.Threads
{
	internal static class ThreadManager
	{
		internal static object locker = new();
		internal static Queue<Action> ActionToInvokeByMainThreadQueue = new();
		internal static object lockerAllList = new();	
		internal static object lockerRenderList = new();	
	}
}
