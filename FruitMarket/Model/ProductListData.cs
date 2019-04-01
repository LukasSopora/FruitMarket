using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Model
{
    public class ProductListData
    {
        private Product m_Product = null;
        private Producer m_Producer = null;
        private Supplier m_Supplier = null;

        public Supplier Supplier
        {
            get { return m_Supplier; }
            set { m_Supplier = value; }
        }

        public Producer Producer
        {
            get { return m_Producer; }
            set { m_Producer = value; }
        }

        public Product Product
        {
            get { return m_Product; }
            set { m_Product = value; }
        }

        public ProductListData()
        {
            m_Product = new Product();
            m_Producer = new Producer();
            m_Supplier = new Supplier();
        }
    }
}
