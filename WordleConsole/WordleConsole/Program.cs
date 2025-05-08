using System.Drawing;

namespace WordleConsole
{
    internal class Program
    {
        
        static void Main(string[] args)
        {

            GameUI game = new GameUI();
            game.CreateGame();
            game.PlayGame();

            game.DisplayHistory();
            
        }

    }
}
