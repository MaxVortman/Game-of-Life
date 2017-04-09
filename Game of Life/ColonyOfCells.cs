using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life
{
    class ColonyOfCells : IEnumerable<RedCell>
    {
        public List<RedCell> colonyOfRedCells = new List<RedCell>();
        private int COUNT_OF_CELLS;
        public int colonyDirection { get; set; }
        public int CELLS_IN_COLONY { get; private set; }
        //1 - right
        //2 - bottom
        //3 - left
        //4 - top


        public ColonyOfCells(int direction, int COUNT_OF_CELLS)
        {
            this.COUNT_OF_CELLS = COUNT_OF_CELLS;
            colonyDirection = direction;
            CELLS_IN_COLONY = 0;
        }

        public void AddCell(Cell cell)
        {
            if (!colonyOfRedCells.Contains(cell) && cell.GetType() == typeof(RedCell))
            {
                colonyOfRedCells.Add((RedCell)cell);
                CELLS_IN_COLONY++;
                ((RedCell)cell).colony = this;
            }
        }

        public void RemoveCell(Cell cell)
        {
            if (colonyOfRedCells.Contains(cell) && cell.GetType() == typeof(RedCell))
            {
                colonyOfRedCells.Remove((RedCell)cell);
            }
        }

        public bool isDeadEnd()
        {
            foreach (RedCell cell in colonyOfRedCells)
            {
                if ((colonyDirection == 1 && cell.X == COUNT_OF_CELLS - 1) || (colonyDirection == 2 && cell.Y == COUNT_OF_CELLS - 1)
                    || (colonyDirection == 3 && cell.X == 0) || (colonyDirection == 4 && cell.Y == 0))
                {
                    return true;
                }                
            }
            return false;
        }

        public IEnumerator<RedCell> GetEnumerator()
        {
            foreach (RedCell cell in colonyOfRedCells)
            {
                yield return cell;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (RedCell cell in colonyOfRedCells)
            {
                yield return cell;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ColonyOfCells)
            {
                ColonyOfCells other = (ColonyOfCells)obj;
                int count = colonyOfRedCells.Count;
                if (count == other.colonyOfRedCells.Count)
                    for (int i = 0; i < count; i++)
                    {
                        if (colonyOfRedCells[i].X != other.colonyOfRedCells[i].X || colonyOfRedCells[i].Y != other.colonyOfRedCells[i].Y)
                        {
                            return false;
                        }
                    }
                else
                    return false;
                return true;
            }
            return false;
        }

        
    }
}
