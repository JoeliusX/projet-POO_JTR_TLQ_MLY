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
        private Game game;
        private List<Button> cardButtons;
        private bool isChecking = false;
        public Menu()
        {
            InitializeComponent();
            game = new Game();
            LoadGrid();
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

            /*DatabaseService dbService = new DatabaseService();
            dbService.InsertUser(name);*/

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
                game.ResetChoices();
                isChecking = false;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pseudoText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void LoadGrid()
        {
            // Donner de façon aléatoire une image à chaque carte entre les 32 images gardées
            cardButtons = new List<Button>();
            for (int i = 0; i < 32; i++)
            {
                Button cardButton = new Button
                {
                    Margin = new Thickness(10),
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch
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
            }
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            game.Restart();

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
        }
    }
}
