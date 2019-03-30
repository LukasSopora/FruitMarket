using FruitMarket.Model;
using FruitMarket.ViewModel;
using MaterialDesignThemes.Wpf;
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender.GetType() == typeof(DataGrid))
            {
                ((DataGrid)sender).UnselectAllCells();
            }
        }

        private void Amount_Increase(object sender, MouseButtonEventArgs e)
        {
            if(sender.GetType() == typeof(PackIcon))
            {
                if (((PackIcon)sender).DataContext.GetType() == typeof(Product))
                {
                    Product fruit = ((PackIcon)sender).DataContext as Product;
                    fruit.Amount++; 
                }
            }
        }

        private void PackIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            if(sender.GetType() == typeof(PackIcon))
            {
                PackIcon icon = sender as PackIcon;
                icon.Foreground = FindResource("PrimaryHueMidBrush") as Brush;
            }
        }

        private void PackIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(PackIcon))
            {
                PackIcon icon = sender as PackIcon;
                icon.Foreground = Brushes.White;
            }
        }

        private void Amount_Decrease(object sender, MouseButtonEventArgs e)
        {
            if (sender.GetType() == typeof(PackIcon))
            {
                if (((PackIcon)sender).DataContext.GetType() == typeof(Product))
                {
                    Product fruit = (Product)((PackIcon)sender).DataContext;
                    if(fruit.Amount > 1)
                    {
                        fruit.Amount--;
                    }
                }
            }
        }

        private void Mature_Increase(object sender, MouseButtonEventArgs e)
        {
            if (sender.GetType() == typeof(PackIcon))
            {
                if (((PackIcon)sender).DataContext.GetType() == typeof(Product))
                {
                    Product fruit = ((PackIcon)sender).DataContext as Product;
                    fruit.Mature++;
                }
            }
        }

        private void Mature_Decrease(object sender, MouseButtonEventArgs e)
        {
            if (sender.GetType() == typeof(PackIcon))
            {
                if (((PackIcon)sender).DataContext.GetType() == typeof(Product))
                {
                    Product fruit = (Product)((PackIcon)sender).DataContext;
                    if (fruit.Mature > 1)
                    {
                        fruit.Mature--;
                    }
                }
            }
        }

        private void DeleteFruit(object sender, MouseButtonEventArgs e)
        {
            ProductImportViewModel pivm = DataContext as ProductImportViewModel;
            if (sender.GetType() == typeof(PackIcon))
            {
                if (((PackIcon)sender).DataContext.GetType() == typeof(Product))
                {
                    Product fruit = (Product)((PackIcon)sender).DataContext;
                    if(pivm.Products.Contains(fruit))
                    {
                        pivm.Products.Remove(fruit);
                    }
                }
            }
        }
    }
}
