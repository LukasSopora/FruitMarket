using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Model
{
    public class Fruit : BindableBase
    {
        private int m_Id;
        private string m_Sort;
        private int m_Amount;
        private string m_Category;
        private Supplier m_Supplier;
        private DateTime m_PurchaseDate;
        private DateTime m_Expiration;
        private TimeSpan m_Mature;
        private string m_Origin;
        private double m_PurchasePrice;
        private double m_SalesPrice;

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

        public TimeSpan Mature
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
            set { m_Amount = value; }
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

        public Fruit(string p_Sort, int p_Amount, string p_Category, Supplier p_Supplier,
                     DateTime p_Expiration, TimeSpan p_Mature, string p_Origin,
                     double p_PurchasePrice, double p_SalesPrice)
        {
            m_Sort = p_Sort;
            m_Amount = p_Amount;
            m_Category = p_Category;
            m_Supplier = p_Supplier;
            m_Expiration = p_Expiration;
            m_Mature = p_Mature;
            m_Origin = p_Origin;
            m_PurchasePrice = p_PurchasePrice;
            m_SalesPrice = p_SalesPrice;
        }
    }
}
