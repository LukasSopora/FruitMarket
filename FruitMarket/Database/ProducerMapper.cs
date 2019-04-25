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
                    "{4} = @4, " +
                    "{5} = @5, " +
                    "{6} = @6, " +
                    "{7} = @7 " +
                    "WHERE {8} = @8",
                    ToolConstants.DB_PRODUCER_TABLE,
                    ToolConstants.DB_PRODUCER_LAST_NAME,
                    ToolConstants.DB_PRODUCER_FIRST_NAME,
                    ToolConstants.DB_PRODUCER_ADRESS_ID,
                    ToolConstants.DB_PRODUCER_BIRTHDAY,
                    ToolConstants.DB_PRODUCER_PHONE,
                    ToolConstants.DB_PRODUCER_COMPANY,
                    ToolConstants.DB_PRODUCER_EMAIL,
                    ToolConstants.DB_PRODUCER_ID);

                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Producer.LastName;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Producer.FirstName;
                command.Parameters.Add("@3", System.Data.DbType.Int32).Value = p_Producer.Adress.Id;
                command.Parameters.Add("@4", System.Data.DbType.DateTime).Value = p_Producer.Birthday;
                command.Parameters.Add("@5", System.Data.DbType.String).Value = p_Producer.Phone;
                command.Parameters.Add("@6", System.Data.DbType.String).Value = p_Producer.Company;
                command.Parameters.Add("@7", System.Data.DbType.String).Value = p_Producer.Email;
                command.Parameters.Add("@8", System.Data.DbType.Int32).Value = p_Producer.Id;

                command.ExecuteNonQuery();

                AdressMapper.SaveAdress(p_Producer.Adress);
            }
            else
            {
                var adressId = AdressMapper.SaveAdress(p_Producer.Adress);
                p_Producer.Adress.Id = adressId;

                command.CommandText = string.Format(
                "INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}) " +
                "VALUES (@1, @2, @3, @4, @5, @6, @7)",
                ToolConstants.DB_PRODUCER_TABLE,
                ToolConstants.DB_PRODUCER_LAST_NAME,
                ToolConstants.DB_PRODUCER_FIRST_NAME,
                ToolConstants.DB_PRODUCER_ADRESS_ID,
                ToolConstants.DB_PRODUCER_BIRTHDAY,
                ToolConstants.DB_PRODUCER_PHONE,
                ToolConstants.DB_PRODUCER_COMPANY,
                ToolConstants.DB_PRODUCER_EMAIL);

                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Producer.LastName;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Producer.FirstName;
                command.Parameters.Add("@3", System.Data.DbType.Int32).Value = p_Producer.Adress.Id;
                command.Parameters.Add("@4", System.Data.DbType.DateTime).Value = p_Producer.Birthday;
                command.Parameters.Add("@5", System.Data.DbType.String).Value = p_Producer.Phone;
                command.Parameters.Add("@6", System.Data.DbType.String).Value = p_Producer.Company;
                command.Parameters.Add("@7", System.Data.DbType.String).Value = p_Producer.Email;

                command.ExecuteNonQuery();
            }
        }

        public static ObservableCollection<Producer> GetAllProducers()
        {
            ObservableCollection<Producer> result = new ObservableCollection<Producer>();

            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            //Build SQLite query
            command.CommandText = string.Format(
                "SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} FROM {8}",
                ToolConstants.DB_PRODUCER_ID,
                ToolConstants.DB_PRODUCER_LAST_NAME,
                ToolConstants.DB_PRODUCER_FIRST_NAME,
                ToolConstants.DB_PRODUCER_ADRESS_ID,
                ToolConstants.DB_PRODUCER_BIRTHDAY,
                ToolConstants.DB_PRODUCER_PHONE,
                ToolConstants.DB_PRODUCER_COMPANY,
                ToolConstants.DB_PRODUCER_EMAIL,
                ToolConstants.DB_PRODUCER_TABLE);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Producer producer = new Producer();

                producer.Id = Convert.ToInt32(reader.GetValue(0));
                producer.LastName = Convert.ToString(reader.GetValue(1));
                producer.FirstName = Convert.ToString(reader.GetValue(2));
                producer.Adress = AdressMapper.GetAdressById(reader.GetInt32(3));
                producer.Birthday = Convert.ToDateTime(reader.GetValue(4));
                producer.Phone = Convert.ToString(reader.GetValue(5));
                producer.Company = Convert.ToString(reader.GetValue(6));
                producer.Email = Convert.ToString(reader.GetValue(7));

                result.Add(producer);
            }
            return result;
        }

        public static Producer GetProducerById(int p_ProducerId)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} FROM {8}" +
                "WHERE {0} = @0",
                ToolConstants.DB_PRODUCER_ID,
                ToolConstants.DB_PRODUCER_LAST_NAME,
                ToolConstants.DB_PRODUCER_FIRST_NAME,
                ToolConstants.DB_PRODUCER_ADRESS_ID,
                ToolConstants.DB_PRODUCER_BIRTHDAY,
                ToolConstants.DB_PRODUCER_PHONE,
                ToolConstants.DB_PRODUCER_COMPANY,
                ToolConstants.DB_PRODUCER_EMAIL,
                ToolConstants.DB_PRODUCER_TABLE);
            command.Parameters.Add("@0", System.Data.DbType.Int32).Value = p_ProducerId;

            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Producer result = new Producer();

                result.Id = Convert.ToInt32(reader.GetValue(0));
                result.LastName = Convert.ToString(reader.GetValue(1));
                result.FirstName = Convert.ToString(reader.GetValue(2));
                result.Adress = AdressMapper.GetAdressById(reader.GetInt32(3));
                result.Birthday = Convert.ToDateTime(reader.GetValue(4));
                result.Phone = Convert.ToString(reader.GetValue(5));
                result.Company = Convert.ToString(reader.GetValue(6));
                result.Email = Convert.ToString(reader.GetValue(7));

                return result;
            }
            else
            {
                return null;
            }
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
