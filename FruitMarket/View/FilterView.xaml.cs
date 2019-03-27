using FruitMarket.Model;
using FruitMarket.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FruitMarket.View
{
    /// <summary>
    /// Interaction logic for FilterView.xaml
    /// </summary>
    public partial class FilterView : UserControl
    {
        public FilterView()
        {
            DataContext = new FilterViewModel();
            InitializeComponent();
        }

        private void Delete_Filter(object sender, RoutedEventArgs e)
        {
            PackIcon btn = sender as PackIcon;
            Grid parent = btn.Parent as Grid;
            Filter filter = parent.DataContext as Filter;

            FilterViewModel fvm = DataContext as FilterViewModel;
            fvm.Filter.Remove(filter);
        }

        private void PackIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            PackIcon btn = sender as PackIcon;
            btn.Foreground = FindResource("PrimaryHueMidBrush") as Brush;
        }

        private void PackIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            PackIcon btn = sender as PackIcon;
            btn.Foreground = Brushes.White;
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            border.BorderBrush = FindResource("PrimaryHueMidBrush") as Brush;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            border.BorderBrush = Brushes.White;
        }
    }
}
