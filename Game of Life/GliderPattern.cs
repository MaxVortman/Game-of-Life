using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class GliderPattern : Pattern
    {
        public GliderPattern(int cells, int[,] ter) : base(cells, ter)
        {
            int[,] glider =
           {
                { 0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0},
                { 0,0,0,1,0,0,0},
                { 0,0,0,0,1,0,0},
                { 0,0,1,1,1,0,0},
                { 0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0}
            };
            mas = glider;
            height = 7;
            width = 7;
            currentHeight = 3;
            currentWidth = 3;
        }
    }
}
