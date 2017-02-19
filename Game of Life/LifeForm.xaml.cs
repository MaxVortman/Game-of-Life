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

        //public LifeForm(double width, double height)
        //{
        //    InitializeComponent();
        //    CELLS_COUNT = settings.Cells_Count;
        //    WIDTH = width;
        //    HEIGHT = height;
        //}
        public LifeForm()
        {
            InitializeComponent();
            WIDTH = Width;
            HEIGHT = Height;
            CELLS_COUNT = settings.Cells_Count;
        }

        private void Drow()
        {
            for (int i = 0; i < WIDTH; i++)
            {

            }
        }
    }
}
