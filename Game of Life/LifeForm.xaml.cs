﻿using System;
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
        StandartMode standart;

        public LifeForm()
        {
            InitializeComponent();


            switch (Setting_Page.GameMode)
            {
                case "Standart":
                    standart = new StandartMode(this);
                    break;                
            }
        }

        private void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                if (standart?.lifeThread != null)
                {
                    if (standart.lifeThread.IsAlive)
                    {
                        standart.lifeThread.Abort();
                    }
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigating += NavigationService_Navigating;

        }
    }
}
