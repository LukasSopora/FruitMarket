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
    public class MainListViewModel : BindableBase
    {
        private ObservableCollection<Product> m_Fruits =
            new ObservableCollection<Product>();

        public ObservableCollection<Product> Fruits
        {
            get { return m_Fruits; }
            set { SetProperty(ref m_Fruits, value); }
        }

        public string PageDescription
        {
            get
            {
                return ToolConstants.MAIN_LIST_VIEW_DESC;
            }
        }

        public MainListViewModel()
        {
            m_Fruits.Add(new Product("Apfel", 1000, "Kat. 1", new Supplier("Grossman KG"), new Producer("Grossman KG"), DateTime.Now.AddDays(2), new Mature(0, 14), "Deutschland, -", 2.50, 3.99));
            m_Fruits.Add(new Product("Drachenfrucht", 30, "Kat. 3", new Supplier("Grossman KG"), new Producer("Grossman KG"), DateTime.Now.AddDays(2), new Mature(0, 96), "China, - ", 4.50, 7.99));
            m_Fruits.Add(new Product("Drachenfrucht", 30, "Kat. 3", new Supplier("Kellerman GmbH"), new Producer("Kellerman GmbH"), DateTime.Now.AddDays(20), new Mature(0, 48), "USA, Arizona", 4.50, 7.99));
            m_Fruits.Add(new Product("Hokkaidokürbis", 1337, "Kat. 2", new Supplier("Biomüll-Wiederaufbereitung KG"), new Producer("Biomüll-Wiederaufbereitung KG"), DateTime.Now.AddDays(20), new Mature(0, 48), "Italien, -", 3.50, 5.99));
            m_Fruits.Add(new Product("Hokkaidokürbis", 20, "Kat. 3", new Supplier("Grossman KG"), new Producer("Grossman KG"), DateTime.Now.AddDays(30), new Mature(0, 96), "Deutschland, -", 4.50, 7.99));
            m_Fruits.Add(new Product("Hokkaidokürbis", 42, "Kat. 3", new Supplier("Kellerman GmbH"), new Producer("Kellerman GmbH"), DateTime.Now.AddDays(40), new Mature(0, 48), "Deutschland, -", 4.50, 7.99));
            m_Fruits.Add(new Product("Hokkaidokürbis", 42, "Kat. 3", new Supplier("Zettelman AG"), new Producer("Zettelman AG"), DateTime.Now.AddDays(50), new Mature(0, 48), "Deutschland, -", 4.50, 7.99));


        }
    }
}
