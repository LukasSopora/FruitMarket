﻿using FruitMarket.Model;
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
    /// Interaction logic for ProductExportView.xaml
    /// </summary>
    public partial class ProductExportView : UserControl
    {
        public ProductExportView()
        {
            DataContext = new ProductExportViewModel();
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(DataGrid))
            {
                ((DataGrid)sender).UnselectAllCells();
            }
        }

        private void PackIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(PackIcon))
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

        private void Amount_Increase(object sender, MouseButtonEventArgs e)
        {
            if (sender.GetType() == typeof(PackIcon))
            {
                if (((PackIcon)sender).DataContext.GetType() == typeof(Product))
                {
                    Product product = ((PackIcon)sender).DataContext as Product;
                    if(product.Amount > 0)
                    {
                        product.ToExport++;
                    }
                }
            }
        }

        private void Amount_Decrease(object sender, MouseButtonEventArgs e)
        {
            if (sender.GetType() == typeof(PackIcon))
            {
                if (((PackIcon)sender).DataContext.GetType() == typeof(Product))
                {
                    Product product = (Product)((PackIcon)sender).DataContext;
                    if (product.ToExport > 1)
                    {
                        product.ToExport--;
                    }
                }
            }
        }

        private void DeleteFruit(object sender, MouseButtonEventArgs e)
        {
            ProductExportViewModel pevm = DataContext as ProductExportViewModel;
            if (sender.GetType() == typeof(PackIcon))
            {
                if (((PackIcon)sender).DataContext.GetType() == typeof(Product))
                {
                    Product product = (Product)((PackIcon)sender).DataContext;
                    if (pevm.Products.Contains(product))
                    {
                        pevm.Products.Remove(product);
                    }
                }
            }
        }

    }
}
