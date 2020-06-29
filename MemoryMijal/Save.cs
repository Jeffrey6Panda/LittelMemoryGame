using System;
using System.Collections.Generic;
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

namespace MemoryMijal
{
    class SaveGame
    {
        public void Save(object pGrid, int pPoints, string map)
        {
            Grid saveGrid = new Grid();
            saveGrid = pGrid as Grid;
            Button saveButton = new Button();
            var formatter = new BinaryFormatter();

            List<string> buttonContentSave = new List<string>();
            List<string> buttonVisibilitySave = new List<string>();
            for (int i = 0; i < saveGrid.Children.Count; i++)
            {
                saveButton = (Button)saveGrid.Children[i];
                buttonContentSave.Add(saveButton.Content.ToString());
                buttonVisibilitySave.Add(saveButton.Visibility.ToString());
            }

            using (Stream fileSteam = new FileStream("Savegame.bin", FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(fileSteam, buttonContentSave);
                formatter.Serialize(fileSteam, buttonVisibilitySave);
                formatter.Serialize(fileSteam, pPoints);
                formatter.Serialize(fileSteam, map);
            }

            MessageBox.Show("The game has been saved.", "Saving");
        }
        public void Load()
        {
            var formatter = new BinaryFormatter();
            List<string> buttonContentLoad = new List<string>();
            List<string> buttonVisibilityLoad = new List<string>();
            int pointsLoad;
            string mapLoad;

            using (Stream fileSteam = new FileStream("Savegame.bin", FileMode.Open, FileAccess.Read))
            {
                buttonContentLoad = (List<string>)formatter.Deserialize(fileSteam);
                buttonVisibilityLoad = (List<string>)formatter.Deserialize(fileSteam);
                pointsLoad = (int)formatter.Deserialize(fileSteam);
                mapLoad = (string)formatter.Deserialize(fileSteam);

                MessageBox.Show(buttonContentLoad[1].ToString());
                MessageBox.Show(buttonVisibilityLoad[1].ToString());
                MessageBox.Show(pointsLoad.ToString());
                MessageBox.Show(mapLoad);
            }
        }
    }
}
