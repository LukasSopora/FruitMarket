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
    public class Adress : BindableBase, ISerializable
    {
        private string m_Street = null;
        private string m_PostCode = null;
        private string m_Place = null;
        private int m_Id;

        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        public string Place
        {
            get { return m_Place; }
            set { SetProperty(ref m_Place, value); }
        }

        public string PostCode
        {
            get { return m_PostCode; }
            set { SetProperty(ref m_PostCode, value); }
        }

        public string Street
        {
            get { return m_Street; }
            set { SetProperty(ref m_Street, value); }
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

        public Adress(SerializationInfo info, StreamingContext context)
        {
            m_Street = (string) info.GetValue(nameof(Street), typeof(string));
            m_PostCode = (string)info.GetValue(nameof(PostCode), typeof(string));
            m_Place = (string)info.GetValue(nameof(Place), typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Street), m_Street);
            info.AddValue(nameof(PostCode), m_PostCode);
            info.AddValue(nameof(Place), m_Place);
        }
    }
}
