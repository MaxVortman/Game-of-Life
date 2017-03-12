using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Game_of_Life
{
    class ScannerTerrainDecorator : TerrainDecorator
    {

        Canvas myCanvas;

        public ScannerTerrainDecorator(Canvas myCanvas)
        {
            this.myCanvas = myCanvas;
        }

        public override void MakeTurn()
        {
            base.MakeTurn();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //удаление не паттернов

            stopwatch.Stop();
            setStatistics("Время сканирования: " + Convert.ToString(stopwatch.Elapsed));


            base.Drow(myCanvas);
        }
    }
}
