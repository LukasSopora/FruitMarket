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
                "INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}) " +
                "VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)",
                ToolConstants.DB_PRODUCT_TABLE,
                ToolConstants.DB_PRODUCT_SORT,
                ToolConstants.DB_PRODUCT_ORIGIN,
                ToolConstants.DB_PRODUCT_AMOUNT,
                ToolConstants.DB_PRODUCT_PRODUCER_ID,
                ToolConstants.DB_PRODUCT_SUPPLIER_ID,
                ToolConstants.DB_PRODUCT_CATEGORY,
                ToolConstants.DB_PRODUCT_PURCHASEDATE,
                ToolConstants.DB_PRODUCT_EXPIRATION,
                ToolConstants.DB_PRODUCT_MATURE,
                ToolConstants.DB_PRODUCT_PURCHASEPRICE,
                ToolConstants.DB_PRODUCT_SALESPRICE);
            command.Parameters.Add("@1", System.Data.DbType.String);
            command.Parameters.Add("@2", System.Data.DbType.String);
            command.Parameters.Add("@3", System.Data.DbType.Int32);
            command.Parameters.Add("@4", System.Data.DbType.Int32);
            command.Parameters.Add("@5", System.Data.DbType.Int32);
            command.Parameters.Add("@6", System.Data.DbType.String);
            command.Parameters.Add("@7", System.Data.DbType.DateTime);
            command.Parameters.Add("@8", System.Data.DbType.DateTime);
            command.Parameters.Add("@9", System.Data.DbType.Int32);
            command.Parameters.Add("@10", System.Data.DbType.Double);
            command.Parameters.Add("@11", System.Data.DbType.Double);

            foreach(var product in p_Products)
            {
                command.Parameters[0].Value = product.Sort;
                command.Parameters[1].Value = product.Origin;
                command.Parameters[2].Value = product.Amount;
                command.Parameters[3].Value = product.ProducerId;
                command.Parameters[4].Value = product.SupplierId;
                command.Parameters[5].Value = product.Category;
                command.Parameters[6].Value = product.PurchaseDate;
                command.Parameters[7].Value = product.Expiration;
                command.Parameters[8].Value = product.Mature;
                command.Parameters[9].Value = product.PurchasePrice;
                command.Parameters[10].Value = product.SalesPrice;

                command.ExecuteNonQuery();
            }

        }

        private static void SaveProduct(Product p_Product)
        {
            SQLiteConnection con = Connection.GetConnection();

            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}) " +
                "VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)",
                ToolConstants.DB_PRODUCT_TABLE,
                ToolConstants.DB_PRODUCT_SORT,
                ToolConstants.DB_PRODUCT_ORIGIN,
                ToolConstants.DB_PRODUCT_AMOUNT,
                ToolConstants.DB_PRODUCT_PRODUCER_ID,
                ToolConstants.DB_PRODUCT_SUPPLIER_ID,
                ToolConstants.DB_PRODUCT_CATEGORY,
                ToolConstants.DB_PRODUCT_PURCHASEDATE,
                ToolConstants.DB_PRODUCT_EXPIRATION,
                ToolConstants.DB_PRODUCT_MATURE,
                ToolConstants.DB_PRODUCT_PURCHASEPRICE,
                ToolConstants.DB_PRODUCT_SALESPRICE
                );
            command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Product.Sort;
            command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Product.Origin;
            command.Parameters.Add("@3", System.Data.DbType.Int32).Value = p_Product.Amount;
            command.Parameters.Add("@4", System.Data.DbType.Int32).Value = p_Product.ProducerId;
            command.Parameters.Add("@5", System.Data.DbType.Int32).Value = p_Product.SupplierId;
            command.Parameters.Add("@6", System.Data.DbType.String).Value = p_Product.Category;
            command.Parameters.Add("@7", System.Data.DbType.DateTime).Value = p_Product.PurchaseDate;
            command.Parameters.Add("@8", System.Data.DbType.DateTime).Value = p_Product.Expiration;
            command.Parameters.Add("@9", System.Data.DbType.Int32).Value = p_Product.Mature;
            command.Parameters.Add("@10", System.Data.DbType.Double).Value = p_Product.PurchasePrice;
            command.Parameters.Add("@11", System.Data.DbType.Double).Value = p_Product.SalesPrice;

            command.ExecuteNonQuery();
        }

        public static ObservableCollection<Product> GetAllProducts()
        {
            ObservableCollection<Product> result = new ObservableCollection<Product>();

            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11} FROM {12}",
                ToolConstants.DB_PRODUCT_ID,
                ToolConstants.DB_PRODUCT_SORT,
                ToolConstants.DB_PRODUCT_AMOUNT,
                ToolConstants.DB_PRODUCT_ORIGIN,
                ToolConstants.DB_PRODUCT_PRODUCER_ID,
                ToolConstants.DB_PRODUCT_SUPPLIER_ID,
                ToolConstants.DB_PRODUCT_CATEGORY,
                ToolConstants.DB_PRODUCT_PURCHASEDATE,
                ToolConstants.DB_PRODUCT_EXPIRATION,
                ToolConstants.DB_PRODUCT_MATURE,
                ToolConstants.DB_PRODUCT_PURCHASEPRICE,
                ToolConstants.DB_PRODUCT_SALESPRICE,
                ToolConstants.DB_PRODUCT_TABLE
                );

            SQLiteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                var product = new Product();

                product.Id = Convert.ToInt32(reader.GetValue(0));
                product.Sort = Convert.ToString(reader.GetValue(1));
                product.Amount = Convert.ToInt32(reader.GetValue(2));
                product.Origin = Convert.ToString(reader.GetValue(3));
                product.ProducerId = Convert.ToInt32(reader.GetValue(4));
                product.SupplierId = Convert.ToInt32(reader.GetValue(5));
                product.Category = Convert.ToString(reader.GetValue(6));
                product.PurchaseDate = Convert.ToDateTime(reader.GetValue(7));
                product.Expiration = Convert.ToDateTime(reader.GetValue(8));
                product.Mature = Convert.ToInt32(reader.GetValue(9));
                product.PurchasePrice = Convert.ToDouble(reader.GetValue(10));
                product.SalesPrice = Convert.ToDouble(reader.GetValue(11));

                result.Add(product);
            }
            return result;
        }
    }
}