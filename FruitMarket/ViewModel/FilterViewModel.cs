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
        private ObservableCollection<string> m_FilterCriteria =
            new ObservableCollection<string>();

        private Filter m_CurrentFilter = null;

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

        public Filter CurrentFilter
        {
            get { return m_CurrentFilter; }
            set { SetProperty(ref m_CurrentFilter, value); }
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

        private void InitializeCommands()
        {
            AddFilterCommand = new DelegateCommand(OnAddFilter);
            RaisePropertyChanged(nameof(AddFilterCommand));
            DeleteFilterCommand = new DelegateCommand(OnDeleteFilter);
            RaisePropertyChanged(nameof(DeleteAllCommand));
            DeleteAllCommand = new DelegateCommand(OnDeleteAll);
            RaisePropertyChanged(nameof(DeleteAllCommand));
        }

        private void OnDeleteAll()
        {
            throw new NotImplementedException();
        }

        private void OnDeleteFilter()
        {
            throw new NotImplementedException();
        }

        private void OnAddFilter()
        {
            if(m_CurrentFilter == null)
            {
                return;
            }
            if(m_CurrentFilter.Criteria == null)
            {
                System.Windows.MessageBox.Show("Kein Filterkriterium ausgewählt.");
            }
            if(m_CurrentFilter.FilterText == null)
            {
                m_CurrentFilter.FilterText = "";
            }
            if(!m_Filter.Contains(m_CurrentFilter))
            {
                m_Filter.Add(m_CurrentFilter);
                RaisePropertyChanged(nameof(Filter));
            }
            else
            {
                System.Windows.MessageBox.Show("Filter ist bereits angewendet.");
            }
        }
        #endregion

        public FilterViewModel()
        {
            InitializeCommands();

            m_FilterCriteria = ToolConstants.FILTER_CRITERIA;
            RaisePropertyChanged(nameof(FilterCriteria));
        }
    }
}
