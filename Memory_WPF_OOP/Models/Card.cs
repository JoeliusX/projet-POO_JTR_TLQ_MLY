using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Memory_WPF_OOP.Controllers;

namespace Memory_WPF_OOP.Models
{
    internal class Card
    {
        private readonly Game game;
        private readonly List<Button> cardButtons;
        private readonly TextBlock scoreText;
        private readonly User current;
        private readonly Storyboard flipAnimation;
        private readonly Storyboard flipAnimationReverse;
        private readonly TextBlock victoryText;
        private readonly DatabaseService db;
        private readonly Action refreshLeaderboard;

        public Card(Game game, List<Button> cardButtons, TextBlock scoreText, User current,
            Storyboard flipAnimation, Storyboard flipAnimationReverse, TextBlock victoryText, DatabaseService db, Action refreshLeaderboard)
        {
            this.game = game;
            this.cardButtons = cardButtons;
            this.scoreText = scoreText;
            this.current = current;
            this.db = db;
            this.refreshLeaderboard = refreshLeaderboard;
            this.flipAnimation = flipAnimation;
            this.flipAnimationReverse = flipAnimationReverse;
            this.victoryText = victoryText;
        }

        public async void Image_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int index = (int)clickedButton.Tag;

            if (clickedButton.Content is Image img && !img.Source.ToString().Contains("CardBack.png"))
                return;

            game.Choose(index);

            string imageFileName = game.Cards[index];
            string imagePath = $"pack://application:,,,/Pictures/{imageFileName}";

            flipAnimation.Begin(clickedButton);
            await Task.Delay(150);

            Image image = new Image
            {
                Source = new BitmapImage(new Uri(imagePath)),
                Stretch = Stretch.UniformToFill
            };
            clickedButton.Content = image;

            if (game.chosenCart1 != null && game.chosenCart2 != null)
            {
                // Bloquer tous les boutons pendant la comparaison
                foreach (var btn in cardButtons)
                    btn.IsEnabled = false;

                await Task.Delay(1500);

                if (game.status == "wrong")
                {
                    flipAnimationReverse.Begin(cardButtons[game.chosenCart1.Value]);
                    flipAnimationReverse.Begin(cardButtons[game.chosenCart2.Value]);
                    await Task.Delay(150);

                    foreach (var i in new[] { game.chosenCart1.Value, game.chosenCart2.Value })
                    {
                        Image backImage = new Image
                        {
                            Source = new BitmapImage(new Uri("pack://application:,,,/CardBack.png")),
                            Stretch = Stretch.UniformToFill
                        };
                        cardButtons[i].Content = backImage;
                    }
                }
                else if (game.status == "correct")
                {
                    // Si correct → laisser les bonnes cartes désactivées
                    cardButtons[game.chosenCart1.Value].IsEnabled = false;
                    cardButtons[game.chosenCart2.Value].IsEnabled = false;
                }

                if (game.Score > current.Score)
                {
                    current.Score = game.Score;
                    db.UpdateScore(current.Id, current.Score);
                    refreshLeaderboard();
                }

                scoreText.Text = $"Score : {game.Score}";
                game.ResetChoices();

                // ⚠️ Réactiver seulement les boutons qui ont encore le dos (les paires non encore trouvées)
                foreach (var btn in cardButtons)
                {
                    if (btn.Content is Image btnImage && btnImage.Source.ToString().Contains("CardBack.png"))
                    {
                        btn.IsEnabled = true;
                    }
                }

                bool allCardsRevealed = cardButtons.All(btn =>
                    btn.Content is Image cardImage &&
                    !cardImage.Source.ToString().Contains("CardBack.png"));

                if (allCardsRevealed)
                {
                    victoryText.Visibility = Visibility.Visible;
                }
            }
        }
    }
}