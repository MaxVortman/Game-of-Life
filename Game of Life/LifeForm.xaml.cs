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
    /// <summary>
    /// Логика взаимодействия для LifeForm.xaml
    /// </summary>
    public partial class LifeForm : Page
    {

        private int CELLS_COUNT;
        private double WIDTH;
        private double HEIGHT;
        Setting_Page settings = new Setting_Page();
        Terrain t;
        private Line myLine;
        private int STEP;


        public LifeForm()
        {
            InitializeComponent();
            WIDTH = this.Width;
            HEIGHT = this.Height;
            CELLS_COUNT = settings.Cells_Count;
            STEP = (int)(WIDTH / CELLS_COUNT);
           // DrowCells();
            t = new Terrain(CELLS_COUNT);
            t.TurnFinished += NewTickDrow;
            Thread th = new Thread(t.StartGame);
            th.Start();
        }

        private void DrowCells()
        {
            for (int i = 0; i <= CELLS_COUNT; i++)
            {
                DrowLine(i * STEP, i * STEP, 0, CELLS_COUNT * STEP);
                DrowLine(0, CELLS_COUNT * STEP, i * STEP, i * STEP);
            }
        }

        private void DrowLine(int x1, int x2, int y1, int y2)
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

        private void DrowRectangle(int x1, int y1)
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


        public void NewTickDrow(object sender, TurnFinishedInfoEventArgs e)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                  (ThreadStart)delegate ()
                  {
                      //var rectangles = myCanvas.Children.OfType<Rectangle>().ToList();
                      //foreach (var rectangle in rectangles)
                      //{
                      //    myCanvas.Children.Remove(rectangle);
                      //}
                      myCanvas.Children.Clear();
                      DrowCells();
                      int[,] current = e.CurrentCity;
                      for (int i = 0; i < CELLS_COUNT; i++)
                      {
                          for (int j = 0; j < CELLS_COUNT; j++)
                          {
                              if (current[i, j] == 1)
                              {
                                  DrowRectangle(i * STEP, j * STEP);
                              }
                          }
                      }
                  });
        }
    }
}
