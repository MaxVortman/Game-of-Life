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
        protected Terrain terrain;

        public void SetTerrain(Terrain terrain)
        {
            this.terrain = terrain;
        }

        public override void Drow(Canvas myCanvas)
        {
            terrain.Drow(myCanvas);
        }

        public override void MakeTurn()
        {
            terrain.MakeTurn();
        }

        public override void DrowGrid(Canvas myCanvas)
        {
            terrain.DrowGrid(myCanvas);
        }

        public new void setStatistics(string str)
        {
            statistics = terrain.statistics + str;
        }
    }
}
