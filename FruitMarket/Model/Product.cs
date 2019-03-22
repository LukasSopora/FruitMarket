using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Model
{
    public class Product : BindableBase, ISerializable
    {
        private int m_Id = 0;
        private string m_Sort = null;
        private int m_Amount = 0;
        private string m_Category = null;
        private Supplier m_Supplier = null;
        private Producer m_Producer = null; 
        private DateTime m_PurchaseDate = DateTime.MinValue;
        private DateTime m_Expiration = DateTime.MinValue;
        private Mature m_Mature = null;
        private string m_Origin = null;
        private double m_PurchasePrice = 0;
        private double m_SalesPrice = 0;

        public Producer Producer
        {
            get { return m_Producer; }
            set { m_Producer = value; }
        }

        public double SalesPrice
        {
            get { return m_SalesPrice; }
            set { m_SalesPrice = value; }
        }

        public double PurchasePrice
        {
            get { return m_PurchasePrice; }
            set { m_PurchasePrice = value; }
        }

        public string Origin
        {
            get { return m_Origin; }
            set { m_Origin = value; }
        }

        public Mature Mature
        {
            get { return m_Mature; }
            set { m_Mature = value; }
        }

        public DateTime PurchaseDate
        {
            get { return m_PurchaseDate; }
            set { m_PurchaseDate = value; }
        }

        public DateTime Expiration
        {
            get { return m_Expiration; }
            set { m_Expiration = value; }
        }

        public Supplier Supplier
        {
            get { return m_Supplier; }
            set { m_Supplier = value; }
        }

        public string Category
        {
            get { return m_Category; }
            set { m_Category = value; }
        }

        public int Amount
        {
            get { return m_Amount; }
            set { SetProperty(ref m_Amount, value); }
        }

        public string Sort
        {
            get { return m_Sort; }
            set { m_Sort = value; }
        }

        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        public Product(string p_Sort, int p_Amount, string p_Category,
                     Supplier p_Supplier, Producer p_Producer,
                     DateTime p_Expiration, Mature p_Mature, string p_Origin,
                     double p_PurchasePrice, double p_SalesPrice)
        {
            m_Sort = p_Sort;
            m_Amount = p_Amount;
            m_Category = p_Category;
            m_Supplier = p_Supplier;
            m_Producer = p_Producer;
            m_Expiration = p_Expiration;
            m_Mature = p_Mature;
            m_Origin = p_Origin;
            m_PurchasePrice = p_PurchasePrice;
            m_SalesPrice = p_SalesPrice;
        }

        public Product(SerializationInfo info, StreamingContext context)
        {
            m_Amount = (int)info.GetValue(nameof(Amount), typeof(int));
            m_Category = (string)info.GetValue(nameof(Category), typeof(string));
            m_Supplier = (Supplier)info.GetValue(nameof(Supplier), typeof(Supplier));
            m_Producer = (Producer)info.GetValue(nameof(Producer), typeof(Producer));
            m_Expiration = (DateTime)info.GetValue(nameof(Expiration), typeof(DateTime));
            m_PurchaseDate = (DateTime)info.GetValue(nameof(PurchaseDate), typeof(DateTime));
            m_Mature = (Mature)info.GetValue(nameof(Mature), typeof(Mature));
            m_Origin = (string)info.GetValue(nameof(Origin), typeof(string));
            m_PurchasePrice = (double)info.GetValue(nameof(PurchasePrice), typeof(double));
            m_SalesPrice = (double)info.GetValue(nameof(SalesPrice), typeof(double));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Amount), m_Amount);
            info.AddValue(nameof(Category), m_Category);
            info.AddValue(nameof(Supplier), m_Supplier);
            info.AddValue(nameof(Producer), m_Producer);
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
