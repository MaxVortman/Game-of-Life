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
        GraphicalApp graphics;
        Terrain terr;
        private int CELLS_COUNT;

        public StandartMode(LifeForm ThatWindow)
        {
            CELLS_COUNT = settings.CellsCount;
            graphics = new GraphicalApp(ThatWindow);
            CELLS_COUNT = settings.CellsCount;
            terr = new Terrain(CELLS_COUNT);
            terr.TurnFinished += NewTickDrow;
            Thread lifeThread = new Thread(terr.StartGame);
            lifeThread.Start();
        }

        public void NewTickDrow(object sender, TurnFinishedInfoEventArgs e)
        {            
            graphics.DrowRectangles(e.CurrentCity);
        }

        
    }
}
