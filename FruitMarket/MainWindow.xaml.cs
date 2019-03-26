using FruitMarket.Helper;
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
            if(sender.GetType() == typeof(Fluent.RibbonTabItem))
            {
                MainViewModel mvm = DataContext as MainViewModel;
                Fluent.RibbonTabItem item = sender as Fluent.RibbonTabItem;
                ViewType type;

                switch(item.Header)
                {
                    case "Hauptmenü": type = ViewType.HomeView; break;
                    case "Lager": type = ViewType.MainListView; break;
                    case "Produkte": type = ViewType.ProductListView; break;
                    case "Suche": type = ViewType.FilterView; break;
                    case "Einkauf": type = ViewType.ProductAdmissionView; break;
                    case "Verkauf": type = ViewType.ProductDeliveryView; break;
                    default: type = ViewType.HomeView; break;
                }

                mvm.ChangeView(type);
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if(sender.GetType() == typeof(Button))
            {
                Button btn = sender as Button;
                if(btn.Content.GetType() == typeof(PackIcon))
                {
                    PackIcon icon = btn.Content as PackIcon;
                    icon.Foreground = Brushes.DarkCyan;
                }

            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button btn = sender as Button;
                if (btn.Content.GetType() == typeof(PackIcon))
                {
                    PackIcon icon = btn.Content as PackIcon;
                    icon.Foreground = Brushes.Black;
                }

            }
        }
    }
}
