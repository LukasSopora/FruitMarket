﻿using FruitMarket.Helper;
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

        public static ObservableCollection<ProductListData> GetAllProductListData()
        {
            var result = new ObservableCollection<ProductListData>();

            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}.{1}, {0}.{2}, {0}.{3}, {0}.{4}, {0}.{5}, {0}.{6}, {0}.{7}, {0}.{8}, {0}.{9}, {0}.{10}, {0}.{11}, {0}.{12}, " +
                "{13}.{14}, {13}.{15}, {13}.{16}, {13}.{17}, {13}.{18}, " +
                "{19}.{20}, {19}.{21}, {19}.{22}, {19}.{23}, {19}.{24} FROM {0} " +
                "INNER JOIN {13} ON {13}.{14} = {0}.{5} " +
                "INNER JOIN {19} ON {19}.{20} = {0}.{6}",
                ToolConstants.DB_PRODUCT_TABLE,
                ToolConstants.DB_PRODUCT_ID,
                ToolConstants.DB_PRODUCT_AMOUNT,
                ToolConstants.DB_PRODUCT_SORT,
                ToolConstants.DB_PRODUCT_ORIGIN,
                ToolConstants.DB_PRODUCT_PRODUCER_ID,
                ToolConstants.DB_PRODUCT_SUPPLIER_ID,
                ToolConstants.DB_PRODUCT_CATEGORY,
                ToolConstants.DB_PRODUCT_PURCHASEDATE,
                ToolConstants.DB_PRODUCT_EXPIRATION,
                ToolConstants.DB_PRODUCT_MATURE,
                ToolConstants.DB_PRODUCT_PURCHASEPRICE,
                ToolConstants.DB_PRODUCT_SALESPRICE,
                ToolConstants.DB_PRODUCER_TABLE,
                ToolConstants.DB_PRODUCER_ID,
                ToolConstants.DB_PRODUCER_LAST_NAME,
                ToolConstants.DB_PRODUCER_FIRST_NAME,
                ToolConstants.DB_PRODUCER_COMPANY,
                ToolConstants.DB_PRODUCER_ADRESS_ID,
                ToolConstants.DB_SUPPLIER_TABLE,
                ToolConstants.DB_SUPPLIER_ID,
                ToolConstants.DB_SUPPLIER_LAST_NAME,
                ToolConstants.DB_SUPPLIER_FIRST_NAME,
                ToolConstants.DB_SUPPLIER_COMPANY,
                ToolConstants.DB_SUPPLIER_ADRESS_ID);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var data = new ProductListData();
                var product = new Product();
                var producer = new Producer();
                var supplier = new Supplier();
                
                product.Id = reader.GetInt32(0);
                product.Amount = reader.GetInt32(1);
                product.Sort = reader.GetString(2);
                product.Origin = reader.GetString(3);
                product.ProducerId = reader.GetInt32(4);
                product.SupplierId = reader.GetInt32(5);
                product.Category= reader.GetString(6);
                product.PurchaseDate = reader.GetDateTime(7);
                product.Expiration = reader.GetDateTime(8);
                product.Mature = reader.GetInt32(9);
                product.PurchasePrice = reader.GetDouble(10);
                product.SalesPrice = reader.GetDouble(11);

                producer.Id = reader.GetInt32(12);
                producer.LastName = reader.GetString(13);
                producer.FirstName = reader.GetString(14);
                producer.Company = reader.GetString(15);
                producer.Adress = AdressMapper.GetAdressById(reader.GetInt32(16));
                
                supplier.Id = reader.GetInt32(17);
                supplier.LastName = reader.GetString(18); //hier schmiert der ab
                supplier.FirstName = reader.GetString(19);
                supplier.Company = reader.GetString(20);
                supplier.Adress = AdressMapper.GetAdressById(reader.GetInt32(21));

                data.Product = product;
                data.Producer = producer;
                data.Supplier = supplier;

                result.Add(data);
            }

            return result;
        }

        public static ProductListData GetProductListData(int p_ProductId)
        {
            ProductListData result = null;

            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}.{1}, {0}.{2}, {0}.{3}, {0}.{4}, {0}.{5}, {0}.{6}, {0}.{7}, {0}.{8}, {0}.{9}, {0}.{10}, {0}.{11}, {0}.{12}, " +
                "{13}.{14}, {13}.{15}, {13}.{16}, {13}.{17}, {13}.{18}, " +
                "{19}.{20}, {19}.{21}, {19}.{22}, {19}.{23}, {19}.{24} FROM {0} WHERE ({1} = @1) " +
                "INNER JOIN {13} ON {13}.{14} = {0}.{5} " +
                "INNER JOIN {19} ON {19}.{20} = {0}.{6}",
                ToolConstants.DB_PRODUCT_TABLE,
                ToolConstants.DB_PRODUCT_ID,
                ToolConstants.DB_PRODUCT_AMOUNT,
                ToolConstants.DB_PRODUCT_SORT,
                ToolConstants.DB_PRODUCT_ORIGIN,
                ToolConstants.DB_PRODUCT_PRODUCER_ID,
                ToolConstants.DB_PRODUCT_SUPPLIER_ID,
                ToolConstants.DB_PRODUCT_CATEGORY,
                ToolConstants.DB_PRODUCT_PURCHASEDATE,
                ToolConstants.DB_PRODUCT_EXPIRATION,
                ToolConstants.DB_PRODUCT_MATURE,
                ToolConstants.DB_PRODUCT_PURCHASEPRICE,
                ToolConstants.DB_PRODUCT_SALESPRICE,
                ToolConstants.DB_PRODUCER_TABLE,
                ToolConstants.DB_PRODUCER_ID,
                ToolConstants.DB_PRODUCER_LAST_NAME,
                ToolConstants.DB_PRODUCER_FIRST_NAME,
                ToolConstants.DB_PRODUCER_COMPANY,
                ToolConstants.DB_PRODUCER_ADRESS_ID,
                ToolConstants.DB_SUPPLIER_TABLE,
                ToolConstants.DB_SUPPLIER_ID,
                ToolConstants.DB_SUPPLIER_LAST_NAME,
                ToolConstants.DB_SUPPLIER_FIRST_NAME,
                ToolConstants.DB_SUPPLIER_COMPANY,
                ToolConstants.DB_SUPPLIER_ADRESS_ID);
            command.Parameters.Add("@1", System.Data.DbType.Int32).Value = p_ProductId;
            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                result = new ProductListData();
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

                result.Product = product;
                result.Producer = producer;
                result.Supplier = supplier;
            }
            return result;
        }
    }                           
}
