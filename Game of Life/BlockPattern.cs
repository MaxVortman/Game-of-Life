using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class BlockPattern : Pattern
    {
        public BlockPattern(int cells, int[,] ter) : base(cells, ter) 
        {
             int[,] mas =
                {
                { 0,0,0,0,0,0 },
                { 0,0,0,0,0,0 },
                { 0,0,1,1,0,0 },
                { 0,0,1,1,0,0 },
                { 0,0,0,0,0,0 },
                { 0,0,0,0,0,0 }
            };
            this.mas = mas;
            height = 6;
            width = 6;
            currentHeight = 2;
            currentWidth = 2;
        }
    }
}
