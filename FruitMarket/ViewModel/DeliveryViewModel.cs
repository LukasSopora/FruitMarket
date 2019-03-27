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
    public class DeliveryViewModel : BindableBase
    {
        private ObservableCollection<Product> m_Products =
            new ObservableCollection<Product>();
        private ObservableCollection<Costumer> m_Costumers =
            new ObservableCollection<Costumer>();
        private ObservableCollection<string> m_Sorts =
            new ObservableCollection<string>();
        private Costumer m_CurrentCostumer = null;
        private DateTime m_DeliveryDate = DateTime.Now;
        private double m_Sum;

        public double Sum
        {
            get
            {
                double result = 0.0;
                foreach(Product p in m_Products)
                {
                    result += p.PositionSum;
                }
                return result;
            }
        }

        public ObservableCollection<string> Sorts
        {
            get { return m_Sorts; }
            set { SetProperty(ref m_Sorts, value); }
        }

        public string PageDescription
        {
            get
            {
                return ToolConstants.DELIVERY_VIEW_DESC;
            }
        }

        public ObservableCollection<Costumer> Costumers
        {
            get { return m_Costumers; }
            set { SetProperty(ref m_Costumers, value); }
        }

        public ObservableCollection<Product> Products
        {
            get { return m_Products; }
            set { SetProperty(ref m_Products, value); }
        }

        public DateTime DeliveryDate
        {
            get { return m_DeliveryDate; }
            set { SetProperty(ref m_DeliveryDate, value); }
        }

        public Costumer CurrentCostumer
        {
            get { return m_CurrentCostumer; }
            set { SetProperty(ref m_CurrentCostumer, value); }
        }

        #region Commands
        public DelegateCommand NewCostumerCommand { get; private set; }
        public DelegateCommand SaveCostumerCommand { get; private set; }
        public DelegateCommand DeleteCostumerCommand { get; private set; }
        public DelegateCommand AddProductCommand { get; private set; }


        private void InitializeCommands()
        {
            AddProductCommand = new DelegateCommand(OnAddProduct);
            RaisePropertyChanged(nameof(AddProductCommand));
        }

        private void OnAddProduct()
        {
            m_Products.Add(new Product());
            RaisePropertyChanged(nameof(Products));
        }


        #endregion

        public DeliveryViewModel()
        {
            InitializeCommands();

            m_Costumers = TestDataReader.GetDefaultCostumers();
            RaisePropertyChanged(nameof(Costumers));

            m_Sorts = ToolConstants.DEFAULT_FRUITS;
            RaisePropertyChanged(nameof(Sorts));
        }

        public void UpdateSum()
        {
            m_Sum = 0.0;
            foreach(Product p in m_Products)
            {
                m_Sum += p.PositionSum;
            }
            RaisePropertyChanged(nameof(Sum));
        }
    }
}
