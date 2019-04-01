using FruitMarket.Helper;
using FruitMarket.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

        public static ObservableCollection<ProductListData> GetProductListData()
        {
            var result = new ObservableCollection<ProductListData>();

            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}.{1}, {0}.{2}, {0}.{3}, {0}.{4}, {0}.{5}, {0}.{6}, {0}.{7}, " +
                "{8}.{9}, {8}.{10}, {8}.{11}, {8}.{12}, {8}.{13}, " +
                "{14}.{15}, {14}.{16}, {14}.{17}, {14}.{18}, {14}.{19} FROM {0} " +
                "INNER JOIN {8} ON {8}.{9} = {0}.{5} " +
                "INNER JOIN {14} ON {14}.{15} = {0}.{6}",
                ToolConstants.DB_PRODUCT_TABLE,
                ToolConstants.DB_PRODUCT_ID,
                ToolConstants.DB_PRODUCT_AMOUNT,
                ToolConstants.DB_PRODUCT_SORT,
                ToolConstants.DB_PRODUCT_ORIGIN,
                ToolConstants.DB_PRODUCT_PRODUCER_ID,
                ToolConstants.DB_PRODUCT_SUPPLIER_ID,
                ToolConstants.DB_PRODUCT_DATA,
                ToolConstants.DB_PRODUCER_TABLE,
                ToolConstants.DB_PRODUCER_ID,
                ToolConstants.DB_PRODUCER_LAST_NAME,
                ToolConstants.DB_PRODUCER_FIRST_NAME,
                ToolConstants.DB_PRODUCER_COMPANY,
                ToolConstants.DB_PRODUCER_DATA,
                ToolConstants.DB_SUPPLIER_TABLE,
                ToolConstants.DB_SUPPLIER_ID,
                ToolConstants.DB_SUPPLIER_LAST_NAME,
                ToolConstants.DB_SUPPLIER_FIRST_NAME,
                ToolConstants.DB_SUPPLIER_COMPANY,
                ToolConstants.DB_SUPPLIER_DATA);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var data = new ProductListData();
                Product product;
                Producer producer;
                Supplier supplier;

                MemoryStream memoryStream = new MemoryStream((byte[])reader.GetValue(6));
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                product = (Product)binaryFormatter.Deserialize(memoryStream);
                product.Id = reader.GetInt32(0);
                product.Amount = reader.GetInt32(1);
                product.Sort = reader.GetString(2);
                product.Origin = reader.GetString(3);
                product.ProducerId = reader.GetInt32(4);
                product.SupplierId = reader.GetInt32(5);

                memoryStream = new MemoryStream((byte[])reader.GetValue(11));
                binaryFormatter = new BinaryFormatter();
                producer = (Producer)binaryFormatter.Deserialize(memoryStream);
                producer.Id = reader.GetInt32(7);
                producer.LastName = reader.GetString(8);
                producer.FirstName = reader.GetString(9);
                producer.Company = reader.GetString(10);

                memoryStream = new MemoryStream((byte[])reader.GetValue(16));
                binaryFormatter = new BinaryFormatter();
                supplier = (Supplier)binaryFormatter.Deserialize(memoryStream);
                supplier.Id = reader.GetInt32(12);
                supplier.LastName = reader.GetString(13);
                supplier.FirstName = reader.GetString(14);
                supplier.Company = reader.GetString(15);

                data.Product = product;
                data.Producer = producer;
                data.Supplier = supplier;

                result.Add(data);
            }

            return result;
        }
    }                           
}
