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
        public Menu()
        {
            InitializeComponent();
            game = new Game();
            LoadGrid();
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int index = (int)clickedButton.Tag;

            game.Choose(index); // Choisir une carte

            var imageFileName = game.Cards[index];
            string imagePath = $"pack://application:,,,/Pictures/{imageFileName}";

            Image image = new Image
            {
                Source = new BitmapImage(new Uri(imagePath)),
                Stretch = Stretch.UniformToFill
            };

            clickedButton.Content = image;
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

            for (int i = 0; i < cardButtons.Count; i++)
            {
                var button = cardButtons[i];

                // Réinitialiser la carte à son dos
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
