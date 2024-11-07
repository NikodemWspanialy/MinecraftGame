using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiscraft.Helpers
{
	internal static class ConsoleWriter
	{
		private static object locker = new();
		internal static void Write(string text, ConsoleColor fontColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
		{
			lock (locker)
			{
			Console.Write($"{DateTime.Now.ToString("HH:mm:ss:fff")}: ");
			Console.ForegroundColor = fontColor;
			Console.BackgroundColor = backgroundColor;
			Console.WriteLine(text);
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Black;
			}
		}
	}
}
