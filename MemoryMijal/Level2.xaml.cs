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
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace MemoryMijal
{
    /// <summary>
    /// Interaktionslogik für Level2.xaml
    /// </summary>
    public partial class Level2 : Page
    {
        private bool multiplayer;
        private bool playerOne = true;
        Random random = new Random();
        bool cardTwo = false;
        int points = 0;
        int pointsMplOne = 0;
        int pointsMplTwo = 0;

        List<string> nummbers = new List<string>()
        {
            "1","1","2","2","3","3","4","4","5","5",
            "6","6","7","7","8","8","9","9","10","10",
        };

        public Level2(bool pMultiplayer)
        {
            InitializeComponent();
            MultiplayerCheck(pMultiplayer);
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
            #region Singelplayer
            if (multiplayer == false)
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
            #endregion

            #region Multiplayer
            if (multiplayer)
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
                        MultiplayerPonitWriter(playerOne);
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
                    if (playerOne)
                    {
                        playerOne = false;
                        lbPlayerTrun.Content = "Player Two Trun";
                        lbPlayerTrun.Foreground = Brushes.Red;
                    }
                    else
                    {
                        playerOne = true;
                        lbPlayerTrun.Content = "Player One Trun";
                        lbPlayerTrun.Foreground = Brushes.Blue;
                    }
                    AllButtonsEnable();
                }
            }
            #endregion
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
        public void MultiplayerCheck(bool pMultiplayer)
        {
            if (pMultiplayer)
            {
                lbPoints.Visibility = Visibility.Hidden;
                lbPlayerTrun.Visibility = Visibility.Visible;
                lbPointsPlayerOne.Visibility = Visibility.Visible;
                lbPointsPlayerTwo.Visibility = Visibility.Visible;
            }
        }
        public void MultiplayerPonitWriter(bool playerOne)
        {
            if (playerOne)
            {
                pointsMplOne++;
                lbPointsPlayerOne.Content = "Points: " + pointsMplOne;
            }
            else
            {
                pointsMplTwo++;
                lbPointsPlayerTwo.Content = "Points: " + pointsMplTwo;
            }
        }
    }
}