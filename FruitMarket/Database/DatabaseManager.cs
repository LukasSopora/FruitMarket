using FruitMarket.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Database
{
    public class DatabaseManager
    {
        public static void InitCountries()
        {
            if(TestDataReader.GetCountryAmount() != CountryMapper.GetCountryAmount())
            {
                CountryMapper.DeleteAllCountries();
                CountryMapper.SaveCountries(TestDataReader.GetAllCountries());
            }

            ProducerMapper.SaveProducers(TestDataReader.GetDefaultProducers());
            SupplierMapper.SaveSuppliers(TestDataReader.GetDefaultSuppliers());
        }
    }
}
