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
                    "{4} = @4, " +
                    "{5} = @5, " +
                    "{6} = @6, " +
                    "{7} = @7 " +
                    "WHERE {8} = @8",
                    ToolConstants.DB_COSTUMER_TABLE,
                    ToolConstants.DB_COSTUMER_LAST_NAME,
                    ToolConstants.DB_COSTUMER_FIRST_NAME,
                    ToolConstants.DB_COSTUMER_ADRESS_ID,
                    ToolConstants.DB_COSTUMER_BIRTHDAY,
                    ToolConstants.DB_COSTUMER_PHONE,
                    ToolConstants.DB_COSTUMER_COMPANY,
                    ToolConstants.DB_COSTUMER_EMAIL,
                    ToolConstants.DB_COSTUMER_ID);

                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Costumer.LastName;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Costumer.FirstName;
                command.Parameters.Add("@3", System.Data.DbType.Int32).Value = p_Costumer.Adress.Id;
                command.Parameters.Add("@4", System.Data.DbType.DateTime).Value = p_Costumer.Birthday;
                command.Parameters.Add("@5", System.Data.DbType.String).Value = p_Costumer.Phone;
                command.Parameters.Add("@6", System.Data.DbType.String).Value = p_Costumer.Company;
                command.Parameters.Add("@7", System.Data.DbType.String).Value = p_Costumer.Email;
                command.Parameters.Add("@8", System.Data.DbType.Int32).Value = p_Costumer.Id;

                command.ExecuteNonQuery();

                AdressMapper.SaveAdress(p_Costumer.Adress);
            }
            else
            {
                var adressId = AdressMapper.SaveAdress(p_Costumer.Adress);
                p_Costumer.Adress.Id = adressId;

                command.CommandText = string.Format(
                "INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}) " +
                "VALUES (@1, @2, @3, @4, @5, @6, @7)",
                ToolConstants.DB_COSTUMER_TABLE,
                ToolConstants.DB_COSTUMER_LAST_NAME,
                ToolConstants.DB_COSTUMER_FIRST_NAME,
                ToolConstants.DB_COSTUMER_ADRESS_ID,
                ToolConstants.DB_COSTUMER_BIRTHDAY,
                ToolConstants.DB_COSTUMER_PHONE,
                ToolConstants.DB_COSTUMER_COMPANY,
                ToolConstants.DB_COSTUMER_EMAIL);

                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Costumer.LastName;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Costumer.FirstName;
                command.Parameters.Add("@3", System.Data.DbType.Int32).Value = p_Costumer.Adress.Id;
                command.Parameters.Add("@4", System.Data.DbType.DateTime).Value = p_Costumer.Birthday;
                command.Parameters.Add("@5", System.Data.DbType.String).Value = p_Costumer.Phone;
                command.Parameters.Add("@6", System.Data.DbType.String).Value = p_Costumer.Company;
                command.Parameters.Add("@7", System.Data.DbType.String).Value = p_Costumer.Email;

                command.ExecuteNonQuery();
            }
        }

        public static ObservableCollection<Costumer> GetAllCostumers()
        {
            ObservableCollection<Costumer> result = new ObservableCollection<Costumer>();

            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} FROM {8}",
                ToolConstants.DB_COSTUMER_ID,
                ToolConstants.DB_COSTUMER_LAST_NAME,
                ToolConstants.DB_COSTUMER_FIRST_NAME,
                ToolConstants.DB_COSTUMER_ADRESS_ID,
                ToolConstants.DB_COSTUMER_BIRTHDAY,
                ToolConstants.DB_COSTUMER_PHONE,
                ToolConstants.DB_COSTUMER_COMPANY,
                ToolConstants.DB_COSTUMER_EMAIL,
                ToolConstants.DB_COSTUMER_TABLE);

            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Costumer costumer = new Costumer();

                costumer.Id = Convert.ToInt32(reader.GetValue(0));
                costumer.LastName = Convert.ToString(reader.GetValue(1));
                costumer.FirstName = Convert.ToString(reader.GetValue(2));
                costumer.Adress = AdressMapper.GetAdressById(reader.GetInt32(3));
                costumer.Birthday = Convert.ToDateTime(reader.GetValue(4));
                costumer.Phone = Convert.ToString(reader.GetValue(5));
                costumer.Company = Convert.ToString(reader.GetValue(6));
                costumer.Email = Convert.ToString(reader.GetValue(7));

                result.Add(costumer);
            }
            return result;
        }

        public static Costumer GetCostumerById(int p_CostumerId)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} FROM {8}" +
                "WHERE {0} = @0",
                ToolConstants.DB_COSTUMER_ID,
                ToolConstants.DB_COSTUMER_LAST_NAME,
                ToolConstants.DB_COSTUMER_FIRST_NAME,
                ToolConstants.DB_COSTUMER_ADRESS_ID,
                ToolConstants.DB_COSTUMER_BIRTHDAY,
                ToolConstants.DB_COSTUMER_PHONE,
                ToolConstants.DB_COSTUMER_COMPANY,
                ToolConstants.DB_COSTUMER_EMAIL,
                ToolConstants.DB_COSTUMER_TABLE);
            command.Parameters.Add("@0", System.Data.DbType.Int32).Value = p_CostumerId;

            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Costumer result = new Costumer();

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
