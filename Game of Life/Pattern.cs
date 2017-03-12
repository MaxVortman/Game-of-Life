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
        protected int[,] mas;

        private bool isEqually(int x, int y, int CELLS, int[,] ExtendedTerr)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (y + j > CELLS + 3 || x + i > CELLS + 3 || mas[i, j] != ExtendedTerr[x + i, y + j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
