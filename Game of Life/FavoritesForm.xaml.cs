using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game_of_Life
{
    /// <summary>
    /// Логика взаимодействия для FavoritesForm.xaml
    /// </summary>
    public partial class FavoritesForm : Window
    {

        LifeForm ThatWindow;
        GraphicalApp graphics;
        private static int[,] terrForFavor;

        public FavoritesForm(LifeForm ThatWindow)
        {
            this.ThatWindow = ThatWindow;
            this.graphics = new GraphicalApp(ThatWindow, this);
            InitializeComponent();

            //Scanner sc = new Scanner(new Terrain(), new LifeForm());
            //sc.PatternDetected += Sc_PatternDetected;
        }

        public void Sc_PatternDetected(object sender, PatternInfoEventArgs e)
        {
            ThatWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                  (ThreadStart)delegate ()
                  {
                      this.Show();
                      //x+1, y+1
                      graphics.DrowRectanglesOnFavoritesForm(e.KindOfPattern, e.X - 2, e.Y - 2);
                  });
        }
    }
}
