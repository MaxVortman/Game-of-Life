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
        LifeForm ThatWindow;
        private int CELLS_COUNT;
        private int STEP;
        private double WIDTH;
        public Thread lifeThread { get; }

        public StandartMode(LifeForm ThatWindow)
        {
            this.ThatWindow = ThatWindow;
            CELLS_COUNT = settings.CellsCount;
            WIDTH = ThatWindow.Width;
            STEP = (int)(WIDTH / CELLS_COUNT);
            terr = new ConcreteTerrain(CELLS_COUNT, STEP, ThatWindow);

            if (Setting_Page.isCheckScan)
            {
                terr = new ScannerTerrainDecorator(terr);
            }
            if (Setting_Page.isCheckStat)
            {
                terr = new StatisticsTerrainDecorator(terr);
            }
            if (Setting_Page.isCheckCells)
            {
                terr = new FramedCellsTerrainDecorator(terr);
            }
            //берем статистику от сюда terr.statistics


            lifeThread = new Thread(StartGame);
            lifeThread.Start();
        }
        

        private void StartGame()
        {
            for (;;Thread.Sleep(300))
            {
                terr.statistics = "";             
                terr.MakeTurn();
                ThatWindow.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, (ThreadStart)delegate ()
                {                  
                    ThatWindow.LabelStat.Content = terr.statistics;
                    terr.Drow(ThatWindow.myCanvas, terr.terrain);
                });
                
            }
        }

    }
}
