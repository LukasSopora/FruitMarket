using FruitMarket.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Database
{
    public class CountryMapper
    {
        public static void SaveCountries(ObservableCollection<string> p_Countries)
        {
            foreach(string conutry in p_Countries)
            {
                SaveCountry(conutry);
            }
        }

        public static void SaveCountry(string p_Country)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "INSERT INTO {0} ({1}) " +
                "VALUES (@1)",
                ToolConstants.DB_COUNTRY_TABLE,
                ToolConstants.DB_COUNTRY_COUNTRYNAME);
            command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Country;

            command.ExecuteNonQuery();
        }

        public static void DeleteAllCountries()
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "DELETE FROM {0}",
                ToolConstants.DB_COUNTRY_TABLE);
            command.ExecuteNonQuery();
        }

        public static ObservableCollection<string> GetAllCountries()
        {
            ObservableCollection<string> result = new ObservableCollection<string>();

            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0} FROM {1}",
                ToolConstants.DB_COUNTRY_COUNTRYNAME,
                ToolConstants.DB_COUNTRY_TABLE);

            SQLiteDataReader reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                result.Add(reader.GetString(0));
            }

            return result;
        }

        public static int GetCountryAmount()
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT COUNT (*) FROM {0}",
                ToolConstants.DB_COUNTRY_TABLE);

            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();

            return reader.GetInt32(0);
        }
    }
}
