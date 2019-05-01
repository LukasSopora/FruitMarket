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
        private static int m_MAX_NUMBER = 20;  //Number of Suppliers and Producers [0:33]
        private static string m_PERSON_TESTDATA_PATH = "Helper/TestData.csv";
        private static string m_COUNTRIES_DATA_PATH = "Helper/countries.csv";

        public static ObservableCollection<Producer> GetDefaultProducers()
        {
            ObservableCollection<Producer> result = 
                new ObservableCollection<Producer>();

            StreamReader reader = new StreamReader("Helper/TestData.csv");

            for (int count = 0; count < m_MAX_NUMBER; count++)
            {
                Producer producer = new Producer();
                string line = File.ReadLines(m_PERSON_TESTDATA_PATH).Skip(count).Take(1).First();
                string[] values = line.Split('|');

                producer.Id = count - m_MAX_NUMBER + 1;
                producer.Company = values[0];
                producer.FirstName = values[1];
                producer.LastName = values[2];
                string[] birthday = values[3].Split('-');
                producer.Birthday = DateTime.Parse(string.Format("{1}-{0}-{2}", birthday[0], birthday[1], birthday[2]));
                producer.Phone = values[4];
                producer.Adress = new Adress(count + 1 ,values[5], values[6], values[7]);
                producer.Email = values[8];

                result.Add(producer);
            }

            return result;
        }

        public static ObservableCollection<Supplier> GetDefaultSuppliers()
        {
            ObservableCollection<Supplier> result = 
                new ObservableCollection<Supplier>();

            for (int count = m_MAX_NUMBER; count < m_MAX_NUMBER * 2; count++)
            {
                Supplier supplier = new Supplier();
                string line = File.ReadLines(m_PERSON_TESTDATA_PATH).Skip(count).Take(1).First();
                string[] values = line.Split('|');

                supplier.Id = count + 1;
                supplier.Company = values[0];
                supplier.FirstName = values[1];
                supplier.LastName = values[2];
                string[] birthday = values[3].Split('-');
                supplier.Birthday = DateTime.Parse(string.Format("{1}-{0}-{2}", birthday[0], birthday[1], birthday[2]));
                supplier.Phone = values[4];
                supplier.Adress = new Adress(count + 1, values[5], values[6], values[7]);
                supplier.Email = values[8];

                result.Add(supplier);
            }

            return result;
        }


        public static ObservableCollection<Costumer> GetDefaultCostumers()
        {
            ObservableCollection<Costumer> result = 
                new ObservableCollection<Costumer>();

            StreamReader reader = new StreamReader("Helper/TestData.csv");

            for (int count = m_MAX_NUMBER * 2; count < m_MAX_NUMBER * 3; count++)
            {
                Costumer costumer = new Costumer();
                string line = File.ReadLines(m_PERSON_TESTDATA_PATH).Skip(count).Take(1).First();
                string[] values = line.Split('|');

                costumer.Id = count - ( m_MAX_NUMBER * 2) + 1;
                costumer.Company = values[0];
                costumer.FirstName = values[1];
                costumer.LastName = values[2];
                string[] birthday = values[3].Split('-');
                costumer.Birthday = DateTime.Parse(string.Format("{1}-{0}-{2}", birthday[0], birthday[1], birthday[2]));
                costumer.Phone = values[4];
                costumer.Adress = new Adress(count + 1, values[5], values[6], values[7]);
                costumer.Email = values[8];

                result.Add(costumer);
            }

            return result;
        }

        public static ObservableCollection<string> GetAllCountries()
        {
            ObservableCollection<string> result = 
                new ObservableCollection<string>();

            string[] countries = File.ReadAllLines(m_COUNTRIES_DATA_PATH);

            for (int i = 1; i < countries.Length; i++) //skip first header line
            {
                result.Add(countries[i].Split(';', '"')[7]);
            }

            return result;
        }

        public static int GetCountryAmount()
        {
            return File.ReadAllLines(m_COUNTRIES_DATA_PATH).Count() - 1; //minus first header line
        }
    }
}
