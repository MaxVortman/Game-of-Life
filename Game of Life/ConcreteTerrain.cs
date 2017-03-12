using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Game_of_Life
{

    class ConcreteTerrain : Terrain
    {
        public Cell[,] terrain;
        private int CELLS_COUNT;
        private int STEP;
        Random r = new Random();
        private Line myLine;
        Canvas myCanvas;


        public ConcreteTerrain(int cells_count, int step, Canvas myCanvas)
        {
            CELLS_COUNT = cells_count;
            STEP = step;
            this.myCanvas = myCanvas;
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
                    if (this.terrain[i,j].isAction())
                    {
                        terrain[i, j].State = 1;
                    }
                    else
                    {
                        terrain[i, j].State = 0;
                    }
                }
            }

            this.terrain = terrain;

            Drow(myCanvas);

            setStatistics("Колличество инфузорий: " + numberOfInfusorians() + "/n");
            setStatistics("Их процент от всех клеток: " + numberOfInfusorians() / (CELLS_COUNT * CELLS_COUNT) * 100 + "%/n");
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
                    else if (i == CELLS_COUNT)
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

        public override void Drow(Canvas myCanvas)
        {
            for (int i = 0; i < CELLS_COUNT; i++)
            {
                for (int j = 0; j < CELLS_COUNT; j++)
                {
                    if (terrain[i,j].State == 1)
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
