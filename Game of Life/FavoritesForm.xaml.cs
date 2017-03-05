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

        public FavoritesForm(LifeForm ThatWindow)
        {
            this.ThatWindow = ThatWindow;
            this.graphics = new GraphicalApp(ThatWindow, this);
            InitializeComponent();
            graphics.DrowGrid(myCanvas);
            //this.Height = myCanvas.Height;
            //this.Width = myCanvas.Width;
        }

        public void Sc_PatternDetected(object sender, PatternInfoEventArgs e)
        {
            ThatWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                  (ThreadStart)delegate ()
                  {
                      this.Show();
                      graphics.DrowRectanglesOnFavoritesForm(e.KindOfPattern, e.X - 2, e.Y - 2);
                  });
        }
    }
}
