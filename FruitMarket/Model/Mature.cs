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
    public class Mature : ISerializable
    {
        private int m_Days;
        private DateTime m_Hours;

        public DateTime Hours
        {
            get { return m_Hours; }
            set { m_Hours = value; }
        }

        public int Days
        {
            get { return m_Days; }
            set { m_Days = value; }
        }

        public Mature()
        {

        }

        public Mature(int p_Days, DateTime p_Hours)
        {
            m_Days = p_Days;
            m_Hours = p_Hours;
        }

        public Mature(int p_Days, double p_Hours)
        {
            m_Days = p_Days;
            m_Hours = DateTime.Now.Date.AddHours(p_Hours);
        }

        public Mature(SerializationInfo info, StreamingContext context)
        {
            m_Days = (int)info.GetValue(nameof(Days), typeof(int));
            m_Hours = (DateTime)info.GetValue(nameof(Hours), typeof(DateTime));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Days), m_Days);
            info.AddValue(nameof(Hours), m_Hours);
        }

        public override string ToString()
        {
            return string.Format("{0} d, {1} h", m_Days, m_Hours);
        }
    }
}
