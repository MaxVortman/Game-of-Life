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

        public int CELLS_COUNT;
        public int STEP;
        public Cell[,] terrain;

        public string statistics { get; protected set; }

        abstract public void Drow(Canvas myCanvas);

        abstract public void MakeTurn();

        abstract public void DrowGrid(Canvas myCanvas);

        public void setStatistics(string str)
        {
            statistics += str;
        }
    }
}
