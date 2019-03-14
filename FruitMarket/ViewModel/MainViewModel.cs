using FruitMarket.View;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FruitMarket.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private UserControl m_ActiveView = null;
        private MainListView m_MainListView = null;
        private UserControl1_Paul m_ProductListView = null;
        private ProductAdmissionView m_ProductAdmissionView = null;
        private FilterView m_FilterView = null;

        public UserControl ActiveView
        {
            get { return m_ActiveView; }
            set { SetProperty(ref m_ActiveView, value); }
        }

        #region Commands
        public DelegateCommand ShowMainListCommand { get; private set; }
        public DelegateCommand ShowHomeViewCommand { get; private set; }
        public DelegateCommand ShowProductListCommand { get; private set; }

        internal void ChangeView()
        {
            m_ActiveView = m_ProductAdmissionView;
            RaisePropertyChanged(nameof(ActiveView));
        }

        public DelegateCommand ProductAdmissionCommand { get; private set; }
        public DelegateCommand FilterCommand { get; private set; }

        private void InitializeCommands()
        {
            ShowMainListCommand = new DelegateCommand(OnShowMainList);
            RaisePropertyChanged(nameof(ShowMainListCommand));
            ShowHomeViewCommand = new DelegateCommand(OnShowHomeView);
            RaisePropertyChanged(nameof(ShowHomeViewCommand));
            ShowProductListCommand = new DelegateCommand(OnShowProductListView);
            RaisePropertyChanged(nameof(ShowProductListCommand));
            ProductAdmissionCommand = new DelegateCommand(OnProductAdmission);
            RaisePropertyChanged(nameof(ProductAdmissionCommand));
            FilterCommand = new DelegateCommand(OnFilter);
            RaisePropertyChanged(nameof(FilterCommand));
        }

        private void OnFilter()
        {
            m_ActiveView = m_FilterView;
            RaisePropertyChanged(nameof(ActiveView));
        }

        private void OnProductAdmission()
        {
            m_ActiveView = m_ProductAdmissionView;
            RaisePropertyChanged(nameof(ActiveView));
        }

        private void OnShowProductListView()
        {
            m_ActiveView = m_ProductListView;
            RaisePropertyChanged(nameof(ActiveView));
        }

        private void OnShowHomeView()
        {
            m_ActiveView = null;
            RaisePropertyChanged(nameof(ActiveView));
        }

        private void OnShowMainList()
        {
            m_ActiveView = m_MainListView;
            RaisePropertyChanged(nameof(ActiveView));
        }
        #endregion

        private void InitializeViews()
        {
            m_MainListView = new MainListView();
            m_ProductListView = new UserControl1_Paul();
            m_ProductAdmissionView = new ProductAdmissionView();
            m_FilterView = new FilterView();
        }

        public MainViewModel()
        {
            InitializeViews();
            InitializeCommands();
        }
    }
}
