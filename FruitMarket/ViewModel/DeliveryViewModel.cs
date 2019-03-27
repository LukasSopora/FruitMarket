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

    }
}
