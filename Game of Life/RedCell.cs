using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Game_of_Life
{
    class RedCell : Cell
    {

        public ColonyOfCells colony { get; set; }

        public RedCell(int Y, int X, ColonyOfCells colony) : base(Y, X)
        {
            color = Brushes.Red;
            this.colony = colony;
            colony.AddCell(this);
            state = 1;
        }


        //act = 1 рождается
        //-|- = 0 умирает
        //-|- = -1 без изменений


            //движение.
            //объединение, столкновение.

        public override int isAction()
        {

            if (lessThanTwoORmoreThanThree() && state == 1)
            {
                act = 0;
            }
            else
            {
                act = -1;
            }

            

            return act;
        }

        


        //private ColonyOfCells MaxColony(ColonyOfCells colony1, ColonyOfCells colony2)
        //{
        //    if (colony1.CELLS_IN_COLONY > colony2.CELLS_IN_COLONY)
        //    {
        //        return colony1;
        //    }
        //    else
        //    {
        //        return colony2;
        //    }
        //}
    }
}