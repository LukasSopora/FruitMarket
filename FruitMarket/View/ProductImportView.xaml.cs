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
    /// Interaction logic for ProductImportView.xaml
    /// </summary>
    public partial class ProductImportView : UserControl
    {
        public ProductImportView()
        {
            DataContext = new ProductImportViewModel();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProductImportViewModel pivm = DataContext as ProductImportViewModel;
            if(sender.GetType() == typeof(Button))
            {
                if(((Button)sender).DataContext.GetType() == typeof(Product))
                {
                    Product fruit = (Product)((Button)sender).DataContext;
                    pivm.Fruits.Remove(fruit);
                }
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender.GetType() == typeof(DataGrid))
            {
                ((DataGrid)sender).UnselectAllCells();
            }
        }
    }
}
