using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class Cell
    {
        //{ new Cell(), new Cell(), new Cell() },
        //{ new Cell(), null, new Cell() },
        //{ new Cell(), new Cell(), new Cell()}

        private Cell[,] neighbors;
        private int state;
        public int act { get; private set; } //Appear(true) or disappear(false)
        private const byte NUMBER_OF_NEIGHBORS = 3;
        private int X;
        private int Y;

        public int State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        public Cell(int Y, int X)
        {
            neighbors = new Cell[NUMBER_OF_NEIGHBORS, NUMBER_OF_NEIGHBORS];
            this.X = X;
            this.Y = Y;
        }

        public void setNeighbors(params Cell[] neighbors)
        {
            try
            {
                int i = 0;
                for (int j = 0; j < NUMBER_OF_NEIGHBORS; j++)
                {
                    for (int k = 0; k < NUMBER_OF_NEIGHBORS; k++)
                    {
                        this.neighbors[j, k] = neighbors[i];
                        i++;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                             
            }  
        }

        public int isAction()
        {
            if (exactlyThree() && state == 0)
            {
                act = 1;
            }
            else if (lessThanTwoORmoreThanThree() && state == 1)
            {
                act = 0;
            }
            else
            {
                act = -1;
            }

            return act;
        }

        private bool exactlyThree() => getNeighbor(0,0) + getNeighbor(0, 1) + getNeighbor(0, 2) +
            getNeighbor(1, 0) + getNeighbor(1, 2) + getNeighbor(2, 0) + getNeighbor(2, 1) + getNeighbor(2, 2) == 3;

        private bool lessThanTwoORmoreThanThree()
        {
            int CountNeigbour = getNeighbor(0, 0) + getNeighbor(0, 1) + getNeighbor(0, 2) +
            getNeighbor(1, 0) + getNeighbor(1, 2) + getNeighbor(2, 0) + getNeighbor(2, 1) + getNeighbor(2, 2);
            if (CountNeigbour > 3 || CountNeigbour < 2)
            {
                return true;
            }
            return false;
        }

        private int getNeighbor(int i, int j)
        {
            if (neighbors[i, j] != null)
            {
                return neighbors[i, j].State;
            }
            return 0;
        }
    }
}
