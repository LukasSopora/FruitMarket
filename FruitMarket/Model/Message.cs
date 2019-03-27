using FruitMarket.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Model
{
    public class Message
    {
        private string m_MessageText;
        private DateTime m_Date;
        private MessageType m_Type;

        public MessageType Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }


        public string MessageText
        {
            get { return m_MessageText; }
            set { m_MessageText = value; }
        }

        public Message(string p_MessageText, DateTime p_Date, MessageType p_Type)
        {
            m_MessageText = p_MessageText;
            m_Date = p_Date;
            m_Type = p_Type;
        }

        public Message()
        {

        }
    }
}
