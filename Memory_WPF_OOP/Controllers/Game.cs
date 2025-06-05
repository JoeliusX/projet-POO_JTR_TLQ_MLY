
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
<<<<<<< HEAD
        public int Score { get; private set; } = 0; // initialiserla variable du score



=======
        
        // Création des cartes
>>>>>>> 73a4ec4353e48a12df6d4ee209e19ad1816c189c
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

<<<<<<< HEAD

=======
        // Compare les deux cartes choisies
>>>>>>> 73a4ec4353e48a12df6d4ee209e19ad1816c189c
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
