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
            Console.WriteLine("Statut : " + game.status);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pseudoText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void LoadGrid()
        {
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
                    Source = new BitmapImage(new Uri("pack://application:,,,/Pictures/CardBack.png")),
                    Stretch = Stretch.UniformToFill
                };

                cardButton.Content = image;
                cardButton.Click += new RoutedEventHandler(Image_Click);
                CardGrid.Children.Add(cardButton);
            }
        }
    }
}
