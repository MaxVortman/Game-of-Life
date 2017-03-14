using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Game_of_Life
{
    class RedCell : Cell
    {
        public RedCell(int Y, int X) : base(Y, X)
        {
            color = Brushes.Red;
        }

        public override int isAction()
        {
            throw new NotImplementedException();
        }
    }
}