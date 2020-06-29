using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für SingelplayerMenu.xaml
    /// </summary>
    public partial class SingelplayerMenu : Page
    {
        public SingelplayerMenu()
        {
            InitializeComponent();
        }

        private void btnSingelBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Play());
        }

        private void btnLevel1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Level1(false));
        }

        private void btnLevel2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Level2(false));
        }

        private void btnSingelLoad_Click(object sender, RoutedEventArgs e)
        {
            SaveGame saveGame = new SaveGame();
            saveGame.Load();
        }
    }
}
