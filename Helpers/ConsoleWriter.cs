
namespace Hiscraft.Helpers
{
	/// <summary>
	/// Static methods that provide logging on console.
	/// </summary>
	internal static class ConsoleWriter
	{
		/// <summary>
		/// Static object use to managment thread working.
		/// </summary>
		private static object locker = new();
		/// <summary>
		/// Static method that write on console.
		/// </summary>
		/// <param name="text">Main text to print</param>
		/// <param name="fontColor">Color of text in the console</param>
		/// <param name="backgroundColor">Color of the background on the console</param>
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
