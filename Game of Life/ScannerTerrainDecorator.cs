using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Game_of_Life
{
    class ScannerTerrainDecorator : TerrainDecorator
    {
        FavoritesForm favor;
        new Canvas myCanvas;
        private int[,] ExtendedTerr;
        private WhiteCell[,] newCell;

        public ScannerTerrainDecorator(FavoritesForm favor, Terrain terr) : base(terr)
        {
            this.myCanvas = favor.myCanvas;
            this.favor = favor;      
        }

        public override void MakeTurn()
        {
            base.MakeTurn();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //удаление не паттернов
            StartScan();
            stopwatch.Stop();
            setStatistics("Время сканирования: " + Convert.ToString(stopwatch.Elapsed) + "\n");

            favor.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                base.Drow(myCanvas, newCell);
            });
            
        }

        public override void Drow(Canvas myCanvas, WhiteCell[,] terrain)
        {
            base.Drow(myCanvas, terrain);
        }


        private WhiteCell[,] createCell(Pattern pattern, int x, int y)
        {
            for (int i = 0; i < pattern.height; i++)
            {
                for (int j = 0; j < pattern.width; j++)
                {
                    if (pattern.mas[i,j] == 1)
                    {
                        newCell[y + i, x + j].State = 1;
                    }
                }
            }
            return newCell;
        }

        private void StartScan()
        {           
            ExtendedTerr = getExpansion(terrain);
            newCell = new WhiteCell[CELLS_COUNT + 4, CELLS_COUNT + 4];
            for (int i = 0; i < CELLS_COUNT + 4; i++)
            {
                for (int j = 0; j < CELLS_COUNT + 4; j++)
                {
                    newCell[i, j] = new WhiteCell(i, j);
                }
            }
                    for (int i = 0; i < CELLS_COUNT; i++)
                for (int j = 0; j < CELLS_COUNT; j++)
                    if (terrain[i, j].State == 1)
                        isPattern(j, i);
        }

        private void isPattern(int x, int y)
        {
            Pattern pattern = new BlockPattern(CELLS_COUNT, ExtendedTerr);
            if (pattern.isEqually(x, y))
            {
                DeletePattern(pattern, x, y);
                createCell(pattern, x, y);

            }
            pattern = new HivePattern(CELLS_COUNT, ExtendedTerr);
            if (x > 0 && pattern.isEqually(x - 1, y))
            {
                DeletePattern(pattern, x - 1, y);
                createCell(pattern, x-1, y);
            }
            pattern = new FlasherPattern(CELLS_COUNT, ExtendedTerr);
            if (pattern.isEqually(x, y))
            {
                DeletePattern(pattern, x, y);
                createCell(pattern, x, y);
            }
            pattern = new GliderPattern(CELLS_COUNT, ExtendedTerr);
            if (x > 0 && pattern.isEqually(x - 1, y))
            {
                DeletePattern(pattern, x - 1, y);
                createCell(pattern, x-1, y);
            }
            pattern = new PentadecathlonPattern(CELLS_COUNT, ExtendedTerr);
            if (x > 1 && pattern.isEqually(x - 2, y))
            {
                DeletePattern(pattern, x - 2, y);
                createCell(pattern, x-2, y);
            }
        }

        private void DeletePattern(Pattern pattern, int x, int y)
        {
            for (int i = 0; i < pattern.currentHeight; i++)
            {
                for (int j = 0; j < pattern.currentWidth; j++)
                {
                    terrain[y + i, x + j].State = 0;
                }
            }
        }

        int[,] forWhat;
        private int[,] getExpansion(WhiteCell[,] terr)
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
