using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
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
using System.Windows.Threading;

namespace MemoryMijal
{
    /// <summary>
    /// Interaktionslogik für Level4.xaml
    /// </summary>
    public partial class Level4 : Page
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DateTime timeStart = DateTime.Now;
        TimeSpan timeLoad;
        TimeSpan timeSave;
        public TimeSpan TimeSave
        {
            get { return timeSave; }
            set { timeSave = value; }
        }

        private bool multiplayer;
        private bool playerOne = true;
        Random random = new Random();
        bool cardTwo = false;
        int points = 0;
        int turns = 0;
        int pairCounter = 25;
        int pointsMplOne = 0;
        int pointsMplTwo = 0;

        List<string> nummbers = new List<string>()
        {
            "1","1","2","2","3","3","4","4","5","5",
            "6","6","7","7","8","8","9","9","10","10",
            "11","11","12","12","13","13","14","14","15","15",
            "16","16","17","17","18","18","19","19","20","20",
            "21","21","22","22","23","23","24","24","25","25",

        };

        public Level4(bool pMultiplayer)
        {
            InitializeComponent();
            Timer();
            MultiplayerCheck(pMultiplayer);
            ButtonsGetFill();
            multiplayer = pMultiplayer;
            if (multiplayer)
            {
                btnSave.Visibility = Visibility.Hidden;
            }
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
        public Level4(bool pMultiplayer, List<string> pPuttonContentLoad, List<Visibility> pButtonVisibilityLoad, int pPointsLoad, TimeSpan pTimeSpan)
        {
            InitializeComponent();
            timeLoad = pTimeSpan;
            Timer();
            MultiplayerCheck(pMultiplayer);
            ButtonsGetFill(pPuttonContentLoad, pButtonVisibilityLoad, pPointsLoad);
            multiplayer = pMultiplayer;
            if (multiplayer)
            {
                btnSave.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonsGetFill(List<string> pPuttonContentLoad, List<Visibility> pButtonVisibilityLoad, int pPointsLoad)
        {

            Button button;
            points = pPointsLoad;
            lbPoints.Content = "Points: " + points;

            for (int i = 0; i < gridCards.Children.Count; i++)
            {
                if (gridCards.Children[i] is Button)
                    button = (Button)gridCards.Children[i];
                else
                    continue;
                button.Content = pPuttonContentLoad[i];
                button.Visibility = pButtonVisibilityLoad[i];
            }
        }
        #region Gameplay
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
                        pairCounter--;
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
                    turns++;
                    AllButtonsEnable();
                }
                if (pairCounter == 0)
                {
                    lbEndScene.Visibility = Visibility.Visible;
                    txtEndPoints.Text = points.ToString();
                    txtEndTime.Text = String.Format("{0:00}:{1:00}",
                    TimeSave.Minutes, TimeSave.Seconds, TimeSave.Milliseconds / 10);
                    txtEndTurns.Text = turns.ToString();
                    btnSave.IsEnabled = false;
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
        public void Timer()
        {
            dispatcherTimer.Tick += new EventHandler(Timer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan currentTime = DateTime.Now - timeStart + timeLoad;
            lbTimer.Content = String.Format("{0:00}:{1:00}",
            currentTime.Minutes, currentTime.Seconds, currentTime.Milliseconds / 10);
            TimeSave = currentTime;
        }
        #endregion

        #region SaveButton
        private void btnSave3_Click(object sender, RoutedEventArgs e)
        {
            SaveGame saveGrid = new SaveGame();
            saveGrid.Save(gridCards, points, Level.Level4, TimeSave);
        }
        #endregion
    }
}
