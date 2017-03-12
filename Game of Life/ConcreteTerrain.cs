using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Threading;

namespace Game_of_Life
{

    class ConcreteTerrain : Terrain
    {
        
        Random r = new Random();
        private Line myLine;
        LifeForm ThatWindow;

        public ConcreteTerrain(int cells_count, int step, LifeForm ThatWindow)
        {
            CELLS_COUNT = cells_count;
            STEP = step;
            this.ThatWindow = ThatWindow;
            this.myCanvas = ThatWindow.myCanvas;
            CreateRandom();
        }


        private void CreateRandom()
        {
            terrain = new Cell[CELLS_COUNT, CELLS_COUNT];
            CellsCreate(terrain);
            for (int i = 0; i < CELLS_COUNT; i++)
            {
                for (int j = 0; j < CELLS_COUNT; j++)
                {
                    terrain[i, j].State = r.Next(0, 2);
                }
            }
        }

        public override void MakeTurn()
        {
            Cell[,] terrain = new Cell[CELLS_COUNT, CELLS_COUNT];
            CellsCreate(terrain);
            for (int i = 0; i < CELLS_COUNT; i++)
            {
                for (int j = 0; j < CELLS_COUNT; j++)
                {
                    int act = this.terrain[i, j].isAction();
                    if (act == 1)
                    {
                        terrain[i, j].State = 1;
                    }
                    else if (act == 0)
                    {
                        terrain[i, j].State = 0;
                    }
                    else
                    {
                        terrain[i, j].State = this.terrain[i, j].State;
                    }
                }
            }

            this.terrain = terrain;

            //Drow(myCanvas, this.terrain);
            statistics = "";
            setStatistics("Колличество инфузорий: " + numberOfInfusorians() + "\n");
            setStatistics("Их процент от всех клеток: " + (double)numberOfInfusorians() / (double)(CELLS_COUNT * CELLS_COUNT) * 100 + "%\n");
        }

        private int numberOfInfusorians()
        {
            int count = 0;
            for (int i = 0; i < CELLS_COUNT; i++)
            {
                for (int j = 0; j < CELLS_COUNT; j++)
                {
                    count += terrain[i, j].State;
                }
            }
            return count;
        }

        private void CellsCreate(Cell[,] terrain)
        {
            for (int i = 0; i < CELLS_COUNT; i++)
            {
                for (int j = 0; j < CELLS_COUNT; j++)
                {
                    terrain[i, j] = new Cell(i, j);
                }
            }
            for (int i = 0; i < CELLS_COUNT; i++)
            {
                for (int j = 0; j < CELLS_COUNT; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        terrain[i, j].setNeighbors(null, null, null, null, null, terrain[i, j + 1], null, terrain[i + 1, j], terrain[i + 1, j + 1]);
                    }
                    else if (i == 0 && j == CELLS_COUNT - 1)
                    {
                        terrain[i, j].setNeighbors(null, null, null, terrain[i, j-1], null, null, terrain[i+1, j-1], terrain[i + 1, j], null);
                    }
                    else if (i == CELLS_COUNT - 1 && j == 0)
                    {
                        terrain[i, j].setNeighbors(null, terrain[i-1, j], terrain[i-1, j+1], null, null, terrain[i, j + 1], null, null, null);
                    }
                    else if (i == CELLS_COUNT - 1 && j == CELLS_COUNT - 1)
                    {
                        terrain[i, j].setNeighbors(terrain[i-1, j-1], terrain[i - 1, j], null, terrain[i, j-1], null, null, null, null, null);
                    }
                    else if (i == 0)
                    {
                        terrain[i, j].setNeighbors(null, null, null, terrain[i, j-1], null, terrain[i, j + 1], terrain[i+1, j-1], terrain[i+1, j], terrain[i+1, j+1]);
                    }
                    else if (i == CELLS_COUNT - 1)
                    {
                        terrain[i, j].setNeighbors(terrain[i-1, j-1], terrain[i - 1, j], terrain[i - 1, j + 1], terrain[i, j-1], null, terrain[i, j + 1], null, null, null);
                    }
                    else if (j == 0)
                    {
                        terrain[i, j].setNeighbors(null, terrain[i - 1, j], terrain[i - 1, j + 1], null, null, terrain[i, j + 1], null, terrain[i+1, j], terrain[i+1, j+1]);
                    }
                    else if (j == CELLS_COUNT -1)
                    {
                        terrain[i, j].setNeighbors(terrain[i-1, j-1], terrain[i - 1, j], null, terrain[i, j-1], null, null, terrain[i+1, j-1], terrain[i+1, j], null);
                    }
                    else
                    {
                        terrain[i, j].setNeighbors(terrain[i - 1, j - 1], terrain[i - 1, j], terrain[i-1, j+1], terrain[i, j - 1], null, terrain[i, j+1], terrain[i + 1, j - 1], terrain[i + 1, j], terrain[i+1, j+1]);
                    }
                }
            }
        }

        public override void Drow(Canvas myCanvas, Cell[,] terrain)
        {
               ThatWindow.myCanvas.Children.Clear();
               for (int i = 0; i < CELLS_COUNT; i++)
               {
                   for (int j = 0; j < CELLS_COUNT; j++)
                   {
                       if (terrain[i, j].State == 1)
                       {
                           //drowing
                           DrowRectangle(i * STEP, j * STEP, myCanvas);
                       }
                   }
               }
            
        }

        public override void DrowGrid(Canvas myCanvas)
        {
            for (int i = 0; i <= CELLS_COUNT; i++)
            {
                DrowLine(i * STEP, i * STEP, 0, CELLS_COUNT * STEP, myCanvas);
                DrowLine(0, CELLS_COUNT * STEP, i * STEP, i * STEP, myCanvas);
            }
        }

        private void DrowLine(int x1, int x2, int y1, int y2, Canvas myCanvas)
        {
            // Add a Line Element
            myLine = new Line();
            myLine.Stroke = Brushes.LightSteelBlue;
            myLine.X1 = x1;
            myLine.X2 = x2;
            myLine.Y1 = y1;
            myLine.Y2 = y2;
            myLine.StrokeThickness = 1;
            myCanvas.Children.Add(myLine);
        }

        private void DrowRectangle(int y1, int x1, Canvas myCanvas)
        {
            Rectangle drCell = new Rectangle();
            drCell.Stroke = Brushes.Black;
            drCell.Fill = Brushes.SkyBlue;
            drCell.Height = STEP;
            drCell.Width = STEP;
            myCanvas.Children.Add(drCell);
            Canvas.SetLeft(drCell, x1);
            Canvas.SetTop(drCell, y1);
        }
    }
}
