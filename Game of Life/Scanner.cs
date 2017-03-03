using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace Game_of_Life
{

    public class PatternInfoEventArgs : EventArgs
    {
        public PatternInfoEventArgs(int x, int y, string kindof)
        {
            X = x;
            Y = y;
            KindOfPattern = kindof;
        }

        public int X { get; }
        public int Y { get; }
        public string KindOfPattern { get; }
    }

    class Scanner
    {
        public event EventHandler<PatternInfoEventArgs> PatternDetected;
        Terrain terr;
        LifeForm ThatWindow;
        private int STEP;
        private int CELLS;
        private int[,] ExtendedTerr;

        public Scanner(Terrain terrain, LifeForm window)
        {
            terr = terrain;
            ThatWindow = window;
            //terr.TurnFinished += StartScan;
            CELLS = terr.CELLS1;
            STEP = (int)(ThatWindow.Width / CELLS);
            int[,] ExtendedTerr = getExpansion(terr.terrain);
        }

        private void StartScan(object sender, TurnFinishedInfoEventArgs e)
        {
            var cells = ThatWindow.myCanvas.Children.OfType<Rectangle>().ToList();
            foreach (var cell in cells)
            {
                int x = (int)Canvas.GetLeft(cell)/STEP;
                int y = (int)Canvas.GetTop(cell)/STEP;
                isPattern(x + 1, y + 1);
            }
        }

        private void isPattern(int x, int y)
        {
            if (isBlock(x-1,y-1))
            {

                PatternDetected?.Invoke(this, new PatternInfoEventArgs(x, y, "Block"));
            }
            else if (isHive(x-1, y-1))
            {

                PatternDetected?.Invoke(this, new PatternInfoEventArgs(x, y, "Hive"));
            }
            else if (isFlasher(x-1, y-1))
            {

                PatternDetected?.Invoke(this, new PatternInfoEventArgs(x, y, "Flasher"));
            }
            else if (isGlider(x-1, y-1))
            {

                PatternDetected?.Invoke(this, new PatternInfoEventArgs(x, y, "Glider"));
            }
            else if (isPentaDecathlon(x-1,y-1))
            {

                PatternDetected?.Invoke(this, new PatternInfoEventArgs(x, y, "PentaDecathlon"));
            }
        }

        private bool isPentaDecathlon(int x, int y)
        {
            int[,] pentadecathlon =
            {
                {0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,1,0,0,0,0,1,0,0,0 },
                {0,1,1,0,1,1,1,1,0,1,1,0 },
                {0,0,0,1,0,0,0,0,1,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0 }
            };
            return isEqually(pentadecathlon, 5, 12, x, y);
        }

        private bool isGlider(int x, int y)
        {
            int[,] glider =
            {
                { 0,0,0,0,0},
                { 0,0,1,0,0},
                { 0,0,0,1,0},
                { 0,1,1,1,0},
                { 0,0,0,0,0}
            };
            return isEqually(glider, 5, 5, x, y);
        }

        private bool isFlasher(int x, int y)
        {
            int[,] flasher =
            {
                {0, 0, 0 },
                {0, 1, 0 },
                {0, 1, 0 },
                {0, 1, 0 },
                {0, 0, 0 }
            };
            return isEqually(flasher, 5, 3, x, y);
        }

        private bool isEqually(int[,] mas, int I, int J, int x, int y)
        {
            for (int i = 0; i < I; i++)
            {
                for (int j = 0; j < J; j++)
                {
                    if (mas[i, j] != ExtendedTerr[x, y])
                    {
                        return false;
                    }
                }
            }
            return true;
        }



        private bool isHive(int x, int y)
        {
            int[,] hive =
            {
                { 0, 0, 0, 0, 0},
                { 0, 0, 1, 0, 0},
                { 0, 1, 0, 1, 0},
                { 0, 1, 0, 1, 0},
                { 0, 0, 1, 0, 0},
                { 0, 0, 0, 0, 0}
            };
            return isEqually(hive, 6, 5, x, y);
        }

        //private bool isBlock(int x, int y) => ExtendedTerr[x - 1, y] == 0 && ExtendedTerr[x - 1, y - 1] == 0 
        //    && ExtendedTerr[x, y - 1] == 0 && ExtendedTerr[x - 1, y + 1] == 0 && ExtendedTerr[x + 1, y - 1] == 0 
        //    && ExtendedTerr[x + 2, y - 1] == 0 && ExtendedTerr[x + 2, y] == 0 && ExtendedTerr[x + 2, y + 1] == 0 
        //    && ExtendedTerr[x + 2, y + 2] == 0 && ExtendedTerr[x + 1, y + 2] == 0 && ExtendedTerr[x, y + 2] == 0 
        //    && ExtendedTerr[x - 1, y + 2] == 0 && ExtendedTerr[x, y + 1] == 1 && ExtendedTerr[x + 1, y] == 1 && ExtendedTerr[x + 1, y + 1] == 1;

        private bool isBlock(int x, int y)
        {
            int[,] block =
            {
                {0, 0, 0, 0 },
                {0, 1, 1, 0 },
                {0, 1, 1, 0 },
                {0, 0, 0, 0 }
            };
            return isEqually(block, 4, 4, x, y);
        }

        private int[,] getExpansion(int[,] What)
        {

            int[,] forWhat = new int[CELLS + 2, CELLS + 2];
            for (int i = 0; i < CELLS + 2; i++)
            {
                for (int j = 0; j < CELLS + 2; j++)
                {
                    if (i == 0 || i == CELLS + 1)
                    {
                        forWhat[i, j] = 0;
                    }
                    else
                        forWhat[i, j] = What[i - 1, j - 1];
                }
            }

            return forWhat;
        }
    }
}
