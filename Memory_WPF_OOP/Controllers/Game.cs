
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Memory_WPF_OOP.Controllers
{
    internal class Game
    {
        public List<string> Cards { get; private set; }
        public int? chosenCart1 { get; private set; }
        public int? chosenCart2 { get; private set; }
        public string status { get; private set; }
        public int Score { get; private set; } = 0; // initialiserla variable du score



        public Game()
        {
            Cards = GenerateCards();
            chosenCart1 = null;
            chosenCart2 = null;
            status = "undefined";
        }
        public void Restart()
        {
            Cards = GenerateCards();
            chosenCart1 = null;
            chosenCart2 = null;
            status = "undefined";
            Score = 0;
        }

        private List<string> GenerateCards()
        {
            string dossierImages = "../../Pictures";


            // Charge tous les fichiers image du dossier
            var images = Directory.GetFiles(dossierImages, "*.jpg")
                                  .Select(Path.GetFileName)
                                  .ToList();

            Random rng = new Random();
            var shuffledImages = images.OrderBy(_ => rng.Next()).ToList();
            var duplication = shuffledImages.Take(16).Concat(shuffledImages.Take(16)).ToList();
            return duplication.OrderBy(_ => rng.Next()).ToList();
        }

        public void Choose(int index)
        {
            if (chosenCart1 == index || chosenCart2 == index)
            {
                chosenCart2 = null;
                return; // Ne pas compter deux fois le même clic
            }

            // Incrémente le score ici
            Score++;

            if (chosenCart1 == null)
            {
                chosenCart1 = index;
            }
            else if (chosenCart2 == null)
            {
                chosenCart2 = index;
            }

            CheckStatus();
        }


        public void CheckStatus()
        {
            if (chosenCart1 == null || chosenCart2 == null)
            {
                status = "undefined";
            }
            else if (Cards[(int)chosenCart1] == Cards[(int)chosenCart2])
            {
                status = "correct";
                Score++;
            }
            else
            {
                status = "wrong";
            }
        }
        public void ResetChoices()
        {
            chosenCart1 = null;
            chosenCart2 = null;
            status = "undefined";
        }
    }
}
