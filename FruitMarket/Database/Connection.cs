using FruitMarket.Helper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Database
{
    public class Connection
    {
        private static SQLiteConnection c_Connection = null;
        private static string c_DataSource =
            Path.Combine(ToolConstants.DB_PATH, ToolConstants.DB_FILENAME);

        public static SQLiteConnection GetConnection()
        {
            if(c_Connection == null)
            {
                c_Connection = new SQLiteConnection();
                c_Connection.ConnectionString = "Data Source=" + c_DataSource;
                c_Connection.Open();

                PrepareDB();
            }
            if(c_Connection.State != System.Data.ConnectionState.Open)
            {
                c_Connection.Open();
            }
            return c_Connection;
        }

        public static void CloseConnection()
        {
            if(c_Connection != null)
            {
                c_Connection.Close();
                c_Connection = null;
            }
        }

        private static void PrepareDB()
        {
            SQLiteCommand command = new SQLiteCommand(c_Connection);

            command.CommandText = ToolConstants.DB_SORT;
            command.ExecuteNonQuery();

            command.CommandText = ToolConstants.DB_COUNTRY;
            command.ExecuteNonQuery();

            command.CommandText = ToolConstants.DB_PRODUCT;
            command.ExecuteNonQuery();

            command.CommandText = ToolConstants.DB_ADRESS;
            command.ExecuteNonQuery();

            command.CommandText = ToolConstants.DB_SUPPLIER;
            command.ExecuteNonQuery();

            command.CommandText = ToolConstants.DB_PRODUCER;
            command.ExecuteNonQuery();

            command.CommandText = ToolConstants.DB_COSTUMER;
            command.ExecuteNonQuery();

            command.CommandText = ToolConstants.DB_DELIVERY;
            command.ExecuteNonQuery();

        }
    }
}
