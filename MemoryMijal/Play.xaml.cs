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
    /// Interaktionslogik für Play.xaml
    /// </summary>
    public partial class Play : Page
    {
        public Play()
        {
            InitializeComponent();
        }

        private void btnSingelPlayey_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SingelplayerMenu());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());
        }

        private void btnMutliplayer_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MultiplayerMenu());
        }
    }
}
