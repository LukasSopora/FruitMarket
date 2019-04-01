using FruitMarket.Database;
using FruitMarket.Helper;
using FruitMarket.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.ViewModel
{
    public class FilterViewModel : BindableBase
    {
        private ObservableCollection<Filter> m_Filter =
            new ObservableCollection<Filter>();
        private ObservableCollection<Product> m_Products =
            new ObservableCollection<Product>();
        private ObservableCollection<Product> m_Filtered =
            new ObservableCollection<Product>();


        private ObservableCollection<string> m_FilterCriteria =
            new ObservableCollection<string>();

        private string m_CurrentFilterCriteria = null;
        private string m_CurrentFilterText = null;

        public ObservableCollection<Product> Filtered
        {
            get { return m_Filtered; }
            set { SetProperty(ref m_Filtered, value); }
        }

        public string PageDescription
        {
            get
            {
                return ToolConstants.FILTER_VIEW_DESC;
            }
        }

        public string CurrentFilterText
        {
            get { return m_CurrentFilterText; }
            set { SetProperty(ref m_CurrentFilterText, value); }
        }

        public string CurrentFilterCriteria
        {
            get { return m_CurrentFilterCriteria; }
            set { SetProperty(ref m_CurrentFilterCriteria, value); }
        }

        public ObservableCollection<string> FilterCriteria
        {
            get { return m_FilterCriteria; }
            set { SetProperty(ref m_FilterCriteria, value); }
        }

        public ObservableCollection<Product> Products
        {
            get { return m_Products; }
            set { SetProperty(ref m_Products, value); }
        }

        public ObservableCollection<Filter> Filter
        {
            get { return m_Filter; }
            set { SetProperty(ref m_Filter, value); }
        }

        #region Commands
        public DelegateCommand AddFilterCommand { get; private set; }
        public DelegateCommand DeleteFilterCommand { get; private set; }
        public DelegateCommand DeleteAllCommand { get; private set; }
        public DelegateCommand FilterProducts { get; private set; }

        private void InitializeCommands()
        {
            AddFilterCommand = new DelegateCommand(OnAddFilter);
            RaisePropertyChanged(nameof(AddFilterCommand));
            FilterProducts = new DelegateCommand(OnFilterProducts);
            RaisePropertyChanged(nameof(FilterProducts));
        }

        private void OnFilterProducts()
        {
            m_Filtered.Clear();

            foreach(Filter f in m_Filter)
            {
                foreach(Product p in m_Products)
                {
                    switch (f.Criteria)
                    {
                        case "Sorte":
                            if (p.Sort.Contains(f.FilterText))
                            {
                                m_Filtered.Add(p);
                            }
                            break;
                        case "Kategorie":
                            if (p.Category.Contains(f.FilterText))
                            {
                                m_Filtered.Add(p);
                            }
                            break;
                        case "MHD": break;
                        case "Reifungsdauer": break;
                        case "Verkaufspreis": break;
                        case "Bestand":
                            if (p.Amount == Convert.ToInt32(f.FilterText))
                            {
                                m_Filtered.Add(p);
                            }
                            break;
                        case "Lieferant":
                        case "Produzent": break;
                        case "Kunde": break;
                        case "Lieferschein": break;
                        default: break;
                    }
                }
            }

            RaisePropertyChanged(nameof(Filtered));
        }

        private void OnAddFilter()
        {
            if(m_CurrentFilterCriteria == null || m_CurrentFilterCriteria == "")
            {
                System.Windows.MessageBox.Show("Es muss ein Filterkriterium ausgewählt sein");
                return;
            }
            if(m_CurrentFilterText == null)
            {
                m_CurrentFilterText = "";
            }
            m_Filter.Add(new Filter(m_CurrentFilterCriteria, m_CurrentFilterText));
            RaisePropertyChanged(nameof(Filter));

            m_CurrentFilterText = null;
            RaisePropertyChanged(nameof(CurrentFilterText));

            m_CurrentFilterCriteria = null;
            RaisePropertyChanged(nameof(CurrentFilterCriteria));
        }
        #endregion

        public void UpdateResources()
        {
            m_Products = ProductMapper.GetAllProducts();
        }

        public FilterViewModel()
        {
            InitializeCommands();

            DatabaseManager.GetProductListData();

            m_Products = ProductMapper.GetAllProducts();
            RaisePropertyChanged(nameof(Products));

            m_FilterCriteria = ToolConstants.FILTER_CRITERIA;
            RaisePropertyChanged(nameof(FilterCriteria));
            RaisePropertyChanged(nameof(Filtered));
        }
    }
}
