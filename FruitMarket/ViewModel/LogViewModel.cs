using FruitMarket.Helper;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.ViewModel
{
    public class LogViewModel : BindableBase
    {
        private ObservableCollection<LogEntry> m_Logs =
            new ObservableCollection<LogEntry>();

        public ObservableCollection<LogEntry> Logs
        {
            get { return m_Logs; }
            set { SetProperty(ref m_Logs, value); }
        }

        public LogViewModel()
        {
            m_Logs.Add(new LogEntry(LogType.Information, "Message 1"));
            m_Logs.Add(new LogEntry(LogType.Warning, "Message 2"));
            m_Logs.Add(new LogEntry(LogType.Error, "Message 3"));
        }
    }
}
