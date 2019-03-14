﻿using FruitMarket.ViewModel;
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

namespace FruitMarket
{
    /// <summary>
    /// Represents the main window of the application
    /// </summary>
    public partial class MyFirstWindow
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MyFirstWindow()
        {
            DataContext = new MainViewModel();
            this.InitializeComponent();
        }
        
        private void RibbonTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            MainViewModel mvm = DataContext as MainViewModel;
            mvm.ChangeView();
        }
    }
}
