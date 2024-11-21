using Hiscraft.WorldModels;
using OpenTK.Graphics.ES20;

namespace Hiscraft
{
	internal class Program
	{
		static void Main(string[] args)
		{
			try
			{
			WorldConst.ReadConst();

			}catch(Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(e.Message.ToString());
				return;
			}
			using (Game game = new Game(1580, 720))
			{
				game.Run();
			}
		}
	}
}
