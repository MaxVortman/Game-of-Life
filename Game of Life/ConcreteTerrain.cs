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
        List<ColonyOfCells> movingColonies = new List<ColonyOfCells>();

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
            int[,] terrain = new int[CELLS_COUNT, CELLS_COUNT];
            for (int i = 0; i < CELLS_COUNT; i++)
            {
                for (int j = 0; j < CELLS_COUNT; j++)
                {


                    if (this.terrain[i, j].State == 1)
                    {
                        if (this.terrain[i, j].unificationWithTheEnemy())
                        {
                            swapSide(ref this.terrain[i, j]);
                            if (this.terrain[i, j].GetType() == typeof(RedCell))
                            {
                                SetRandomDirection((RedCell)this.terrain[i, j]);
                            }
                        }
                        else if (this.terrain[i, j].GetType() == typeof(RedCell) && isColonyCompound(((RedCell)this.terrain[i, j])))
                        {
                            SetRandomDirection((RedCell)this.terrain[i, j]);
                        }
                        else
                        //mashing red cells for moving 
                        if (this.terrain[i, j].GetType() == typeof(RedCell))
                        {
                            if (((RedCell)this.terrain[i, j]).colony.isDeadEnd())
                            {
                                SetRandomDirection(((RedCell)this.terrain[i, j]));
                            }
                            else
                            {
                                movingColonies.Add(((RedCell)this.terrain[i, j]).colony);
                                MashingRedCells(((RedCell)this.terrain[i, j]).colony);
                            }
                        } 
                    }



                    int act = this.terrain[i, j].isAction();
                    if (act == 1)
                    {
                        terrain[i, j] = 1;
                       // this.terrain[i, j] = new WhiteCell(i, j);
                    }
                    else if (act == 0)
                    {
                        terrain[i, j] = 0;
                    }
                    else
                    {
                        terrain[i, j] = this.terrain[i, j].State;
                    }



                }
            }
          
            for (int i = 0; i < CELLS_COUNT; i++)
            {
                for (int j = 0; j < CELLS_COUNT; j++)
                {
                    this.terrain[i, j].State = terrain[i, j];
                }
            }

            MovingColonies();

            statistics = "";
            setStatistics("Колличество инфузорий: " + numberOfInfusorians() + "\n");
            setStatistics("Их процент от всех клеток: " + (double)numberOfInfusorians() / (double)(CELLS_COUNT * CELLS_COUNT) * 100 + "%\n");
        }

        private bool isColonyCompound(RedCell red)
        {
            bool flag = false;
            foreach (Cell neighbor in red.neighbors)
            {
                if (neighbor != null && neighbor.State == 1 && neighbor.GetType() == typeof(RedCell) && !red.colony.Equals(((RedCell)neighbor).colony))
                {
                    foreach (var cell in ((RedCell)neighbor).colony)
                    {
                        red.colony.AddCell(cell);
                        ////RedCell new_cell = new RedCell(cell.Y, cell.X, red.colony);
                        ////setCellNeighbors(new_cell);
                        //UpdateNeighbors(new_cell);
                        ////this.terrain[new_cell.Y, new_cell.X] = new_cell;                       
                    }
                    flag = true;
                }
            }
            return flag;
        }

        
        private void MovingColonies()
        {
            //1 - right
            //2 - bottom
            //3 - left
            //4 - top
            foreach (ColonyOfCells colony in movingColonies)
            {
                ColonyOfCells new_colony = new ColonyOfCells(colony.colonyDirection, CELLS_COUNT);
                foreach (RedCell cell in colony)
                {
                    RedCell red = null;
                    switch (colony.colonyDirection)
                    {
                        case 1:
                            red = new RedCell(cell.Y, cell.X + 1, new_colony);
                            break;
                        case 2:
                            red = new RedCell(cell.Y + 1, cell.X, new_colony);
                            break;
                        case 3:
                            red = new RedCell(cell.Y, cell.X - 1, new_colony);
                            break;
                        case 4:
                            red = new RedCell(cell.Y - 1, cell.X, new_colony);
                            break;
                    }

                    setCellNeighbors(red);                    
                    this.terrain[red.Y, red.X] = red;
                    UpdateNeighbors(terrain[red.Y, red.X]);
                }
                foreach (RedCell cell in new_colony)
                {
                    if (cell.isAction() == 0)
                    {
                        Cell new_cell = new WhiteCell(cell.Y, cell.X);
                        setCellNeighbors(new_cell);
                        terrain[cell.Y, cell.X] = new_cell;
                        UpdateNeighbors(terrain[cell.Y, cell.X]);
                    }
                }
            }
            movingColonies = new List<ColonyOfCells>();
        }

        private void MashingRedCells(ColonyOfCells colony)
        {
            foreach (RedCell cell in colony)
            {
                Cell new_cell = new WhiteCell(cell.Y, cell.X);
                setCellNeighbors(new_cell);
                terrain[cell.Y, cell.X] = new_cell;
                UpdateNeighbors(terrain[cell.Y, cell.X]);
            }
        }

        private void SetRandomDirection(RedCell red)
        {
            int rand = r.Next(1, 5);
            while (rand == red.colony.colonyDirection)
            {
                rand = r.Next(1, 5);
            }
            red.colony.colonyDirection = rand;
        }

        

        private void swapSide(ref Cell cell)
        {
            if (cell.GetType() == typeof(WhiteCell))
            {
                cell = new RedCell(cell.Y, cell.X, cell.getColonyOfRedNeighbor());
            }
            else
            {
                cell = new WhiteCell(cell.Y, cell.X);
            }

            setCellNeighbors(cell);
            UpdateNeighbors(cell);
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
                    terrain[i, j] = new WhiteCell(i, j);
                }
            }
            for (int i = 0; i < CELLS_COUNT; i++)
            {
                for (int j = 0; j < CELLS_COUNT; j++)
                {
                    setCellNeighbors(terrain[i, j]);
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
                           DrowRectangle(i * STEP, j * STEP, terrain[i,j].color, myCanvas);
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

        private void DrowRectangle(int y1, int x1, SolidColorBrush color, Canvas myCanvas)
        {
            Rectangle drCell = new Rectangle();
            drCell.Stroke = Brushes.Black;
            drCell.Fill = color;
            drCell.Height = STEP;
            drCell.Width = STEP;
            myCanvas.Children.Add(drCell);
            Canvas.SetLeft(drCell, x1);
            Canvas.SetTop(drCell, y1);
        }
    }
}
