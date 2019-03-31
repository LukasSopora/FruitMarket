using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Model
{
    [Serializable]
    public class Product : BindableBase, ISerializable
    {
        private int m_Id = 0;
        private string m_Sort = null;
        private int m_Amount = 0;
        private string m_Category = null;
        private int m_SupplierId = 0;
        private int m_ProducerId = 0; 
        private DateTime m_PurchaseDate = DateTime.Now;
        private DateTime m_Expiration = DateTime.Now;
        private int m_Mature = 0;
        private string m_Origin = null;
        private double m_PurchasePrice = 0;
        private double m_SalesPrice = 0;


        public int Remaining
        {
            get
            {
                //When Database exists, you have to check the real remainig amonut!
                return 1000 - m_Amount;
            }
        }

        public int ProducerId
        {
            get { return m_ProducerId; }
            set { SetProperty(ref m_ProducerId, value); }
        }

        public double SalesPrice
        {
            get { return m_SalesPrice; }
            set
            {
                SetProperty(ref m_SalesPrice, value);
                RaisePropertyChanged(nameof(PositionSum));
            }
        }

        public double PurchasePrice
        {
            get { return m_PurchasePrice; }
            set { SetProperty(ref m_PurchasePrice, value); }
        }

        public string Origin
        {
            get { return m_Origin; }
            set { SetProperty(ref m_Origin, value); }
        }

        public int Mature
        {
            get { return m_Mature; }
            set { SetProperty(ref m_Mature, value); }
        }

        public DateTime PurchaseDate
        {
            get { return m_PurchaseDate; }
            set { SetProperty(ref m_PurchaseDate, value); }
        }

        public DateTime Expiration
        {
            get { return m_Expiration; }
            set { SetProperty(ref m_Expiration, value); }
        }

        public int SupplierId
        {
            get { return m_SupplierId; }
            set { SetProperty(ref m_SupplierId, value); }
        }

        public string Category
        {
            get { return m_Category; }
            set { SetProperty(ref m_Category, value); }
        }

        public int Amount
        {
            get { return m_Amount; }
            set
            {
                SetProperty(ref m_Amount, value);
                RaisePropertyChanged(nameof(Remaining));
                RaisePropertyChanged(nameof(PositionSum));
            }
        }

        public double PositionSum
        {
            get
            {
                return m_SalesPrice * m_Amount;
            }
        }

        public string Sort
        {
            get { return m_Sort; }
            set { SetProperty(ref m_Sort, value); }
        }

        public int Id
        {
            get { return m_Id; }
            set { SetProperty(ref m_ProducerId, value); }
        }

        public Product(string p_Sort, int p_Amount, string p_Category,
                     int p_SupplierId, int p_ProducerId, DateTime p_PurchaseDate,
                     DateTime p_Expiration, int p_Mature, string p_Origin,
                     double p_PurchasePrice, double p_SalesPrice)
        {
            m_Sort = p_Sort;
            m_Amount = p_Amount;
            m_Category = p_Category;
            m_SupplierId = p_SupplierId;
            m_ProducerId = p_ProducerId;
            m_PurchaseDate = p_PurchaseDate;
            m_Expiration = p_Expiration;
            m_Mature = p_Mature;
            m_Origin = p_Origin;
            m_PurchasePrice = p_PurchasePrice;
            m_SalesPrice = p_SalesPrice;
        }

        public Product(SerializationInfo info, StreamingContext context)
        {
            m_Category = (string)info.GetValue(nameof(Category), typeof(string));
            m_Expiration = (DateTime)info.GetValue(nameof(Expiration), typeof(DateTime));
            m_PurchaseDate = (DateTime)info.GetValue(nameof(PurchaseDate), typeof(DateTime));
            m_Mature = (int)info.GetValue(nameof(Mature), typeof(int));
            m_Origin = (string)info.GetValue(nameof(Origin), typeof(string));
            m_PurchasePrice = (double)info.GetValue(nameof(PurchasePrice), typeof(double));
            m_SalesPrice = (double)info.GetValue(nameof(SalesPrice), typeof(double));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Category), m_Category);
            info.AddValue(nameof(Expiration), m_Expiration);
            info.AddValue(nameof(PurchaseDate), m_PurchaseDate);
            info.AddValue(nameof(Mature), m_Mature);
            info.AddValue(nameof(Origin), m_Origin);
            info.AddValue(nameof(PurchasePrice), m_PurchasePrice);
            info.AddValue(nameof(SalesPrice), m_SalesPrice);
        }

        public Product()
        {
        }
    }
}
