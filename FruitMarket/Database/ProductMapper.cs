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

        private static void SaveProduct(Product p_Product)
        {
            SQLiteConnection con = Connection.GetConnection();

            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}) " +
                "VALUES (@1, @2, @3, @4, @5, @6, @7)",
                ToolConstants.DB_PRODUCT_TABLE,
                ToolConstants.DB_PRODUCT_SORT,
                ToolConstants.DB_PRODUCT_AMOUNT,
                ToolConstants.DB_PRODUCT_PRODUCER_ID,
                ToolConstants.DB_PRODUCT_PRODUCER_NAME,
                ToolConstants.DB_PRODUCT_SUPPLIER_ID,
                ToolConstants.DB_PRODUCT_SUPPLIER_NAME,
                ToolConstants.DB_PRODUCT_DATA);

            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, p_Product);

            byte[] productData = memoryStream.ToArray();
            memoryStream.Close();

            command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Product.Sort;
            command.Parameters.Add("@2", System.Data.DbType.Int32).Value = p_Product.Amount;
            command.Parameters.Add("@3", System.Data.DbType.Int32).Value = p_Product.Producer.Id;
            command.Parameters.Add("@4", System.Data.DbType.String).Value = p_Product.Producer.Company;
            command.Parameters.Add("@5", System.Data.DbType.Int32).Value = p_Product.Supplier.Id;
            command.Parameters.Add("@6", System.Data.DbType.String).Value = p_Product.Supplier.Company;
            command.Parameters.Add("@7", System.Data.DbType.Binary, productData.Length).Value = productData;

            command.ExecuteNonQuery();
        }
    }
}