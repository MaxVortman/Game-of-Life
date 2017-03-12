using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Game_of_Life
{

    public class TurnFinishedInfoEventArgs : EventArgs
    {
        public TurnFinishedInfoEventArgs(int[,] city)
        {
            CurrentCity = city;
        }

        public int[,] CurrentCity { get; }
    }

    class Terrain
    {
        public Cell[,] terrain;
        private int CELLS;
        private double WIDTH;
        Random r = new Random();

        public int CELLS1
        {
            get
            {
                return CELLS;
            }

            set
            {
                CELLS = value;
            }
        }

        public double WIDTH1
        {
            get
            {
                return WIDTH;
            }

            set
            {
                WIDTH = value;
            }
        }

        public event EventHandler<TurnFinishedInfoEventArgs> TurnFinished;  

        public void CreateRandom()
        {
            terrain = new Cell[CELLS, CELLS];
            CellsCreate(terrain);
            for (int i = 0; i < CELLS; i++)
            {
                for (int j = 0; j < CELLS; j++)
                {
                    terrain[i, j].State = r.Next(0, 2);
                }
            }
        }

        public void StartGame()
        {           
            for (;;)
            {                
                BirthORDie();
            }
        }

        private void BirthORDie()
        {
            Cell[,] terrain = new Cell[CELLS, CELLS];
            CellsCreate(terrain);
            for (int i = 0; i < CELLS; i++)
            {
                for (int j = 0; j < CELLS; j++)
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
            //TurnFinished?.Invoke(this, new TurnFinishedInfoEventArgs(this.terrain));
            Thread.Sleep(300);
        }

        private void CellsCreate(Cell[,] terrain)
        {
            for (int i = 0; i < CELLS; i++)
            {
                for (int j = 0; j < CELLS; j++)
                {
                    terrain[i, j] = new Cell(i, j);
                }
            }
            for (int i = 0; i < CELLS; i++)
            {
                for (int j = 0; j < CELLS; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        terrain[i, j].setNeighbors(null, null, null, null, null, terrain[i, j + 1], null, terrain[i + 1, j], terrain[i + 1, j + 1]);
                    }
                    else if (i == 0 && j == CELLS - 1)
                    {
                        terrain[i, j].setNeighbors(null, null, null, terrain[i, j-1], null, null, terrain[i+1, j-1], terrain[i + 1, j], null);
                    }
                    else if (i == CELLS - 1 && j == 0)
                    {
                        terrain[i, j].setNeighbors(null, terrain[i-1, j], terrain[i-1, j+1], null, null, terrain[i, j + 1], null, null, null);
                    }
                    else if (i == CELLS - 1 && j == CELLS - 1)
                    {
                        terrain[i, j].setNeighbors(terrain[i-1, j-1], terrain[i - 1, j], null, terrain[i, j-1], null, null, null, null, null);
                    }
                    else if (i == 0)
                    {
                        terrain[i, j].setNeighbors(null, null, null, terrain[i, j-1], null, terrain[i, j + 1], terrain[i+1, j-1], terrain[i+1, j], terrain[i+1, j+1]);
                    }
                    else if (i == CELLS)
                    {
                        terrain[i, j].setNeighbors(terrain[i-1, j-1], terrain[i - 1, j], terrain[i - 1, j + 1], terrain[i, j-1], null, terrain[i, j + 1], null, null, null);
                    }
                    else if (j == 0)
                    {
                        terrain[i, j].setNeighbors(null, terrain[i - 1, j], terrain[i - 1, j + 1], null, null, terrain[i, j + 1], null, terrain[i+1, j], terrain[i+1, j+1]);
                    }
                    else if (j == CELLS -1)
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
    }
}
