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
        private int[,] patternTerr;

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
            StartScan();
            stopwatch.Stop();
            setStatistics("Время сканирования: " + Convert.ToString(stopwatch.Elapsed));


            base.Drow(myCanvas, createCell());
        }

        private Cell[,] createCell()
        {
            Cell[,] newCell = new Cell[CELLS_COUNT, CELLS_COUNT];
            for (int i = 0; i < CELLS_COUNT; i++)
            {
                for (int j = 0; j < CELLS_COUNT; j++)
                {
                    newCell[i, j].State = patternTerr[i, j];
                }
            }
            return newCell;
        }

        private void StartScan()
        {           
            ExtendedTerr = getExpansion(terrain);
            patternTerr = new int[CELLS_COUNT, CELLS_COUNT];
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
            }
            pattern = new HivePattern(CELLS_COUNT, ExtendedTerr);
            if (x > 0 && pattern.isEqually(x - 1, y))
            {
                DeletePattern(pattern, x - 1, y);
            }
            pattern = new FlasherPattern(CELLS_COUNT, ExtendedTerr);
            if (pattern.isEqually(x, y))
            {
                DeletePattern(pattern, x, y);
            }
            pattern = new GliderPattern(CELLS_COUNT, ExtendedTerr);
            if (x > 0 && pattern.isEqually(x - 1, y))
            {
                DeletePattern(pattern, x - 1, y);
            }
            pattern = new PentadecathlonPattern(CELLS_COUNT, ExtendedTerr);
            if (x > 1 && pattern.isEqually(x - 2, y))
            {
                DeletePattern(pattern, x - 2, y);
            }
        }

        private void DeletePattern(Pattern pattern, int x, int y)
        {
            for (int i = 0; i < pattern.currentHeight; i++)
            {
                for (int j = 0; j < pattern.currentWidth; j++)
                {
                    terrain[y + i, x + j].State = 0;
                    patternTerr[y + i, x + j] = 1;
                }
            }
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
