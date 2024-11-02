using Hiscraft.WorldModels;

namespace Hiscraft
{
	internal class Program
	{
		static void Main(string[] args)
		{
			using (Game game = new Game(1580, 720))
			{
				game.Run();
			}
		}
	}
}
