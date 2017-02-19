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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {

        public MainWindow()
        {
            this.WindowHeight = 500;
            this.WindowWidth = 500;
            InitializeComponent();
            
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LifeForm.xaml",
                UriKind.Relative));
            //LifeForm life = new LifeForm(this.WindowWidth, this.WindowHeight);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Setting_Page.xaml",
                UriKind.Relative));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
