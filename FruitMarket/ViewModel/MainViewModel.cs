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
        UserControl m_ActiveView = null;
        MainListView m_MainListView = null;
        UserControl1_Paul m_ProductListView = null;

        public UserControl ActiveView
        {
            get { return m_ActiveView; }
            set { SetProperty(ref m_ActiveView, value); }
        }

        #region Commands
        public DelegateCommand ShowMainListCommand { get; private set; }
        public DelegateCommand ShowHomeViewCommand { get; private set; }

        public DelegateCommand ShowProductListCommand { get; private set; }
        private void InitializeCommands()
        {
            ShowMainListCommand = new DelegateCommand(OnShowMainList);
            RaisePropertyChanged(nameof(ShowMainListCommand));
            ShowHomeViewCommand = new DelegateCommand(OnShowHomeView);
            RaisePropertyChanged(nameof(ShowHomeViewCommand));
            ShowProductListCommand = new DelegateCommand(OnShowProductListView);
            RaisePropertyChanged(nameof(ShowProductListCommand));
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
        }

        public MainViewModel()
        {
            InitializeViews();
            InitializeCommands();
        }
    }
}
