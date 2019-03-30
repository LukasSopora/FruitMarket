using FruitMarket.Database;
using FruitMarket.Helper;
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

        private HomeView m_HomeView = null;
        private MainListView m_MainListView = null;
        private ProductListView m_ProductListView = null;
        private ProductImportView m_ProductImportView = null;
        private ProductExportView m_ProductExportView = null;
        private FilterView m_FilterView = null;
        private DeliveryView m_DeliveryView = null;
        private bool m_DescriptionVisible = false;

        public bool DescriptionVisible
        {
            get { return m_DescriptionVisible; }
            set { SetProperty(ref m_DescriptionVisible, value); }
        }


        public UserControl ActiveView
        {
            get { return m_ActiveView; }
            set { SetProperty(ref m_ActiveView, value); }
        }

        #region Commands
        public DelegateCommand ShowHomeViewCommand { get; private set; }
        public DelegateCommand PrintSiteCommand { get; private set; }
        public DelegateCommand ShowInformationCommand { get; private set; }
        public DelegateCommand ShowHelpCommand { get; private set; }

        internal void ChangeView(ViewType p_Type)
        {
            switch(p_Type)
            {
                case ViewType.HomeView: m_ActiveView = m_HomeView; break;
                case ViewType.MainListView: m_ActiveView = m_MainListView; break;
                case ViewType.ProductListView: m_ActiveView = m_ProductListView; break;
                case ViewType.FilterView: m_ActiveView = m_FilterView; break;
                case ViewType.ProductAdmissionView: m_ActiveView = m_ProductImportView; break;
                case ViewType.ProductDeliveryView: m_ActiveView = m_ProductExportView; break;
                case ViewType.DeliveryView: m_ActiveView = m_DeliveryView; break;
            }
            RaisePropertyChanged(nameof(ActiveView));
        }

        private void InitializeCommands()
        {
            ShowHomeViewCommand = new DelegateCommand(OnShowHomeView);
            RaisePropertyChanged(nameof(ShowHomeViewCommand));
            PrintSiteCommand = new DelegateCommand(OnPrintSite);
            RaisePropertyChanged(nameof(PrintSiteCommand));
            ShowInformationCommand = new DelegateCommand(OnShowInformation);
            RaisePropertyChanged(nameof(ShowInformationCommand));
            ShowHelpCommand = new DelegateCommand(OnShowHelp);
            RaisePropertyChanged(nameof(ShowHelpCommand));
        }

        private void OnShowHelp()
        {
            if (m_DescriptionVisible == false)
            {
                m_DescriptionVisible = true;
            }
            else
            {
                m_DescriptionVisible = false;
            }
            RaisePropertyChanged(nameof(DescriptionVisible));
        }

        private void OnShowInformation()
        {
        }

        private void OnPrintSite()
        {
        }

        private void OnShowHomeView()
        {
            m_ActiveView = m_HomeView;
            RaisePropertyChanged(nameof(ActiveView));
        }
        #endregion

        private void InitializeViews()
        {
            m_HomeView = new HomeView();
            m_MainListView = new MainListView();
            m_ProductListView = new ProductListView();
            m_FilterView = new FilterView();
            m_ProductImportView = new ProductImportView();
            m_ProductExportView = new ProductExportView();
            m_DeliveryView = new DeliveryView();
        }

        public MainViewModel()
        {
            DatabaseManager.InitCountries();

            InitializeViews();
            InitializeCommands();

            m_ActiveView = m_HomeView;
            RaisePropertyChanged(nameof(ActiveView));

            m_DescriptionVisible = false;
            RaisePropertyChanged(nameof(DescriptionVisible));

        }
    }
}
