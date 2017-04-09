using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Game_of_Life
{
    abstract class Cell
    {
        public Cell[,] neighbors { get; protected set; }
        protected int state;
        public int act { get; protected set; } //Appear(true) or disappear(false)
        protected const byte NUMBER_OF_NEIGHBORS = 3;
        public int X { get; set; }
        public int Y { get; set; }
        public SolidColorBrush color { get; protected set; }

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

        public void SetNeighbors(Cell[,] neighbors)
        {
            this.neighbors = neighbors;
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

        protected bool exactlyThree() => getAliedNeighbors() == 3;

        protected bool lessThanTwoORmoreThanThree()
        {
            int CountNeigbour = getAliedNeighbors();
            if (CountNeigbour > 3 || CountNeigbour < 2)
            {
                return true;
            }
            return false;
        }

        protected int getAliedNeighbors()
        {
            return isAlly(0, 0) + isAlly(0, 1) + isAlly(0, 2) +
            isAlly(1, 0) + isAlly(1, 2) + isAlly(2, 0) + isAlly(2, 1) + isAlly(2, 2);
        }

        private int isAlly(int i, int j)
        {
            if (neighbors[i, j] != null && neighbors[i, j].GetType() == this.GetType())
            {
                return neighbors[i, j].State;
            }
            return 0;
        }

        private int isEnemy(int i, int j)
        {
            if (neighbors[i, j] != null && neighbors[i, j].GetType() != this.GetType())
            {
                return neighbors[i, j].State;
            }
            return 0;
        }

        protected int getEnemyNeighbors()
        {
            return isEnemy(0, 0) + isEnemy(0, 1) + isEnemy(0, 2) +
            isEnemy(1, 0) + isEnemy(1, 2) + isEnemy(2, 0) + isEnemy(2, 1) + isEnemy(2, 2);
        }

        public bool unificationWithTheEnemy()
        {
            if (getAliedNeighbors() + 1 < getEnemyNeighbors())
            {
                return true;
            }
            return false;
        }

        public ColonyOfCells getColonyOfRedNeighbor()
        {
            for (int i = 0; i < NUMBER_OF_NEIGHBORS; i++)
            {
                for (int j = 0; j < NUMBER_OF_NEIGHBORS; j++)
                {
                    if (neighbors[i, j] != null && neighbors[i, j].GetType() == typeof(RedCell) && ((RedCell)neighbors[i, j]).colony != null)
                    {
                        return ((RedCell)neighbors[i, j]).colony;
                    }
                }
            }
            return null;
        }
    }
}