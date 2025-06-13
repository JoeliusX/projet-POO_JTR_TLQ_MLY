using Memory_WPF_OOP.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Memory_WPF_OOP.Models;


namespace Memory_WPF_OOP
{
    public partial class Menu : Page
    {
        private readonly DatabaseService db = new DatabaseService();
        private User current;
        private Game game;
        private List<Button> cardButtons;
        private Card cardHandler;

        private readonly List<string> backgroundImages = new List<string>
        {
            "/ChatGPT Image 10 avr. 2025, 15_16_54.png",
            "/e3867127-4eec-4261-8cef-161b51c19b87.png",
            "/9f04cc4e-12cd-4c9f-8602-6dbc24c8bc2a.png",
            "/Copilot_20250605_163819.png"
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
            string name = OverlayNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Enter your name");
                return;
            }

            pseudoText.Text = name;
            current = db.GetUserByName(name) ?? db.CreateUser(name);

            NameOverlay.Visibility = Visibility.Collapsed;

            var flipAnimation = (Storyboard)this.FindResource("FlipCardStoryboard");
            var flipAnimationReverse = (Storyboard)this.FindResource("FlipCardStoryboard");

            // Instancier Card avec les bonnes références
            cardHandler = new Models.Card(game, cardButtons, scoreText, current, flipAnimation, flipAnimationReverse, VictoryText);
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
                    Style = (Style)FindResource("CardButtonStyle"),
                    Tag = i,
                    RenderTransformOrigin = new Point(0.5, 0.5),
                    RenderTransform = new ScaleTransform(1, 1)
                };

                Image image = new Image
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/CardBack.png")),
                    Stretch = Stretch.UniformToFill
                };

                cardButton.Content = image;
                cardButton.Click += Card_Click;
                CardGrid.Children.Add(cardButton);
                cardButtons.Add(cardButton);
            }
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            if (cardHandler != null)
            {
                cardHandler.Image_Click(sender, e);
            }
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            game.Restart();
            scoreText.Text = $"Score : {game.Score}";

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
    }
}