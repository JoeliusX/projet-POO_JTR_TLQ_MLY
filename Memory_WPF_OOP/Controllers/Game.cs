
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
        // initialiserla variable du score
        public int Score { get; private set; } = 0;


        // Création des cartes
        public Game()
        {
            Cards = GenerateCards();
            chosenCart1 = null;
            chosenCart2 = null;
            status = "undefined";
        }
        // Recommencer le jeux
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

            // Melange tous les fichiers stockés dans la liste
            Random rng = new Random();
            var shuffledImages = images.OrderBy(_ => rng.Next()).ToList();
            // Prend les 16 premiers fichiers de la liste et les duplique
            var duplication = shuffledImages.Take(16).Concat(shuffledImages.Take(16)).ToList();
            return duplication.OrderBy(_ => rng.Next()).ToList();
        }
         // Enregistre quel carte à été choisi et en quel ordre
        public void Choose(int index)
        {
            if (chosenCart1 == index || chosenCart2 == index)
            {
                chosenCart2 = null;
                return; // Ne pas compter deux fois le même clic
            }

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

        // Compare les deux cartes choisies
        public void CheckStatus()
        {
            if (chosenCart1 == null || chosenCart2 == null)
            {
                status = "undefined";
            }
            else if (Cards[(int)chosenCart1] == Cards[(int)chosenCart2])
            {
                status = "correct";
                Score+=5;
            }
            else
            {
                status = "wrong";
                Score -= 1;
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
