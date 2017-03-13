using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class WhiteCell : Cell
    {
        public WhiteCell(int Y, int X) : base(Y, X)
        {
        }

        //{ new Cell(), new Cell(), new Cell() },
        //{ new Cell(), null, new Cell() },
        //{ new Cell(), new Cell(), new Cell()}

        public override int isAction()
        {
            if (exactlyThree() && state == 0)
            {
                act = 1;
            }
            else if (lessThanTwoORmoreThanThree() && state == 1)
            {
                act = 0;
            }
            else
            {
                act = -1;
            }

            return act;
        }

       
    }
}
