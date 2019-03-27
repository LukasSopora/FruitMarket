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
    public class DeliveryViewModel : BindableBase
    {
        private ObservableCollection<Product> m_Products =
            new ObservableCollection<Product>();
        private ObservableCollection<Costumer> m_Costumers =
            new ObservableCollection<Costumer>();
        private Costumer m_Costumer = null;
        private DateTime m_DeliveryDate;
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

        public Costumer Costumer
        {
            get { return m_Costumer; }
            set { SetProperty(ref m_Costumer, value); }
        }

        public DeliveryViewModel()
        {
            m_Costumers = TestDataReader.GetDefaultCostumers();
            RaisePropertyChanged(nameof(Costumers));
        }
    }
}
