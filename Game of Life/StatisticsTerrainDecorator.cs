﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Game_of_Life
{
    class StatisticsTerrainDecorator : TerrainDecorator
    {
        public StatisticsTerrainDecorator(Terrain terr) : base(terr)
        {
        }

        public override void MakeTurn()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            base.MakeTurn();
            stopwatch.Stop();
            //вывод информации о времени и другой статистики
            setStatistics("Время затраченное на ход: " + Convert.ToString(stopwatch.Elapsed) + "\n");
        }

        public override void Drow(Canvas myCanvas, Cell[,] terrain)
        {
            base.Drow(myCanvas, terrain);
        }
    }
}
