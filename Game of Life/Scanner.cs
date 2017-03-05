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

    public class Pattern
    {

        public int width { get; private set; }
        public int height { get; private set; }
        public int[,] mas { get; private set; }

        public Pattern(int width, int height, int[,] mas)
        {
            this.width = width;
            this.height = height;
            this.mas = mas;
        }
    }

    public class PatternInfoEventArgs : EventArgs
    {
        public PatternInfoEventArgs(int x, int y, Pattern kindof)
        {
            X = x;
            Y = y;
            KindOfPattern = kindof;
        }

        public int X { get; }
        public int Y { get; }
        public Pattern KindOfPattern { get; }
                
    }

    class Scanner
    {
        public event EventHandler<PatternInfoEventArgs> PatternDetected;
        Terrain terr;
        LifeForm ThatWindow;
        private int STEP;
        private int CELLS;
        private int[,] ExtendedTerr;


        private static int[,] pentadecathlon =
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,1,0,0,0,0,1,0,0,0,0 },
                {0,0,1,1,0,1,1,1,1,0,1,1,0,0 },
                {0,0,0,0,1,0,0,0,0,1,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
            };

        private static int[,] glider =
           {
                { 0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0},
                { 0,0,0,1,0,0,0},
                { 0,0,0,0,1,0,0},
                { 0,0,1,1,1,0,0},
                { 0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0}
            };

        private static int[,] flasher =
            {
                {0,0,0,0,0 },
                {0,0,0,0,0 },
                {0,0,1,0,0 },
                {0,0,1,0,0 },
                {0,0,1,0,0 },
                {0,0,0,0,0 },
                {0,0,0,0,0 }
            };

        private static int[,] hive =
            {
                {0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0},
                {0,0,0,1,0,0,0},
                {0,0,1,0,1,0,0},
                {0,0,1,0,1,0,0},
                {0,0,0,1,0,0,0},
                {0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0}
            };

        private static int[,] block =
            {
                {0,0,0,0,0,0 },
                {0,0,0,0,0,0 },
                {0,0,1,1,0,0 },
                {0,0,1,1,0,0 },
                {0,0,0,0,0,0 },
                {0,0,0,0,0,0 }
            };


        private static Pattern Pentadecathlon = new Pattern(14, 7, pentadecathlon);
        private static Pattern Glider = new Pattern(7, 7, glider);
        private static Pattern Flasher = new Pattern(5, 7, flasher);
        private static Pattern Hive = new Pattern(7, 8, hive);
        private static Pattern Block = new Pattern(6, 6, block);
       


        public Scanner(Terrain terrain, LifeForm window)
        {
            terr = terrain;
            ThatWindow = window;
        }

        public void StartScan(object sender, TurnFinishedInfoEventArgs e)
        {
            //ThatWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
            //      (ThreadStart)delegate ()
            //      {
            //          var cells = ThatWindow.myCanvas.Children.OfType<Rectangle>().ToList();
            //          CELLS = terr.CELLS1;
            //          STEP = (int)(ThatWindow.Width / CELLS);
            //          ExtendedTerr = getExpansion(e.CurrentCity);
            //          foreach (var cell in cells)
            //          {
            //              int x = (int)Canvas.GetLeft(cell) / STEP;
            //              int y = (int)Canvas.GetTop(cell) / STEP;
            //              isPattern(x + 1, y + 1);
            //          }
            //      });         

            CELLS = terr.CELLS1;
            STEP = (int)(terr.WIDTH1 / CELLS);
            ExtendedTerr = getExpansion(e.CurrentCity);
            for (int i = 0; i < CELLS; i++)
            {
                for (int j = 0; j < CELLS; j++)
                {
                    if (e.CurrentCity[i, j] == 1)
                    {
                        isPattern(i, j);
                    }
                }
            }

        }

        private void isPattern(int x, int y)
        {
            if (isBlock(x, y))
            {
                PatternDetected?.Invoke(this, new PatternInfoEventArgs(x, y, Block));
            }
            else if (x > 0 && isHive(x - 1, y))
            {
                PatternDetected?.Invoke(this, new PatternInfoEventArgs(x, y, Hive));
            }
            else if (isFlasher(x, y))
            {
                PatternDetected?.Invoke(this, new PatternInfoEventArgs(x, y, Flasher));
            }
            else if (x > 0 && isGlider(x - 1, y))
            {
                PatternDetected?.Invoke(this, new PatternInfoEventArgs(x, y, Glider));
            }
            else if (x > 1 && isPentaDecathlon(x - 2,y))
            {
                PatternDetected?.Invoke(this, new PatternInfoEventArgs(x, y, Pentadecathlon));
            }
        }

        private bool isPentaDecathlon(int x, int y) => isEqually(Pentadecathlon, x, y);

        private bool isGlider(int x, int y) => isEqually(Glider, x, y);

        private bool isFlasher(int x, int y) => isEqually(Flasher, x, y);

        private bool isBlock(int x, int y) => isEqually(Block, x, y);

        private bool isHive(int x, int y) => isEqually(Hive, x, y);

        private bool isEqually(Pattern pattern, int x, int y)
        {
            for (int i = 0; i < pattern.height; i++)
            {
                for (int j = 0; j < pattern.width; j++)
                {
                    if (y+j>=CELLS+2 || x+i>=CELLS+2 || pattern.mas[i, j] != ExtendedTerr[x + i, y + j])
                    {
                        return false; 
                    }
                }
            }
            return true;
        }

        private int[,] getExpansion(int[,] What)
        {

            int[,] forWhat = new int[CELLS + 4, CELLS + 4];
            for (int i = 0; i < CELLS + 4; i++)
            {
                for (int j = 0; j < CELLS + 4; j++)
                {
                    if (i < 2 || i > CELLS + 1 || j < 2 || j > CELLS + 1)
                    {
                        forWhat[i, j] = 0;
                    }
                    else
                        forWhat[i, j] = What[i - 2, j - 2];
                }
            }

            return forWhat;
        }
    }
}
