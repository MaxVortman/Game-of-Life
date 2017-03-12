using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class Pattern
    {
        protected int height;
        protected int width;
        public int currentHeight { get; protected set; }
        public int currentWidth { get; protected set; }
        protected int[,] mas;
        protected int CELLS;
        protected int[,] ExtendedTerr;



        public Pattern(int CELLS, int[,] ExtendedTerr)
        {
            this.CELLS = CELLS;
            this.ExtendedTerr = ExtendedTerr;
        }

        public bool isEqually(int x, int y)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (y + i > CELLS + 3 || x + j > CELLS + 3 || mas[i, j] != ExtendedTerr[y + i, x + j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
