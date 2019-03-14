using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Model
{
    public class Supplier
    {
        private string m_FirstName = null;
        private string m_LastName = null;
        private string m_Adress = null;
        private DateTime m_Birthday;
        private string m_Phone = null;
        private string m_Company = null;

        public string Company
        {
            get { return m_Company; }
            set { m_Company = value; }
        }

        public string Phone
        {
            get { return m_Phone; }
            set { m_Phone = value; }
        }

        public DateTime Birthday
        {
            get { return m_Birthday; }
            set { m_Birthday = value; }
        }

        public string Adress
        {
            get { return m_Adress; }
            set { m_Adress = value; }
        }


        public string LastName
        {
            get { return m_LastName; }
            set { m_LastName = value; }
        }



        public string FirstName
        {
            get { return m_FirstName; }
            set { m_FirstName = value; }
        }

        public Supplier()
        {
        }

        public Supplier(string p_LastName)
        {
            m_LastName = p_LastName;
        }

        public override string ToString()
        {
            return m_LastName;
        }
    }
}
