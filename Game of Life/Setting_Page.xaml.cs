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
    /// Логика взаимодействия для Setting_Page.xaml
    /// </summary>
    public partial class Setting_Page : Page
    {

        private static int CELLS_COUNT;
        private static string GAME_MODE;

        public Setting_Page()
        {
            InitializeComponent();
        }

        public static string GameMode
        {
            get
            {
                return GAME_MODE;
            }

        }

        public int CellsCount
        {
            get
            {
                return CELLS_COUNT;
            }
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CELLS_COUNT = int.Parse(Cells_Count.Text);
            GAME_MODE = Game_Mode.Text;
            NavigationService.GoBack();
        }
    }
}
