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
    public class GraphicalApp
    {

        private int CELLS_COUNT;
        private double WIDTH;
        private double HEIGHT;
        Setting_Page settings = new Setting_Page();
        LifeForm ThatWindow;
        FavoritesForm favor;
        private Line myLine;
        private int STEP;


        public GraphicalApp(LifeForm thatWindow, FavoritesForm favor)
        {
            ThatWindow = thatWindow;
            this.favor = favor;
            WIDTH = ThatWindow.Width;
            HEIGHT = ThatWindow.Height;
            CELLS_COUNT = settings.CellsCount;
            STEP = (int)(WIDTH / CELLS_COUNT);
        }


        public void DrowGrid(Canvas myCanvas)
        {
            for (int i = 0; i <= CELLS_COUNT; i++)
            {
                DrowLine(i * STEP, i * STEP, 0, CELLS_COUNT * STEP, myCanvas);
                DrowLine(0, CELLS_COUNT * STEP, i * STEP, i * STEP, myCanvas);
            }
        }

        private void DrowLine(int x1, int x2, int y1, int y2, Canvas myCanvas)
        {
            // Add a Line Element
            myLine = new Line();
            myLine.Stroke = Brushes.LightSteelBlue;
            myLine.X1 = x1;
            myLine.X2 = x2;
            myLine.Y1 = y1;
            myLine.Y2 = y2;
            myLine.StrokeThickness = 1;
            myCanvas.Children.Add(myLine);
        }

        private void DrowRectangle(int y1, int x1, Canvas myCanvas)
        {
            Rectangle drCell = new Rectangle();
            drCell.Stroke = Brushes.Black;
            drCell.Fill = Brushes.SkyBlue;
            drCell.Height = STEP;
            drCell.Width = STEP;
            myCanvas.Children.Add(drCell);
            Canvas.SetLeft(drCell, x1);
            Canvas.SetTop(drCell, y1);
        }

        public void DrowRectanglesOnLifeForm(int[,] current)
        {
            ThatWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                  (ThreadStart)delegate ()
                  {
                      ThatWindow.myCanvas.Children.Clear();
                      DrowGrid(ThatWindow.myCanvas);                      
                      for (int i = 0; i < CELLS_COUNT; i++)
                      {
                          for (int j = 0; j < CELLS_COUNT; j++)
                          {
                              if (current[i, j] == 1)
                              {
                                  DrowRectangle(i * STEP, j * STEP, ThatWindow.myCanvas);
                              }
                          }
                      }
                  });
        }

        public void DrowRectanglesOnFavoritesForm(Pattern pattern, int x, int y)
        {
            for (int i = 0; i < pattern.height; i++)
            {
                for (int j = 0; j < pattern.width; j++)
                {
                    if (pattern.mas[i, j] == 1)
                    {
                        DrowRectangle((i + x) * STEP, (j + y) * STEP, favor.myCanvas);
                    }
                }
            }
        }
    }
}
