using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Model
{
    public class Filter
    {
        private string m_Criteria = null;
        private string m_FilterText = null;
        private bool m_Enabled = false;

        public bool Enabled
        {
            get { return m_Enabled; }
            set { m_Enabled = value; }
        }

        public string FilterText
        {
            get { return m_FilterText; }
            set { m_FilterText = value; }
        }

        public string Criteria
        {
            get { return m_Criteria; }
            set { m_Criteria = value; }
        }

        public Filter(string p_Criteria, string p_FilterText)
        {
            m_Criteria = p_Criteria;
            m_FilterText = p_FilterText;
            m_Enabled = true;
        }

        public Filter()
        {
            m_Enabled = false;
        }
    }
}
