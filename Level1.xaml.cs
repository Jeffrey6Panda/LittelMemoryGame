using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemoryMijal
{
    /// <summary>
    /// Interaktionslogik für Level1.xaml
    /// </summary>
    public partial class Level1 : Page
    {
        private bool multiplayer;
        Random random = new Random();
        bool cardOne = true;

        List<string> nummbers = new List<string>()
        {
            "1","1","2","2","3","3","4","4","5","5","6","6",
        };

        public Level1(bool pMultiplayer)
        {
            InitializeComponent();
            ButtonsGetFill();
            multiplayer = pMultiplayer;
        }



        private void ButtonsGetFill()
        {
            Button button;
            int randomNumber;

            for (int i = 0; i < gridCards.Children.Count; i++)
            {
                if (gridCards.Children[i] is Button)
                    button = (Button)gridCards.Children[i];
                else
                    continue;

                randomNumber = random.Next(0, nummbers.Count);
                button.Content = nummbers[randomNumber];

                nummbers.RemoveAt(randomNumber);
            }

        }
        private void btnInGameEnd_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());
        }

        Button clieckedButtonOne;
        private void card_Click(object sender, RoutedEventArgs e)
        {
            if (cardOne)
            {
                clieckedButtonOne = sender as Button;
                clieckedButtonOne.Foreground = Brushes.Black;
            }
        }
    }
}
