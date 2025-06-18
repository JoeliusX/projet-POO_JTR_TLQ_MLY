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
using Memory_WPF_OOP.Models;

using System.Windows.Shapes;

namespace Memory_WPF_OOP
{

    /// </summary>

    /// 


    /// </summary>

        private class LeaferItem
{
        "/ChatGPT Image 10 avr. 2025, 15_16_54.png",
        "/e3867127-4eec-4261-8cef-161b51c19b87.png",
        "/9f04cc4e-12cd-4c9f-8602-6dbc24c8bc2a.png",
        "/Copilot_20250605_163819.png"
};
        {
            "/ChatGPT Image 10 avr. 2025, 15_16_54.png",
            "/e3867127-4eec-4261-8cef-161b51c19b87.png",
            "/9f04cc4e-12cd-4c9f-8602-6dbc24c8bc2a.png",
            "/Copilot_20250605_163819.png"
        };


            public int Score { get; set; }

        }

        // differents fonds d'écran

    /// 


    /// </summary>
    /// 
    public partial class Menu : Page
        private readonly DatabaseService db = new DatabaseService();
        private Game game;
        private Card cardHandler;
        private readonly List<string> backgroundImages = new List<string>
        "/ChatGPT Image 10 avr. 2025, 15_16_54.png",
        "/e3867127-4eec-4261-8cef-161b51c19b87.png",
        "/9f04cc4e-12cd-4c9f-8602-6dbc24c8bc2a.png",
};
        private int currentBackgroundIndex = 0;

        public Menu()
        {
            InitializeComponent();
            game = new Game();
            LoadGrid();
            RefreshLeaderboard();
        }

        private void NameOverlayOkButton_Click(object sender, RoutedEventArgs e)

        {



            {






            if (current == null)
                current = db.CreateUser(name);

                current = db.CreateUser(name);

            var flipAnimation = (Storyboard)this.FindResource("FlipCardStoryboard");
            var flipAnimationReverse = (Storyboard)this.FindResource("FlipCardStoryboard");

            cardHandler = new Models.Card(game, cardButtons, scoreText, current, flipAnimation, flipAnimationReverse, VictoryText);

        {


        }

        private void LoadGrid()

        {

            cardButtons = new List<Button>();

            for (int i = 0; i < 32; i++)

            {

                Button cardButton = new Button
                    Style = (Style)FindResource("CardButtonStyle"),
                    RenderTransformOrigin = new Point(0.5, 0.5),
                    RenderTransform = new ScaleTransform(1, 1)
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    Style = (Style)FindResource("CardButtonStyle")  // Ici on applique le style

                };

                Image image = new Image
                    Source = new BitmapImage(new Uri("pack://application:,,,/CardBack.png")),
                cardButton.Content = image;
                cardButton.Tag = i;
        }
        private void Card_Click(object sender, RoutedEventArgs e)
            if (cardHandler != null)
                cardHandler.Image_Click(sender, e);
                cardButton.Tag = i;

                CardGrid.Children.Add(cardButton);

                cardButton.Tag = i;




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
            background.Source = new BitmapImage(new Uri(backgroundImages[currentBackgroundIndex], UriKind.Relative));
        }

        private void RefreshLeaderboard()
        {
            List<User> top = db.GetTopUsers(10);
            var sb = new StringBuilder();
            sb.AppendLine("  #  Name        Score");
            sb.AppendLine(" ───────────────┬─────");

            int rank = 1;
            foreach (var u in top)
            {
                sb.AppendLine($"{rank,3}  {u.Nom,-10}  {u.Score,5}");
                rank++;
            }

            LeaderboardBox.Text = sb.ToString();
        }
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