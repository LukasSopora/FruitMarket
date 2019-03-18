using Fluent;
using FruitMarket.Helper;
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
            ThemeManager.ChangeAppTheme(this, "Dark.Red");
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
    }
}
