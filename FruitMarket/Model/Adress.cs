using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Model
{
    public class Adress
    {
        private string m_Street = null;
        private string m_PostCode = null;
        private string m_Place = null;

        public string Place
        {
            get { return m_Place; }
            set { m_Place = value; }
        }

        public string PostCode
        {
            get { return m_PostCode; }
            set { m_PostCode = value; }
        }

        public string Street
        {
            get { return m_Street; }
            set { m_Street = value; }
        }

        public Adress(string p_Street, string p_PostCode, string p_Place)
        {
            m_Street = p_Street;
            m_PostCode = p_PostCode;
            m_Place = p_Place;
        }

        public Adress()
        { 
        }
    }
}
