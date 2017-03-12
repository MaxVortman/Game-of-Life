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
        private int[,] ExtendedTerr;

        public ScannerTerrainDecorator(Canvas myCanvas, Terrain terr) : base(terr)
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

        private void StartScan()
        {           
            ExtendedTerr = getExpansion(terrain);
            for (int i = 0; i < CELLS_COUNT; i++)
                for (int j = 0; j < CELLS_COUNT; j++)
                    if (terrain[i, j].State == 1)
                        isPattern(i, j);
        }

        private void isPattern(int x, int y)
        {
            if (isBlock(x, y))
            {
                
            }
            else if (x > 0 && isHive(x - 1, y))
            {
                
            }
            else if (isFlasher(x, y))
            {
                
            }
            else if (x > 0 && isGlider(x - 1, y))
            {
               
            }
            else if (x > 1 && isPentaDecathlon(x - 2, y))
            {
                
            }
        }

        private bool isPentaDecathlon(int x, int y)
        {
            Pattern 
            return isEqually(Pentadecathlon, x, y);
        }

        private bool isGlider(int x, int y) => isEqually(Glider, x, y);

        private bool isFlasher(int x, int y) => isEqually(Flasher, x, y);

        private bool isBlock(int x, int y) => isEqually(Block, x, y);

        private bool isHive(int x, int y) => isEqually(Hive, x, y);

        private bool isEqually(Pattern pattern, int x, int y)
        {
            for (int i = 0; i < pattern.height; i++)
            {
                for (int j = 0; j < pattern.width; j++)
                {
                    if (y + j > CELLS_COUNT + 3 || x + i > CELLS_COUNT + 3 || pattern.mas[i, j] != ExtendedTerr[x + i, y + j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        int[,] forWhat;
        private int[,] getExpansion(Cell[,] terr)
        {            
            forWhat = new int[CELLS_COUNT + 4, CELLS_COUNT + 4];
            for (int i = 0; i < CELLS_COUNT + 4; i++)
            {
                for (int j = 0; j < CELLS_COUNT + 4; j++)
                {
                    if (i < 2 || i > CELLS_COUNT + 1 || j < 2 || j > CELLS_COUNT + 1)
                    {
                        forWhat[i, j] = 0;
                    }
                    else
                        forWhat[i, j] = terr[i - 2, j - 2].State;
                }
            }

            return forWhat;
        }
    }
}
