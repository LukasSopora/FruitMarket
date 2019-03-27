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
            ObservableCollection<Product> result = new ObservableCollection<Product>();

            foreach(Filter f in m_Filter)
            {
                switch(f.Criteria)
                {
                    case "Sorte": break;
                    case "Kategorie": break;
                    case "MHD": break;
                    case "Reifungsdauer": break;
                    case "Verkaufspreis": break;
                    case "Bestand": break;
                    case "Lieferant": break;
                    case "Produzent": break;
                    case "Kunde": break;
                    case "Lieferschein": break;
                    default: break;
                }
            }
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

        public FilterViewModel()
        {
            InitializeCommands();

            m_FilterCriteria = ToolConstants.FILTER_CRITERIA;
            RaisePropertyChanged(nameof(FilterCriteria));

            m_Filtered = new ObservableCollection<Product>
            {
                new Product("Banane", 3, "Kl 1", new Supplier("Bauhaus"), new Producer("Grosshaus"), DateTime.Parse("12-01-2019"), DateTime.Parse("05-04-2019"), new Mature(3, 4.0), "Griechenland", 3.0, 5.0),
                new Product("Banane", 3, "Kl 2", new Supplier("Knecht"), new Producer("Viega"), DateTime.Parse("10-01-2019"), DateTime.Parse("03-04-2019"), new Mature(2, 5.0), "Portugal", 5.0, 7.0),
                new Product("Banane", 3, "Kl 3", new Supplier("Vogel"), new Producer("Kirchhoff"), DateTime.Parse("24-01-2019"), DateTime.Parse("20-04-2019"), new Mature(1, 8.0), "Spanien", 6.0, 9.0),
            };
            RaisePropertyChanged(nameof(Filtered));
        }
    }
}
