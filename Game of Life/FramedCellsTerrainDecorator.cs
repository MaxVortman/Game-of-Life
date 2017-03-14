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
        public FramedCellsTerrainDecorator(Terrain terr) : base(terr)
        {
        }

        public override void Drow(Canvas myCanvas, Cell[,] terrain)
        {
            base.Drow(myCanvas, terrain);
            base.DrowGrid(myCanvas);
        }

        public override void MakeTurn()
        {
            base.MakeTurn();
            this.statistics = terr.statistics;
        }
    }
}
