using Memory_WPF_OOP.Controllers;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Windows;

using System.Windows.Controls;

using System.Windows.Data;

using System.Windows.Documents;

using System.Windows.Input;

using System.Windows.Media;

using System.Windows.Media.Animation;

using System.Windows.Media.Imaging;

using System.Windows.Navigation;

using System.Windows.Shapes;

namespace Memory_WPF_OOP

{

    /// <summary>

    /// Interaction logic for Menu.xaml

    /// </summary>

    /// 

    public partial class Menu : Page

    {

        private readonly DatabaseService db = new DatabaseService();

        private User current;

        private Game game;

        private List<Button> cardButtons;

        private bool isChecking = false;

        public Menu()

        {

            InitializeComponent();

            game = new Game();

            LoadGrid();

            RefreshLeaderboard();

        }

        private class LeaferItem

        {

            public int Rank { get; set; }

            public string Nom { get; set; }

            public int Score { get; set; }

        }

        // differents fonds d'écran

        private readonly List<string> backgroundImages = new List<string>

{

        "/ChatGPT Image 10 avr. 2025, 15_16_54.png",

        "/e3867127-4eec-4261-8cef-161b51c19b87.png",

        "/9f04cc4e-12cd-4c9f-8602-6dbc24c8bc2a.png",

        "/Copilot_20250605_163819.png"

};

        private int currentBackgroundIndex = 0;


        private void NameOverlayOkButton_Click(object sender, RoutedEventArgs e)

        {

            string name = OverlayNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))

            {

                MessageBox.Show("Enter your name");

                return;

            }

            pseudoText.Text = name;

            current = db.GetUserByName(name) ?? db.CreateUser(name);



            if (current == null)

                current = db.CreateUser(name);

            NameOverlay.Visibility = Visibility.Collapsed;

        }

        private async void Image_Click(object sender, RoutedEventArgs e)

        {

            // Eviter pouvoir tourner plus de cartes pendant la comparaison d'images

            if (isChecking) return;

            Button clickedButton = sender as Button;

            int index = (int)clickedButton.Tag;

            // Stoper la fonction si on clique sur la même carte plusieures fois de suite

            if (clickedButton.Content is Image img && !img.Source.ToString().Contains("CardBack.png"))

                return;

            // Choisir une carte

            game.Choose(index);

            string imageFileName = game.Cards[index];

            string imagePath = $"pack://application:,,,/Pictures/{imageFileName}";

            Storyboard flipAnimation = (Storyboard)this.FindResource("FlipCardStoryboard");

            flipAnimation.Begin(clickedButton);

            await Task.Delay(150);

            // Montrer l'image de la carte choisie

            Image image = new Image

            {

                Source = new BitmapImage(new Uri(imagePath)),

                Stretch = Stretch.UniformToFill

            };

            clickedButton.Content = image;

            // Comparaison entre les deux cartes si deux cartes ont été choisies

            if (game.chosenCart1 != null && game.chosenCart2 != null)

            {

                isChecking = true;

                await Task.Delay(1500);

                // Retourner les cartes si elles ne sont pas correctes

                if (game.status == "wrong")

                {

                    Storyboard flipAnimationReverse = (Storyboard)this.FindResource("FlipCardStoryboard");

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

                if (game.Score > current.Score)

                {

                    current.Score = game.Score;

                    db.UpdateScore(current.Id, game.Score);

                }

                scoreText.Text = $"Score : {game.Score}";

                game.ResetChoices();

                isChecking = false;

                // Vérification de si toutes les cartes sont justes

                bool allCardsRevealed = cardButtons.All(btn =>

                    btn.Content is Image cardImage &&

                    !cardImage.Source.ToString().Contains("CardBack.png"));

                if (allCardsRevealed)

                {

                    VictoryText.Visibility = Visibility.Visible;

                }

            }

        }

        private void RefreshLeaderboard()

        {

            List<User> top = db.GetTopUsers(10);

            var sb = new StringBuilder();

            sb.AppendLine("  #  Name        Score");

            sb.AppendLine(" ─────────────┬─────");

            int rank = 1;

            foreach (var u in top)

            {

                sb.AppendLine($"{rank,3}  {u.Nom,-10}  {u.Score,5}");

                rank++;

            }

            LeaderboardBox.Text = sb.ToString();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)

        {

        }

        private void pseudoText_TextChanged(object sender, TextChangedEventArgs e)

        {

        }

        private void LoadGrid()

        {

            cardButtons = new List<Button>();

            for (int i = 0; i < 32; i++)

            {

                Button cardButton = new Button

                {

                    Margin = new Thickness(10),

                    VerticalContentAlignment = VerticalAlignment.Stretch,

                    HorizontalContentAlignment = HorizontalAlignment.Stretch,

                    Style = (Style)FindResource("CardButtonStyle")  // Ici on applique le style

                };

                Image image = new Image

                {

                    Source = new BitmapImage(new Uri("pack://application:,,,/CardBack.png")),

                    Stretch = Stretch.UniformToFill

                };

                cardButton.Content = image;

                cardButton.Click += new RoutedEventHandler(Image_Click);

                CardGrid.Children.Add(cardButton);

                cardButton.Tag = i;

                cardButtons.Add(cardButton);

                cardButton.RenderTransformOrigin = new Point(0.5, 0.5);

                cardButton.RenderTransform = new ScaleTransform(1, 1);

            }

        }

        private void restartButton_Click(object sender, RoutedEventArgs e)

        {

            game.Restart();

            scoreText.Text = $"Score : {game.Score}";

            // Redonner des nouvelles images à toutes les cartes quand on recommence la partie

            for (int i = 0; i < cardButtons.Count; i++)

            {

                var button = cardButtons[i];

                Image image = new Image

                {

                    Source = new BitmapImage(new Uri("pack://application:,,,/CardBack.png")),

                    Stretch = Stretch.UniformToFill

                };

                button.Content = image;

                button.IsEnabled = true;

            }

            VictoryText.Visibility = Visibility.Collapsed;

        }

        private void BackgroundButton_Click(object sender, RoutedEventArgs e)

        {

            currentBackgroundIndex = (currentBackgroundIndex + 1) % backgroundImages.Count;

            background.Source = new BitmapImage(new Uri(backgroundImages[currentBackgroundIndex], UriKind.Relative));

        }

    }

}

