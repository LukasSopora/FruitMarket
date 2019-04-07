using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Model
{
    public class Producer : BindableBase
    {
        private int m_Id;
        private string m_FirstName = null;
        private string m_LastName = null;
        private Adress m_Adress = null;
        private DateTime m_Birthday = DateTime.Now;
        private string m_Phone = null;
        private string m_Company = null;
        private string m_Email = null;

        public int Id
        {
            get { return m_Id; }  
            set { m_Id = value; }
        }

        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }

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

        public Adress Adress
        {
            get { return m_Adress; }
            set { SetProperty(ref m_Adress, value); }
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

        public Producer()
        {
            m_Adress = new Adress();
            RaisePropertyChanged(nameof(Adress));
        }

        public Producer(string p_Company)
        {
            m_Company = p_Company;
        }

        public Producer(string p_LastName, string p_FirstName, Adress p_Adress, DateTime p_Birthday, string p_Phone, string p_Company, string p_Email)
        {
            m_LastName = p_LastName;
            m_FirstName = p_FirstName;
            m_Adress = p_Adress;
            m_Birthday = p_Birthday;
            m_Phone = p_Phone;
            m_Company = p_Company;
            m_Email = p_Email;
        }

        public override string ToString()
        {
            return m_Company;
        }
    }
}
