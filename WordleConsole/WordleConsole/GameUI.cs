using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleConsole
{
    internal class GameUI
    {
        private List<String> words = new List<String>();
        String wordToGuess = null;
        Game game;
        int nbTries;
        private List<Game> games = new List<Game>();

        /// <summary>
        /// Initialize game
        /// </summary>
        public void CreateGame()
        {
            words.Add("ALIBI");
            words.Add("BALAI");
            words.Add("VILLE");
            words.Add("BOLET");
        }


        /// <summary>
        /// Start the game and play it
        /// </summary>
        public void PlayGame() { 

            bool play = true;

            while (play)
            {
                Random random = new Random();
                wordToGuess = words[random.Next(words.Count)];

                game = new Game();
                game.Succeed = false;
                nbTries = 0;

                string? pseudo = "";
                while (pseudo == "")
                {
                    Console.WriteLine("Quel est ton nom?");
                    pseudo = Console.ReadLine();    
                }
                game.Pseudo = pseudo;

                while (!game.Succeed && nbTries <= 4)
                {
                    Console.WriteLine("Quel mot proposes-tu?");
                    string? word = Console.ReadLine();
                    try
                    {
                        nbTries++;
                        ValidateWord(word);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                    Console.WriteLine();
                }
                if (game.Succeed)
                {
                    Console.WriteLine(game.ToString());
                }
                if (nbTries >= 5)
                {
                    Console.WriteLine("Vous avez fait plus de 5 coups, vous avez perdu.");
                }
                game.NbTries = nbTries;
                games.Add(game);
                Console.WriteLine("Voulez-vous recommencer une partie? Y / N ");
                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    play = true;
                } else
                {
                    play = false;
                }
                Console.WriteLine();
            }
        }


        private void ValidateWord(string word)
        {
            int pos = 0;
            string txtWord = "";

            if (word == null || word == "")
            {
                throw new Exception("Mot vide");
            }

            if (word.Length != wordToGuess.Length)
            {
                throw new Exception("Longueur du mot incorrect");
            }

            while (pos < word.Length)
            {
                if (wordToGuess[pos].ToString() == word[pos].ToString())
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                }
                else
                {
                    if (wordToGuess.Contains(word[pos].ToString()))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                }

                Console.Write(word[pos].ToString());
                
                txtWord += word[pos].ToString();
                pos++;
            }

            Console.ForegroundColor = ConsoleColor.White;

            if (txtWord == wordToGuess)
            {
                game.Succeed = true;
                game.NbTries = nbTries;
            }
        }

        public void DisplayHistory()
        {
            int count = 0;
            foreach (Game game in games)
            {
                count++;
                Console.WriteLine("Partie " + count + " : " + game.ToString());
            }
        }
    }
}
