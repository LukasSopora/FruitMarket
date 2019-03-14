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
    public class UserControl1_PaulModel : BindableBase
    {
        private ObservableCollection<Product> m_Fruits =
            new ObservableCollection<Product>();

        public ObservableCollection<Product> Fruits
        {
            get { return m_Fruits; }
            set { SetProperty(ref m_Fruits, value); }
        }

        public UserControl1_PaulModel()
        {
            m_Fruits.Add(new Product("Hokkaidokürbis", 1337, "Kat. 2", new Supplier("Biomüll-Wiederaufbereitung KG"), DateTime.Now.AddDays(20), TimeSpan.FromHours(24), "Italien, -", 3.50, 5.99));
            m_Fruits.Add(new Product("Apfel", 1000, "Kat. 1", new Supplier("Grossman KG"), DateTime.Now.AddDays(2), TimeSpan.FromHours(14), "Deutschland, -", 2.50, 3.99));
            m_Fruits.Add(new Product("Drachenfrucht", 30, "Kat. 3", new Supplier("Grossman KG"), DateTime.Now.AddDays(2), TimeSpan.FromHours(96), "China, - ", 4.50, 7.99));
            m_Fruits.Add(new Product("Hokkaidokürbis", 20, "Kat. 3", new Supplier("Grossman KG"), DateTime.Now.AddDays(30), TimeSpan.FromHours(96), "Deutschland, -", 4.50, 7.99));
            m_Fruits.Add(new Product("Wassermelone", 0, "Kat. 3", new Supplier("Gassel AG"), DateTime.Now, TimeSpan.FromHours(48), "Vietnam, Phu Tho", 4.50, 12.99));
            m_Fruits.Add(new Product("Wassermelone", 0, "Kat. 3", new Supplier("Grossman KG"), DateTime.Now, TimeSpan.FromHours(48), "Vietnam, Phu Tho", 4.50, 7.99));
            m_Fruits.Add(new Product("Drachenfrucht", 0, "Kat. 3", new Supplier("Kellerman GmbH"), DateTime.Now.AddDays(20), TimeSpan.FromHours(48), "USA, Arizona", 4.50, 7.99));
            m_Fruits.Add(new Product("Hokkaidokürbis", 42, "Kat. 3", new Supplier("Kellerman GmbH"), DateTime.Now.AddDays(40), TimeSpan.FromHours(48), "Deutschland, -", 4.50, 7.99));
            m_Fruits.Add(new Product("Hokkaidokürbis", 42, "Kat. 3", new Supplier("Zettelman AG"), DateTime.Now.AddDays(50), TimeSpan.FromHours(48), "Deutschland, -", 4.50, 7.99));

        }
    }
}
