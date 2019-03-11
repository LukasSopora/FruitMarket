using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Helper
{
    public class LogEntry
    {
        private LogType m_Type;
        private string m_Message;
        private DateTime m_Date;

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        public string Message
        {
            get { return m_Message; }
            set { m_Message = value; }
        }

        public LogType Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public LogEntry(LogType p_Type, string p_Message, DateTime p_Date)
        {
            m_Type = p_Type;
            m_Message = p_Message;
            m_Date = p_Date;
        }

        public LogEntry(LogType p_Type, string p_Message)
        {
            m_Type = p_Type;
            m_Message = p_Message;
            m_Date = DateTime.Now;
        }
    }
}
