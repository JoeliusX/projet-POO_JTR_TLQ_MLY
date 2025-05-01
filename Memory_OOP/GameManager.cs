using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_OOP
{
    public class GameManager
    {
        public Board GameBoard { get; private set; }

        public GameManager()
        {
           //
        }

        public void StartGame()
        {
            Console.WriteLine("Game started");
            GameBoard = new Board();
        }
    }
}
