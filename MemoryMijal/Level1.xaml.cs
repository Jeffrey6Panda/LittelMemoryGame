using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
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

namespace MemoryMijal
{
    /// <summary>
    /// Interaktionslogik für Level1.xaml
    /// </summary>
    public partial class Level1 : Page
    {
        private bool multiplayer;
        Random random = new Random();
        bool cardTwo = false;
        int points = 0;

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
        Button clieckedButtonTwo;
        private void card_Click(object sender, RoutedEventArgs e)
        {
            Cards(sender);
        }
        public async Task Cards(object sender)
        {
            if (cardTwo == false)
            {
                clieckedButtonOne = sender as Button;
                clieckedButtonOne.Foreground = Brushes.Black;
                cardTwo = true;
                clieckedButtonOne.IsEnabled = false;
                return;
            }
            if (cardTwo)
            {
                clieckedButtonTwo = sender as Button;
                clieckedButtonTwo.Foreground = Brushes.Black;
                AllButtonsDisable();
                await Task.Run(Black);
                if (clieckedButtonOne.Content == clieckedButtonTwo.Content)
                {
                    points++;
                    lbPoints.Content = "Points: " + points;
                    clieckedButtonOne.Visibility = Visibility.Hidden;
                    clieckedButtonTwo.Visibility = Visibility.Hidden;
                }
                else
                {
                    clieckedButtonOne.IsEnabled = true;
                    clieckedButtonOne.Foreground = Brushes.Transparent;
                    clieckedButtonTwo.Foreground = Brushes.Transparent;
                }
                clieckedButtonOne = null;
                clieckedButtonTwo = null;
                cardTwo = false;
                AllButtonsEnable();
            }
        }
        public void Black()
        {
            Thread.Sleep(1000);
        }
        public void AllButtonsDisable()
        {
            foreach (var item in gridCards.Children.OfType<Button>())
            {
                item.IsEnabled = false;
            }
        }
        public void AllButtonsEnable()
        {
            foreach (var item in gridCards.Children.OfType<Button>())
            {
                item.IsEnabled = true;
            }
        }
    }
}
