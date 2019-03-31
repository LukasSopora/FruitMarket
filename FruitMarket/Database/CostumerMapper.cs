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
    public class CostumerMapper
    {
        public static void SaveCostumers(ObservableCollection<Costumer> p_Costumers)
        {
            foreach (Costumer costumer in p_Costumers)
            {
                SaveCostumer(costumer);
            }
        }

        public static void SaveCostumer(Costumer p_Costumer)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            bool alreadyExists = false;

            command.CommandText = string.Format(
                "SELECT {0} FROM {1} " +
                "WHERE {0} = @0",
                ToolConstants.DB_COSTUMER_ID,
                ToolConstants.DB_COSTUMER_TABLE);

            command.Parameters.Add("@0", System.Data.DbType.Int32).Value = p_Costumer.Id;

            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                alreadyExists = true;
            }

            reader.Close();

            if (alreadyExists)
            {
                command.CommandText = string.Format(
                    "UPDATE {0} SET " +
                    "{1} = @1, " +
                    "{2} = @2, " +
                    "{3} = @3, " +
                    "{4} = @4 " +
                    "WHERE {5} = @5",
                    ToolConstants.DB_COSTUMER_TABLE,
                    ToolConstants.DB_COSTUMER_FIRST_NAME,
                    ToolConstants.DB_COSTUMER_LAST_NAME,
                    ToolConstants.DB_COSTUMER_COMPANY,
                    ToolConstants.DB_COSTUMER_DATA,
                    ToolConstants.DB_COSTUMER_ID
                    );

                MemoryStream memoryStream = new MemoryStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, p_Costumer);

                byte[] CostumerData = memoryStream.ToArray();
                memoryStream.Close();

                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Costumer.LastName;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Costumer.FirstName;
                command.Parameters.Add("@3", System.Data.DbType.String).Value = p_Costumer.Company;
                command.Parameters.Add("@4", System.Data.DbType.Binary, CostumerData.Length).Value = CostumerData;
                command.Parameters.Add("@5", System.Data.DbType.Int32).Value = p_Costumer.Id;

                command.ExecuteNonQuery();
            }
            else
            {
                command.CommandText = string.Format(
                "INSERT INTO {0} ({1}, {2}, {3}, {4}) " +
                "VALUES (@1, @2, @3, @4)",
                ToolConstants.DB_COSTUMER_TABLE,
                ToolConstants.DB_COSTUMER_LAST_NAME,
                ToolConstants.DB_COSTUMER_FIRST_NAME,
                ToolConstants.DB_COSTUMER_COMPANY,
                ToolConstants.DB_COSTUMER_DATA);

                MemoryStream memoryStream = new MemoryStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, p_Costumer);

                byte[] CostumerData = memoryStream.ToArray();
                memoryStream.Close();

                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Costumer.LastName;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Costumer.FirstName;
                command.Parameters.Add("@3", System.Data.DbType.String).Value = p_Costumer.Company;
                command.Parameters.Add("@4", System.Data.DbType.Binary, CostumerData.Length).Value = CostumerData;

                command.ExecuteNonQuery();
            }
        }

        public static ObservableCollection<Costumer> GetAllCostumers()
        {
            ObservableCollection<Costumer> result = new ObservableCollection<Costumer>();

            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}, {1}, {2}, {3}, {4} FROM {5}",
                ToolConstants.DB_COSTUMER_ID,
                ToolConstants.DB_COSTUMER_LAST_NAME,
                ToolConstants.DB_COSTUMER_FIRST_NAME,
                ToolConstants.DB_COSTUMER_COMPANY,
                ToolConstants.DB_COSTUMER_DATA,
                ToolConstants.DB_COSTUMER_TABLE);

            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                MemoryStream memoryStream = new MemoryStream((byte[])reader.GetValue(4));
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                Costumer costumer = (Costumer)binaryFormatter.Deserialize(memoryStream);

                costumer.Id = Convert.ToInt32(reader.GetValue(0));
                costumer.LastName = Convert.ToString(reader.GetValue(1));
                costumer.FirstName = Convert.ToString(reader.GetValue(2));
                costumer.Company = Convert.ToString(reader.GetValue(3));

                result.Add(costumer);
            }
            return result;
        }

        public static void DeleteCostumer(int p_CostumerId)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "DELETE FROM {0} " +
                "WHERE {1} = @1",
                ToolConstants.DB_COSTUMER_TABLE,
                ToolConstants.DB_COSTUMER_ID);

            command.Parameters.Add("@1", System.Data.DbType.Int32).Value = p_CostumerId;

            command.ExecuteNonQuery();
        }
    }
}
