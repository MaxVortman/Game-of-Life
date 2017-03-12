﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class FlasherPattern : Pattern
    {
        public FlasherPattern()
        {
            int[,] flasher =
            {
                {0,0,0,0,0 },
                {0,0,0,0,0 },
                {0,0,1,0,0 },
                {0,0,1,0,0 },
                {0,0,1,0,0 },
                {0,0,0,0,0 },
                {0,0,0,0,0 }
            };
            mas = flasher;
            height = 7;
            width = 5;
        }
    }
}
