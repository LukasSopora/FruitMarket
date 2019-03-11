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
    public class MainListViewModel : BindableBase
    {
        private ObservableCollection<Fruit> m_Fruits =
            new ObservableCollection<Fruit>();

        public ObservableCollection<Fruit> Fruits
        {
            get { return m_Fruits; }
            set { SetProperty(ref m_Fruits, value); }
        }

        public MainListViewModel()
        {
            m_Fruits.Add(new Fruit("Sort 1", 1, "Cat. 1", new Supplier("Lidl"), DateTime.Now, TimeSpan.FromHours(12), "Spain", 2.50, 3.99));
            m_Fruits.Add(new Fruit("Sort 2", 1, "Cat. 2", new Supplier("Aldi"), DateTime.Now, TimeSpan.FromHours(24), "Italy", 3.50, 5.99));
            m_Fruits.Add(new Fruit("Sort 3", 1, "Cat. 3", new Supplier("Apple"), DateTime.Now, TimeSpan.FromHours(48), "Germany", 4.50, 7.99));
        }
    }
}
