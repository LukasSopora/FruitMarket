using FruitMarket.Helper;
using FruitMarket.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.ViewModel
{
    public class HomeViewModel : BindableBase
    {
        private ObservableCollection<Message> m_Mesages =
            new ObservableCollection<Message>();

        public ObservableCollection<Message> Messages
        {
            get { return m_Mesages; }
            set { SetProperty(ref m_Mesages, value); }
        }

        public HomeViewModel()
        {
            m_Mesages.Add(new Message("Auberginen können bis Oktober nicht mehr bestellt werden.", DateTime.Now, MessageType.Critical));
            m_Mesages.Add(new Message("Lager ist zu ~80% gefüllt.", DateTime.Parse("20-03-2019"), MessageType.Warning));
            m_Mesages.Add(new Message("Lieferung 01342-1343 erfolgreich zugestellt.", DateTime.Now, MessageType.Information));
            RaisePropertyChanged(nameof(Messages));
        }
    }
}
