using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_of_Life
{
    class RedCell : Cell
    {
        public RedCell(int Y, int X) : base(Y, X)
        {
        }

        public override int isAction()
        {
            throw new NotImplementedException();
        }
    }
}