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
    public class ProducerMapper
    {
        public static void SaveProducers(ObservableCollection<Producer> p_Producers)
        {
            foreach(Producer producer in p_Producers)
            {
                SaveProducer(producer);
            }
        }

        private static void SaveProducer(Producer p_Producer)
        {
            SQLiteConnection con = Connection.GetConnection();

            SQLiteCommand command = new SQLiteCommand(con);

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
    }
}