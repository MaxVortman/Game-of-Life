﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class HivePattern : Pattern
    {
        public HivePattern(int cells, int[,] ter) : base(cells, ter)
        {
            int[,] hive =
            {
                {0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0},
                {0,0,0,1,0,0,0},
                {0,0,1,0,1,0,0},
                {0,0,1,0,1,0,0},
                {0,0,0,1,0,0,0},
                {0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0}
            };
            mas = hive;
            height = 8;
            width = 7;
            currentHeight = 4;
            currentWidth = 3;
        }
    }
}
