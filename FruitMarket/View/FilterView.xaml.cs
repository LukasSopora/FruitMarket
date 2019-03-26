using FruitMarket.Model;
using FruitMarket.ViewModel;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StackPanel parent = btn.Parent as StackPanel;
            Filter filter = parent.DataContext as Filter;

            FilterViewModel fvm = DataContext as FilterViewModel;
            fvm.Filter.Remove(filter);
        }
    }
}
