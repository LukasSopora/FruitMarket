using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Model
{
    public class Mature : ISerializable
    {
        private int m_Days;
        private double m_Hours;

        public double Hours
        {
            get { return m_Hours; }
            set { m_Hours = value; Format(); }
        }

        public int Days
        {
            get { return m_Days; }
            set { m_Days = value; }
        }

        private void Format()
        {
            while(m_Hours > 23)
            {
                m_Hours -= 23;
                m_Days++;
            }
        }

        public Mature()
        {

        }

        public Mature(int p_Days, double p_Hours)
        {
            m_Days = p_Days;
            m_Hours = p_Hours;
            Format();
        }

        public Mature(SerializationInfo info, StreamingContext context)
        {
            m_Days = (int)info.GetValue(nameof(Days), typeof(int));
            m_Hours = (double)info.GetValue(nameof(Hours), typeof(double));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Days), m_Days);
            info.AddValue(nameof(Hours), m_Hours);
        }
    }
}
