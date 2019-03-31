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
    public class ProducerMapper
    {
        public static void SaveProducers(ObservableCollection<Producer> p_Producers)
        {
            foreach(Producer producer in p_Producers)
            {
                SaveProducer(producer);
            }
        }

        public static void SaveProducer(Producer p_Producer)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            bool alreadyExists = false;

            command.CommandText = string.Format(
                "SELECT {0} FROM {1} " +
                "WHERE {0} = @0",
                ToolConstants.DB_PRODUCER_ID,
                ToolConstants.DB_PRODUCER_TABLE);

            command.Parameters.Add("@0", System.Data.DbType.Int32).Value = p_Producer.Id;

            SQLiteDataReader reader = command.ExecuteReader();

            if(reader.HasRows)
            {
                alreadyExists = true;
            }

            reader.Close();

            if(alreadyExists)
            {
                command.CommandText = string.Format(
                    "UPDATE {0} SET " +
                    "{1} = @1, " + 
                    "{2} = @2, " + 
                    "{3} = @3, " + 
                    "{4} = @4 " +
                    "WHERE {5} = @5",
                    ToolConstants.DB_PRODUCER_TABLE,
                    ToolConstants.DB_PRODUCER_LAST_NAME,
                    ToolConstants.DB_PRODUCER_FIRST_NAME,
                    ToolConstants.DB_PRODUCER_COMPANY,
                    ToolConstants.DB_PRODUCER_DATA,
                    ToolConstants.DB_PRODUCER_ID
                    );

                MemoryStream memoryStream = new MemoryStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, p_Producer);

                byte[] producerData = memoryStream.ToArray();
                memoryStream.Close();

                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Producer.LastName;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Producer.FirstName;
                command.Parameters.Add("@3", System.Data.DbType.String).Value = p_Producer.Company;
                command.Parameters.Add("@4", System.Data.DbType.Binary, producerData.Length).Value = producerData;
                command.Parameters.Add("@5", System.Data.DbType.Int32).Value = p_Producer.Id;

                command.ExecuteNonQuery();
            }
            else
            {
                command.CommandText = string.Format(
                "INSERT INTO {0} ({1}, {2}, {3}, {4}) " +
                "VALUES (@1, @2, @3, @4)",
                ToolConstants.DB_PRODUCER_TABLE,
                ToolConstants.DB_PRODUCER_LAST_NAME,
                ToolConstants.DB_PRODUCER_FIRST_NAME,
                ToolConstants.DB_PRODUCER_COMPANY,
                ToolConstants.DB_PRODUCER_DATA);

                MemoryStream memoryStream = new MemoryStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, p_Producer);

                byte[] productData = memoryStream.ToArray();
                memoryStream.Close();

                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Producer.LastName;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Producer.FirstName;
                command.Parameters.Add("@3", System.Data.DbType.String).Value = p_Producer.Company;
                command.Parameters.Add("@4", System.Data.DbType.Binary, productData.Length).Value = productData;

                command.ExecuteNonQuery();
            }
        }

        public static ObservableCollection<Producer> GetAllProduers()
        {
            ObservableCollection<Producer> result = new ObservableCollection<Producer>();

            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}, {1}, {2}, {3}, {4} FROM {5}",
                ToolConstants.DB_PRODUCER_ID,
                ToolConstants.DB_PRODUCER_LAST_NAME,
                ToolConstants.DB_PRODUCER_FIRST_NAME,
                ToolConstants.DB_PRODUCER_COMPANY,
                ToolConstants.DB_PRODUCER_DATA,
                ToolConstants.DB_PRODUCER_TABLE);

            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                MemoryStream memoryStream = new MemoryStream((byte[])reader.GetValue(4));
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                Producer producer = (Producer)binaryFormatter.Deserialize(memoryStream);

                producer.Id = Convert.ToInt32(reader.GetValue(0));
                producer.LastName = Convert.ToString(reader.GetValue(1));
                producer.FirstName = Convert.ToString(reader.GetValue(2));
                producer.Company = Convert.ToString(reader.GetValue(3));

                result.Add(producer);
            }
            return result;
        }

        public static void DeleteProducer(int p_ProducerId)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "DELETE FROM {0} " +
                "WHERE {1} = @1",
                ToolConstants.DB_PRODUCER_TABLE,
                ToolConstants.DB_PRODUCER_ID);

            command.Parameters.Add("@1", System.Data.DbType.Int32).Value = p_ProducerId;

            command.ExecuteNonQuery();
        }
    }
}
