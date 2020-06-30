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
    /// Interaktionslogik für MultiplayerMenu.xaml
    /// </summary>
    public partial class MultiplayerMenu : Page
    {
        public MultiplayerMenu()
        {
            InitializeComponent();
        }
        private void btnMultiBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Play());
        }

        private void btnLevel1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Level1(true));
        }

        private void btnLevel2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Level2(true));
        }

        private void btnLevel3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Level3(true));
        }

        private void btnLevel4_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Level4(true));
        }
    }
}
