using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading;
using System.Windows.Threading;

namespace Game_of_Life
{
    class SpecialMode : GameModesInterface
    {

        private int CELLS_COUNT;
        private double WIDTH;
        private double HEIGHT;
        Setting_Page settings = new Setting_Page();
        LifeForm ThatWindow;
        Terrain terr;
        private int STEP;

        public SpecialMode()
        {
            CELLS_COUNT = settings.CellsCount;
            terr = new Terrain(CELLS_COUNT);
            WIDTH = ThatWindow.Width;
            HEIGHT = ThatWindow.Height;
            STEP = (int)(WIDTH / CELLS_COUNT);
        }

        public void NewTickDrow(object sender, TurnFinishedInfoEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void mouseDown(MouseButtonEventArgs e)
        {
            int posX = (int)e.GetPosition(null).X / STEP;
            int posY = (int)e.GetPosition(null).Y / STEP;
            if (terr.terrain[posX, posY] == 0)
            {
                terr.terrain[posX, posY] = 1;
            }
        }
    }
}
