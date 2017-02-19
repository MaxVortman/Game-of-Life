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
        private Line myLine;
        private int STEP;


        public LifeForm()
        {
            InitializeComponent();
            WIDTH = this.Width;
            HEIGHT = this.Height;
            CELLS_COUNT = settings.Cells_Count;
            STEP = (int)(WIDTH / CELLS_COUNT);
            DrowCells();
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
                myGrid.Children.Add(myLine);
        }
    }
}
