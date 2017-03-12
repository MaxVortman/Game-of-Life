using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class PentadecathlonPattern : Pattern
    {
        public PentadecathlonPattern()
        {
            int[,] pentadecathlon =
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,1,0,0,0,0,1,0,0,0,0 },
                {0,0,1,1,0,1,1,1,1,0,1,1,0,0 },
                {0,0,0,0,1,0,0,0,0,1,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
            };
            mas = pentadecathlon;
            height = 7;
            width = 14;
        }
    }
}
