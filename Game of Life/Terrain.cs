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
        public int[,] terrain;
        private int CELLS;
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

        public event EventHandler<TurnFinishedInfoEventArgs> TurnFinished;  

        public void CreateRandom()
        {
            terrain = new int[CELLS1, CELLS1];
            for (int i = 0; i < CELLS1; i++)
            {
                for (int j = 0; j < CELLS1; j++)
                {
                    terrain[i, j] = r.Next(0, 2);
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
            int[,] city = new int[CELLS1, CELLS1];
            for (int i = 0; i < CELLS1; i++)
            {
                for (int j = 0; j < CELLS1; j++)
                {
                    if (this.terrain[i, j] == 0 && exactlyThree(i, j))
                    {
                        city[i, j] = 1;
                    }
                    else if (this.terrain[i,j] == 1 && lessThanTwoORmoreThanThree(i, j))
                    {
                        city[i, j] = 0;
                    }
                    else
                    {
                        city[i, j] = terrain[i, j];
                    }
                }
            }

            this.terrain = city;
            Thread.Sleep(500);
            TurnFinished?.Invoke(this, new TurnFinishedInfoEventArgs(terrain));
        }

        private bool exactlyThree(int i, int j) => CellsValue(i, j + 1) + CellsValue(i + 1, j + 1) + CellsValue(i + 1, j) +
            CellsValue(i + 1, j - 1) + CellsValue(i, j - 1) + CellsValue(i - 1, j - 1) + CellsValue(i - 1, j) + CellsValue(i - 1, j + 1) == 3;

        private bool lessThanTwoORmoreThanThree(int i, int j)
        {
            int CountNeigbour = CellsValue(i, j + 1) + CellsValue(i + 1, j + 1) + CellsValue(i + 1, j) +
            CellsValue(i + 1, j - 1) + CellsValue(i, j - 1) + CellsValue(i - 1, j - 1) + CellsValue(i - 1, j) + CellsValue(i - 1, j + 1);
            if (CountNeigbour > 3 || CountNeigbour < 2)
            {
                return true;
            }
            return false;
        }

        private int CellsValue(int i, int j)
        {
            if(i < 0 || j < 0 || i >= CELLS1 || j >= CELLS1)
            {
                return 0;
            }
            return terrain[i,j];
        }
    }
}
