using FruitMarket.Helper;
using FruitMarket.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
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

            SortMapper.SaveSorts(ToolConstants.DEFAULT_FRUITS);

            ProducerMapper.SaveProducers(TestDataReader.GetDefaultProducers());
            SupplierMapper.SaveSuppliers(TestDataReader.GetDefaultSuppliers());
            CostumerMapper.SaveCostumers(TestDataReader.GetDefaultCostumers());
        }
    }

    public static ObservableCollection<ProductListData> GetProductListData()
    {
        var result = new ObservableCollection<ProductListData>();

        SQLiteConnection con = Connection.GetConnection();
        SQLiteCommand command = new SQLiteCommand(con);

        command.CommandText = string.Format(
            "SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}")
    }
}
