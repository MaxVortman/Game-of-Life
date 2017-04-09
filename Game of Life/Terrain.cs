using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Game_of_Life
{
    abstract class Terrain
    {
        protected const byte NUMBER_OF_NEIGHBORS = 3;
        public int CELLS_COUNT;
        public int STEP;
        public Cell[,] terrain;
        protected Canvas myCanvas;

        public string statistics { get; set; }

        abstract public void Drow(Canvas myCanvas, Cell[,] terrain);

        abstract public void MakeTurn();

        abstract public void DrowGrid(Canvas myCanvas);

        public void setStatistics(string str)
        {
            statistics += str;
        }

        protected void UpdateNeighbors(Cell cell)
        {
            for (int i = 0; i < NUMBER_OF_NEIGHBORS; i++)
            {
                for (int j = 0; j < NUMBER_OF_NEIGHBORS; j++)
                {
                    if (cell.neighbors[i, j] != null)
                        setCellNeighbors(cell.neighbors[i, j]);
                }
            }
        }

        public void setCellNeighbors(Cell cell)
        {

            int i = cell.Y;
            int j = cell.X;

            if (i == 0 && j == 0)
            {
                cell.setNeighbors(null, null, null, null, null, terrain[i, j + 1], null, terrain[i + 1, j], terrain[i + 1, j + 1]);
            }
            else if (i == 0 && j == CELLS_COUNT - 1)
            {
                cell.setNeighbors(null, null, null, terrain[i, j - 1], null, null, terrain[i + 1, j - 1], terrain[i + 1, j], null);
            }
            else if (i == CELLS_COUNT - 1 && j == 0)
            {
                cell.setNeighbors(null, terrain[i - 1, j], terrain[i - 1, j + 1], null, null, terrain[i, j + 1], null, null, null);
            }
            else if (i == CELLS_COUNT - 1 && j == CELLS_COUNT - 1)
            {
                cell.setNeighbors(terrain[i - 1, j - 1], terrain[i - 1, j], null, terrain[i, j - 1], null, null, null, null, null);
            }
            else if (i == 0)
            {
                cell.setNeighbors(null, null, null, terrain[i, j - 1], null, terrain[i, j + 1], terrain[i + 1, j - 1], terrain[i + 1, j], terrain[i + 1, j + 1]);
            }
            else if (i == CELLS_COUNT - 1)
            {
                cell.setNeighbors(terrain[i - 1, j - 1], terrain[i - 1, j], terrain[i - 1, j + 1], terrain[i, j - 1], null, terrain[i, j + 1], null, null, null);
            }
            else if (j == 0)
            {
                cell.setNeighbors(null, terrain[i - 1, j], terrain[i - 1, j + 1], null, null, terrain[i, j + 1], null, terrain[i + 1, j], terrain[i + 1, j + 1]);
            }
            else if (j == CELLS_COUNT - 1)
            {
                cell.setNeighbors(terrain[i - 1, j - 1], terrain[i - 1, j], null, terrain[i, j - 1], null, null, terrain[i + 1, j - 1], terrain[i + 1, j], null);
            }
            else
            {
                cell.setNeighbors(terrain[i - 1, j - 1], terrain[i - 1, j], terrain[i - 1, j + 1], terrain[i, j - 1], null, terrain[i, j + 1], terrain[i + 1, j - 1], terrain[i + 1, j], terrain[i + 1, j + 1]);
            }
        }
    }
}
