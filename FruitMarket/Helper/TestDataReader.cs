using FruitMarket.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Helper
{
    public class TestDataReader
    {
        private static int m_MAX_NUMBER = 20;  //Number of Suppliers and Producers [0:50]

        public static ObservableCollection<Supplier> GetDefaultSuppliers()
        {
            ObservableCollection<Supplier> result = new ObservableCollection<Supplier>();

            StreamReader reader = new StreamReader("Helper/TestData.csv");
            reader.ReadLine(); //First Line not important
            int count = 0;
            while (count < m_MAX_NUMBER)
            {
                Supplier supplier = new Supplier();
                string line = reader.ReadLine();
                string[] values = line.Split('|');

                supplier.Id = count + 1;
                supplier.Company = values[0];
                supplier.FirstName = values[1];
                supplier.LastName = values[2];
                string[] birthday = values[3].Split('-');
                supplier.Birthday = DateTime.Parse(string.Format("{1}-{0}-{2}", birthday[0], birthday[1], birthday[2]));
                supplier.Phone = values[4];
                supplier.Adress = new Adress(values[5], values[6], values[7]);
                supplier.Email = values[8];

                result.Add(supplier);
                count++;
            }

            return result;
        }

        public static ObservableCollection<Producer> GetDefaultProducers()
        {
            ObservableCollection<Producer> result = new ObservableCollection<Producer>();

            StreamReader reader = new StreamReader("Helper/TestData.csv");
            reader.ReadLine(); //First Line not important
            int count = m_MAX_NUMBER;
            while (count < m_MAX_NUMBER * 2)
            {
                Producer producer= new Producer();
                string line = reader.ReadLine();
                string[] values = line.Split('|');

                producer.Id = count - m_MAX_NUMBER + 1;
                producer.Company = values[0];
                producer.FirstName = values[1];
                producer.LastName = values[2];
                string[] birthday = values[3].Split('-');
                producer.Birthday = DateTime.Parse(string.Format("{1}-{0}-{2}", birthday[0], birthday[1], birthday[2]));
                producer.Phone = values[4];
                producer.Adress = new Adress(values[5], values[6], values[7]);
                producer.Email = values[8];

                result.Add(producer);
                count++;
            }

            return result;
        }
    }
}
