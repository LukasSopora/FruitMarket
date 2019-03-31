using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Model
{
    [Serializable]
    public class Delivery : ISerializable
    {
        private int m_Id = 0;
        private int m_ProductId = 0;
        private int m_CostumerId = 0;
        private int m_Amount = 0;
        private DateTime m_SalesDate = DateTime.Now;

        public DateTime SalesDate
        {
            get { return m_SalesDate; }
            set { m_SalesDate = value; }
        }

        public int CostumerId
        {
            get { return m_CostumerId; }
            set { m_CostumerId = value; }
        }

        public int Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }

        public int ProductId
        {
            get { return m_ProductId; }
            set { m_ProductId = value; }
        }

        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        public Delivery(SerializationInfo info, StreamingContext context)
        {
            m_Amount = (int)info.GetValue(nameof(Amount), typeof(int));
            m_SalesDate = (DateTime)info.GetValue(nameof(SalesDate), typeof(DateTime));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Amount), m_Amount);
            info.AddValue(nameof(SalesDate), m_SalesDate);
        }
    }
}
