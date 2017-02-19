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
        private int[,] terrain;
        private int CELLS;
        Random r = new Random();
        public event EventHandler<TurnFinishedInfoEventArgs> TurnFinished;

        public Terrain(int cells)
        {
            CELLS = cells;
            CreateRandom();            
        }       

        private void CreateRandom()
        {
            terrain = new int[CELLS, CELLS];
            for (int i = 0; i < CELLS; i++)
            {
                for (int j = 0; j < CELLS; j++)
                {
                    terrain[i, j] = r.Next(0, 2);
                }
            }
        }

        public void BirthORDie()
        {
            int[,] city = new int[CELLS, CELLS];
            for (int i = 0; i < CELLS; i++)
            {
                for (int j = 0; j < CELLS; j++)
                {
                    if (this.terrain[i, j] == 0 && NearMoreThanTwo(i, j))
                    {
                        city[i, j] = 1;
                    }
                    else if (this.terrain[i,j] == 1 && LessThanThreeORmoreThanFour(i, j))
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

            TurnFinished?.Invoke(this, new TurnFinishedInfoEventArgs(terrain));
        }

        private bool NearMoreThanTwo(int i, int j) => CellsValue(i, j + 1) + CellsValue(i + 1, j + 1) + CellsValue(i + 1, j) +
            CellsValue(i + 1, j - 1) + CellsValue(i, j - 1) + CellsValue(i - 1, j - 1) + CellsValue(i - 1, j) + CellsValue(i - 1, j + 1) > 2;

        private bool LessThanThreeORmoreThanFour(int i, int j)
        {
            int CountNeigbour = CellsValue(i, j + 1) + CellsValue(i + 1, j + 1) + CellsValue(i + 1, j) +
            CellsValue(i + 1, j - 1) + CellsValue(i, j - 1) + CellsValue(i - 1, j - 1) + CellsValue(i - 1, j) + CellsValue(i - 1, j + 1);
            if (CountNeigbour > 4 || CountNeigbour < 3)
            {
                return true;
            }
            return false;
        }

        private int CellsValue(int i, int j)
        {
            if(i < 0 || j < 0 || i >= CELLS || j >= CELLS)
            {
                return 0;
            }
            return terrain[i,j];
        }
    }
}
