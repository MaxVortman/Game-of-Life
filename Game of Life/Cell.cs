using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_of_Life
{
    abstract class Cell
    {
        protected Cell[,] neighbors;
        protected int state;
        public int act { get; protected set; } //Appear(true) or disappear(false)
        protected const byte NUMBER_OF_NEIGHBORS = 3;
        protected int X;
        protected int Y;

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

        public abstract int isAction();

        protected bool exactlyThree() => getNeighbor(0, 0) + getNeighbor(0, 1) + getNeighbor(0, 2) +
            getNeighbor(1, 0) + getNeighbor(1, 2) + getNeighbor(2, 0) + getNeighbor(2, 1) + getNeighbor(2, 2) == 3;

        protected bool lessThanTwoORmoreThanThree()
        {
            int CountNeigbour = getNeighbor(0, 0) + getNeighbor(0, 1) + getNeighbor(0, 2) +
            getNeighbor(1, 0) + getNeighbor(1, 2) + getNeighbor(2, 0) + getNeighbor(2, 1) + getNeighbor(2, 2);
            if (CountNeigbour > 3 || CountNeigbour < 2)
            {
                return true;
            }
            return false;
        }

        protected int getNeighbor(int i, int j)
        {
            if (neighbors[i, j] != null)
            {
                return neighbors[i, j].State;
            }
            return 0;
        }
    }
}