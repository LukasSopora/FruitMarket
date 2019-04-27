using FruitMarket.Database;
using FruitMarket.Helper;
using FruitMarket.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.ViewModel
{
    public class MainListViewModel : BindableBase
    {
        private ObservableCollection<ProductListData> m_ProductListData =
            new ObservableCollection<ProductListData>();

        public ObservableCollection<ProductListData> ProductListData
        {
            get { return m_ProductListData; }
            set { SetProperty(ref m_ProductListData, value); }
        }

        public string PageDescription
        {
            get
            {
                return ToolConstants.MAIN_LIST_VIEW_DESC;
            }
        }

        public void UpdateResourceData()
        {
            var products = DatabaseManager.GetAllProductListData().OrderBy(x => x.Product.Sort);
            m_ProductListData = new ObservableCollection<ProductListData>(products);
            RaisePropertyChanged(nameof(ProductListData));
        }

        public MainListViewModel()
        {
            UpdateResourceData();
        }
    }
}
