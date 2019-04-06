using FruitMarket.Helper;
using FruitMarket.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Database
{
    public class AdressMapper
    {
        public static void SaveAdresses(IList<Adress> p_Adresses)
        {
            foreach(Adress adress in p_Adresses)
            {
                SaveAdress(adress);
            }
        }

        /// <summary>
        /// Saves an adress to the database. If the adress alreay exists, its values are just updated.
        /// The function reutrns the column index of the insertet / updated adress.
        /// </summary>
        /// <param name="p_Adress"></param>
        /// <returns>Index of insertet / updated adress</returns>
        public static int SaveAdress(Adress p_Adress)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);
            bool alreadyExists = false;

            command.CommandText = string.Format(
                "SELECT {0} FROM {1} " +
                "WHERE {0} = @0",
                ToolConstants.DB_ADRESS_ID,
                ToolConstants.DB_ADRESS_TABLE);
            command.Parameters.Add("@0", System.Data.DbType.Int32).Value = p_Adress.Id;

            SQLiteDataReader reader = command.ExecuteReader();
            if(reader.HasRows)
            {
                alreadyExists = true;
            }
            reader.Close();

            if(alreadyExists)
            {
                command.CommandText = string.Format(
                    "UPDATE {0} SET" +
                    "{1} = @1, " +
                    "{2} = @2, " +
                    "{3} = @3 " +
                    "WHERE {4} = @4",
                    ToolConstants.DB_ADRESS,
                    ToolConstants.DB_ADRESS_STREET,
                    ToolConstants.DB_ADRESS_POSTCODE,
                    ToolConstants.DB_ADRESS_PLACE,
                    ToolConstants.DB_ADRESS_ID
                    );
                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Adress.Street;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Adress.PostCode;
                command.Parameters.Add("@3", System.Data.DbType.String).Value = p_Adress.Place;
                command.Parameters.Add("@4", System.Data.DbType.Int32).Value = p_Adress.Id;

                command.ExecuteNonQuery();

                return p_Adress.Id;
            }
            else
            {
                command.CommandText = string.Format(
                "INSERT INTO {0} ({1}, {2}, {3}) " +
                "VALUES (@1, @2, @3)",
                ToolConstants.DB_ADRESS_TABLE,
                ToolConstants.DB_ADRESS_STREET,
                ToolConstants.DB_ADRESS_POSTCODE,
                ToolConstants.DB_ADRESS_PLACE);
                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Adress.Street;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Adress.PostCode;
                command.Parameters.Add("@3", System.Data.DbType.String).Value = p_Adress.Place;

                command.ExecuteNonQuery();

                command.CommandText = "SELECT last_insert_rowid()";
                reader = command.ExecuteReader();
                reader.Read();
                int adressID = reader.GetInt32(0);
                reader.Close();
                return adressID;
            }
        }

        public static Adress GetAdressById(int p_AdressId)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}, {1}, {2}, {3} FROM {4} " +
                "WHERE {0} = @0",
                ToolConstants.DB_ADRESS_ID,
                ToolConstants.DB_ADRESS_STREET,
                ToolConstants.DB_ADRESS_POSTCODE,
                ToolConstants.DB_ADRESS_PLACE,
                ToolConstants.DB_ADRESS_TABLE);
            command.Parameters.Add("@0", System.Data.DbType.Int32).Value = p_AdressId;

            SQLiteDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                Adress result = new Adress();
                result.Id = reader.GetInt32(0);
                result.Street = reader.GetString(1);
                result.PostCode = reader.GetString(2);
                result.Place = reader.GetString(3);

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void DeleteAdress(int p_AdressId)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "DELETE FROM {0} " +
                "WHERE {1} = @1",
                ToolConstants.DB_ADRESS_TABLE,
                ToolConstants.DB_ADRESS_ID);
            command.Parameters.Add("@1", System.Data.DbType.Int32).Value = p_AdressId;

            command.ExecuteNonQuery();
        }
    }
}
