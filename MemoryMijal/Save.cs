using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
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

namespace MemoryMijal
{
    class SaveGame
    {
        private Level lvl;
        private int pointsLoad;
        private List<string> buttonContentLoad = new List<string>();
        private List<Visibility> buttonVisibilityLoad = new List<Visibility>();

        public Level Lvl
        {
            get { return lvl; }
            set { lvl = value; }
        }
        public int PointsLoad
        {
            get { return pointsLoad; }
            set { pointsLoad = value; }
        }
        public List<string> ButtonContentLoad
        {
            get { return buttonContentLoad; }
            set { buttonContentLoad = value; }
        }
        public List<Visibility> ButtonVisibiltyLoad
        {
            get { return buttonVisibilityLoad; }
            set { buttonVisibilityLoad = value; }
        }

        public void Save(object pGrid, int pPoints, Level pLvl)
        {
            Grid saveGrid = new Grid();
            saveGrid = pGrid as Grid;
            var formatter = new BinaryFormatter();
            Button saveButton = new Button();

            List<string> buttonContentSave = new List<string>();
            List<Visibility> buttonVisibilitySave = new List<Visibility>();
            for (int i = 0; i < saveGrid.Children.Count; i++)
            {
                saveButton = (Button)saveGrid.Children[i];
                buttonContentSave.Add(saveButton.Content.ToString());
                buttonVisibilitySave.Add(saveButton.Visibility);
            }

            using (Stream fileSteam = new FileStream("Savegame.bin", FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(fileSteam, buttonContentSave);
                formatter.Serialize(fileSteam, buttonVisibilitySave);
                formatter.Serialize(fileSteam, pPoints);
                formatter.Serialize(fileSteam, pLvl);
            }

            MessageBox.Show("The game has been saved.", "Saving");
        }

        public void Load()
        {
            var formatter = new BinaryFormatter();
            Level bufferLvl;
            int bufferPointsLoad;
            List<string> bufferButtonContentLoad = new List<string>();
            List<Visibility> bufferButtonVisibilityLoad = new List<Visibility>();

            using (Stream fileSteam = new FileStream("Savegame.bin", FileMode.Open, FileAccess.Read))
            {
                bufferButtonContentLoad = (List<string>)formatter.Deserialize(fileSteam);
                bufferButtonVisibilityLoad = (List<Visibility>)formatter.Deserialize(fileSteam);
                bufferPointsLoad = (int)formatter.Deserialize(fileSteam);
                bufferLvl = (Level)formatter.Deserialize(fileSteam);
            }
            Lvl = bufferLvl;
            PointsLoad = bufferPointsLoad;
            ButtonContentLoad = bufferButtonContentLoad;
            ButtonVisibiltyLoad = bufferButtonVisibilityLoad;
        }
    }
}
