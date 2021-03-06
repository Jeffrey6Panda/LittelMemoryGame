﻿using System;
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

        private void btnLevel3_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Level3(false));
        }

        private void btnLevel4_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Level4(false));
        }
        private void btnSingelLoad_Click(object sender, RoutedEventArgs e)
        {
            SaveGame sg = new SaveGame();
            sg.Load();
            switch (sg.Lvl)
            {
                case Level.Level1:
                    this.NavigationService.Navigate(new Level1(false, sg.ButtonContentLoad, sg.ButtonVisibiltyLoad, sg.PointsLoad, sg.TimeLoad));
                    break;
                case Level.Level2:
                    this.NavigationService.Navigate(new Level2(false, sg.ButtonContentLoad, sg.ButtonVisibiltyLoad, sg.PointsLoad, sg.TimeLoad));
                    break;
                case Level.Level3:
                    this.NavigationService.Navigate(new Level3(false, sg.ButtonContentLoad, sg.ButtonVisibiltyLoad, sg.PointsLoad, sg.TimeLoad));
                    break;
                case Level.Level4:
                    this.NavigationService.Navigate(new Level4(false, sg.ButtonContentLoad, sg.ButtonVisibiltyLoad, sg.PointsLoad, sg.TimeLoad));
                    break;
                default:
                    break;
            }
        }

    }
}
