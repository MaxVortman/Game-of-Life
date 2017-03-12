using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Game_of_Life
{
    class FramedCellsTerrainDecorator : TerrainDecorator
    {
        public override void Drow(Canvas myCanvas)
        {
            base.Drow(myCanvas);
            terrain.DrowGrid(myCanvas);
        }
    }
}
