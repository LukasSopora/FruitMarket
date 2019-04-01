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
    public class ProductMapper
    {
        public static void SaveProducts(ObservableCollection<Product> p_Products)
        {
            foreach (Product product in p_Products)
            {
                SaveProduct(product);
            }
        }

        public static void SaveProducts(IEnumerable<Product> p_Products, bool a)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}) " +
                "VALUES (@1, @2, @3, @4, @5, @6)",
                ToolConstants.DB_PRODUCT_TABLE,
                ToolConstants.DB_PRODUCT_SORT,
                ToolConstants.DB_PRODUCT_ORIGIN,
                ToolConstants.DB_PRODUCT_AMOUNT,
                ToolConstants.DB_PRODUCT_PRODUCER_ID,
                ToolConstants.DB_PRODUCT_SUPPLIER_ID,
                ToolConstants.DB_PRODUCT_DATA);
            command.Parameters.Add("@1", System.Data.DbType.String);
            command.Parameters.Add("@2", System.Data.DbType.String);
            command.Parameters.Add("@3", System.Data.DbType.Int32);
            command.Parameters.Add("@4", System.Data.DbType.Int32);
            command.Parameters.Add("@5", System.Data.DbType.Int32);
            command.Parameters.Add("@6", System.Data.DbType.Binary);

            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            byte[] productData = null;

            foreach(var product in p_Products)
            {
                binaryFormatter.Serialize(memoryStream, product);

                command.Parameters[0].Value = product.Sort;
                command.Parameters[1].Value = product.Origin;
                command.Parameters[2].Value = product.Amount;
                command.Parameters[3].Value = product.ProducerId;
                command.Parameters[4].Value = product.SupplierId;
                command.Parameters[5].Value = memoryStream.ToArray();

                command.ExecuteNonQuery();
            }

        }

        private static void SaveProduct(Product p_Product)
        {
            SQLiteConnection con = Connection.GetConnection();

            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}) " +
                "VALUES (@1, @2, @3, @4, @5, @6)",
                ToolConstants.DB_PRODUCT_TABLE,
                ToolConstants.DB_PRODUCT_SORT,
                ToolConstants.DB_PRODUCT_ORIGIN,
                ToolConstants.DB_PRODUCT_AMOUNT,
                ToolConstants.DB_PRODUCT_PRODUCER_ID,
                ToolConstants.DB_PRODUCT_SUPPLIER_ID,
                ToolConstants.DB_PRODUCT_DATA);

            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, p_Product);

            byte[] productData = memoryStream.ToArray();
            memoryStream.Close();

            command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Product.Sort;
            command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Product.Origin;
            command.Parameters.Add("@3", System.Data.DbType.Int32).Value = p_Product.Amount;
            command.Parameters.Add("@4", System.Data.DbType.Int32).Value = p_Product.ProducerId;
            command.Parameters.Add("@5", System.Data.DbType.Int32).Value = p_Product.SupplierId;
            command.Parameters.Add("@6", System.Data.DbType.Binary, productData.Length).Value = productData;

            command.ExecuteNonQuery();
        }

        public static ObservableCollection<Product> GetAllProducts()
        {
            ObservableCollection<Product> result = new ObservableCollection<Product>();

            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6} FROM {7}",
                ToolConstants.DB_PRODUCT_ID,
                ToolConstants.DB_PRODUCT_SORT,
                ToolConstants.DB_PRODUCT_AMOUNT,
                ToolConstants.DB_PRODUCT_ORIGIN,
                ToolConstants.DB_PRODUCT_PRODUCER_ID,
                ToolConstants.DB_PRODUCT_SUPPLIER_ID,
                ToolConstants.DB_PRODUCT_DATA,
                ToolConstants.DB_PRODUCT_TABLE
                );

            SQLiteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                MemoryStream memoryStream = new MemoryStream((byte[])reader.GetValue(6));
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                Product product = (Product)binaryFormatter.Deserialize(memoryStream);

                product.Id = Convert.ToInt32(reader.GetValue(0));
                product.Sort = Convert.ToString(reader.GetValue(1));
                product.Amount = Convert.ToInt32(reader.GetValue(2));
                product.Origin = Convert.ToString(reader.GetValue(3));
                product.ProducerId = Convert.ToInt32(reader.GetValue(4));
                product.SupplierId = Convert.ToInt32(reader.GetValue(5));

                result.Add(product);
            }
            return result;
        }
    }
}