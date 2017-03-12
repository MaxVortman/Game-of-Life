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
    class StandartMode : GameModesInterface
    {

        Setting_Page settings = new Setting_Page();
        Terrain terr;
        private int CELLS_COUNT;
        private int STEP;
        private double WIDTH;
        //public Thread lifeThread { get; }

        public StandartMode(LifeForm ThatWindow)
        {
            CELLS_COUNT = settings.CellsCount;
            WIDTH = ThatWindow.Width;
            STEP = (int)(WIDTH / CELLS_COUNT);
            terr = new ConcreteTerrain(CELLS_COUNT, STEP, ThatWindow.myCanvas);
            
            //берем статистику от сюда terr.statistics


            //lifeThread = new Thread(terr.StartGame);
            //lifeThread.Start();
        }
        
    }
}
