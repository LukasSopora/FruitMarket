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
    public class SortMapper
    {
        public static void SaveSorts(ObservableCollection<string> p_Sorts)
        {
            foreach(string sort in p_Sorts)
            {
                SaveSort(sort);
            }
        }

        public static void SaveSort(string p_Sort)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            bool alreadyExists = false;

            command.CommandText = string.Format(
                "SELECT {0} FROM {1} " +
                "WHERE {0} = @0",
                ToolConstants.DB_SORT_SORTNAME,
                ToolConstants.DB_SORT_TABLE);
            command.Parameters.Add("@0", System.Data.DbType.String).Value = p_Sort;

            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();

            if(reader.HasRows)
            {
                alreadyExists = true;
            }
            reader.Close();

            if(!alreadyExists)
            {
                command.CommandText = string.Format(
                "INSERT INTO {0} ({1}) " +
                "VALUES (@1)",
                ToolConstants.DB_SORT_TABLE,
                ToolConstants.DB_SORT_SORTNAME);
                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Sort;

                command.ExecuteNonQuery();
            }
        }

        public static ObservableCollection<string> GetAllSorts()
        {
            ObservableCollection<string> result = 
                new ObservableCollection<string>();

            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0} FROM {1}",
                ToolConstants.DB_SORT_SORTNAME,
                ToolConstants.DB_SORT_TABLE);

            SQLiteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                result.Add(reader.GetString(0));
            }

            return result;
        }
    }
}
