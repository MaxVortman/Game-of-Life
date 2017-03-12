using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Game_of_Life
{
    abstract class TerrainDecorator : Terrain
    {
        protected Terrain terr;

        public TerrainDecorator(Terrain terr)
        {
            this.terr = terr;
            terrain = terr.terrain;
            STEP = terr.STEP;
            CELLS_COUNT = terr.CELLS_COUNT;
        }

        public void SetTerrain(Terrain terrain)
        {
            this.terr = terrain;
        }

        public override void Drow(Canvas myCanvas)
        {
            terr.Drow(myCanvas);
        }

        public override void MakeTurn()
        {
            terr.MakeTurn();
        }

        public override void DrowGrid(Canvas myCanvas)
        {
            terr.DrowGrid(myCanvas);
        }

        public new void setStatistics(string str)
        {
            statistics = terr.statistics + str;
        }
    }
}
