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
        Scanner scan;
        private int CELLS_COUNT;
        public Thread lifeThread { get; }

        public StandartMode(LifeForm ThatWindow)
        {
            CELLS_COUNT = settings.CellsCount;
            
            CELLS_COUNT = settings.CellsCount;
            terr = new Terrain();
            terr.WIDTH1 = ThatWindow.Width;
            
            scan = new Scanner(terr, ThatWindow);
            terr.TurnFinished += scan.StartScan;
            terr.TurnFinished += NewTickDrow;
            terr.CELLS1 = CELLS_COUNT;
            terr.CreateRandom();

            FavoritesForm favor = new FavoritesForm(ThatWindow);
            graphics = new GraphicalApp(ThatWindow, favor);
            scan.PatternDetected += favor.Sc_PatternDetected;



            lifeThread = new Thread(terr.StartGame);
            lifeThread.Start();
        }

        public void NewTickDrow(object sender, TurnFinishedInfoEventArgs e)
        {            
            graphics.DrowRectanglesOnLifeForm(e.CurrentCity);
        }

        
    }
}
