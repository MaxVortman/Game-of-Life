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
        Random r = new Random();
        private int[,] ExtendedTerr;

        public ScannerTerrainDecorator(Terrain terr) : base(terr)
        {
        }

        public override void MakeTurn()
        {
            base.MakeTurn();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            StartScan();
            stopwatch.Stop();
            setStatistics("Время сканирования: " + Convert.ToString(stopwatch.Elapsed) + "\n");

           
            
        }

        public override void Drow(Canvas myCanvas, Cell[,] terrain)
        {
            base.Drow(myCanvas, terrain);
        }

        

        private void StartScan()
        {           
            ExtendedTerr = getExpansion(terrain);            
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
                FormationOfTraitors(pattern, x, y);

            }
            pattern = new HivePattern(CELLS_COUNT, ExtendedTerr);
            if (x > 0 && pattern.isEqually(x - 1, y))
            {
                FormationOfTraitors(pattern, x - 1, y);                
            }
            pattern = new FlasherPattern(CELLS_COUNT, ExtendedTerr);
            if (pattern.isEqually(x, y))
            {
                FormationOfTraitors(pattern, x, y);                
            }
            pattern = new GliderPattern(CELLS_COUNT, ExtendedTerr);
            if (x > 0 && pattern.isEqually(x - 1, y))
            {
                FormationOfTraitors(pattern, x - 1, y);
            }
            pattern = new PentadecathlonPattern(CELLS_COUNT, ExtendedTerr);
            if (x > 1 && pattern.isEqually(x - 2, y))
            {
                FormationOfTraitors(pattern, x - 2, y);                
            }
        }

        private void FormationOfTraitors(Pattern pattern, int x, int y)
        {            
            ColonyOfCells colony = new ColonyOfCells(r.Next(1, 5), CELLS_COUNT);        
            for (int i = 0; i < pattern.height; i++)
            {
                for (int j = 0; j < pattern.width; j++)
                {
                    if (pattern.mas[i,j] == 1)
                    {
                        
                        Cell cell = new RedCell(y + i-2, x + j-2, colony);
                        setCellNeighbors(cell);
                        UpdateNeighbors(cell);
                        this.terrain[y + i-2, x + j-2] = cell;
                      //  colony.AddCell(cell);
                    }
                   
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
                    if (i < 2 || i > CELLS_COUNT + 1 || j < 2 || j > CELLS_COUNT + 1 || terr[i-2, j-2] is RedCell)
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
