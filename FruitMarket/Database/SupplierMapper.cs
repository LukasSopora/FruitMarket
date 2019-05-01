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
    class SupplierMapper
    {
        public static void SaveSuppliers(ObservableCollection<Supplier> p_Suppliers)
        {
            foreach (Supplier supplier in p_Suppliers)
            {
                SaveSupplier(supplier);
            }
        }

        /// <summary>
        /// Saves an supplier to a database. If the supplier already exists,
        /// its values are updated. This function also calls the function
        /// for saving the suppliers adress.
        /// </summary>
        /// <param name="p_Supplier">Supplier to save</param>
        public static void SaveSupplier(Supplier p_Supplier)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            bool alreadyExists = false;

            command.CommandText = string.Format(
                "SELECT {0} FROM {1} " +
                "WHERE {0} = @0",
                ToolConstants.DB_SUPPLIER_ID,
                ToolConstants.DB_SUPPLIER_TABLE);

            command.Parameters.Add("@0", System.Data.DbType.Int32).Value = p_Supplier.Id;

            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                alreadyExists = true;
            }

            reader.Close();

            if (alreadyExists) //update values
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
                    ToolConstants.DB_SUPPLIER_TABLE,
                    ToolConstants.DB_SUPPLIER_LAST_NAME,
                    ToolConstants.DB_SUPPLIER_FIRST_NAME,
                    ToolConstants.DB_SUPPLIER_ADRESS_ID,
                    ToolConstants.DB_SUPPLIER_BIRTHDAY,
                    ToolConstants.DB_SUPPLIER_PHONE,
                    ToolConstants.DB_SUPPLIER_COMPANY,
                    ToolConstants.DB_SUPPLIER_EMAIL,
                    ToolConstants.DB_SUPPLIER_ID);

                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Supplier.LastName;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Supplier.FirstName;
                command.Parameters.Add("@3", System.Data.DbType.Int32).Value = p_Supplier.Adress.Id;
                command.Parameters.Add("@4", System.Data.DbType.DateTime).Value = p_Supplier.Birthday;
                command.Parameters.Add("@5", System.Data.DbType.String).Value = p_Supplier.Phone;
                command.Parameters.Add("@6", System.Data.DbType.String).Value = p_Supplier.Company;
                command.Parameters.Add("@7", System.Data.DbType.String).Value = p_Supplier.Email;
                command.Parameters.Add("@8", System.Data.DbType.Int32).Value = p_Supplier.Id;

                command.ExecuteNonQuery();

                AdressMapper.SaveAdress(p_Supplier.Adress);
            }
            else //not exising --> create new one
            {
                var adressId = AdressMapper.SaveAdress(p_Supplier.Adress);
                p_Supplier.Adress.Id = adressId;

                command.CommandText = string.Format(
                "INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}) " +
                "VALUES (@1, @2, @3, @4, @5, @6, @7)",
                ToolConstants.DB_SUPPLIER_TABLE,
                ToolConstants.DB_SUPPLIER_LAST_NAME,
                ToolConstants.DB_SUPPLIER_FIRST_NAME,
                ToolConstants.DB_SUPPLIER_ADRESS_ID,
                ToolConstants.DB_SUPPLIER_BIRTHDAY,
                ToolConstants.DB_SUPPLIER_PHONE,
                ToolConstants.DB_SUPPLIER_COMPANY,
                ToolConstants.DB_SUPPLIER_EMAIL);

                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Supplier.LastName;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Supplier.FirstName;
                command.Parameters.Add("@3", System.Data.DbType.Int32).Value = p_Supplier.Adress.Id;
                command.Parameters.Add("@4", System.Data.DbType.DateTime).Value = p_Supplier.Birthday;
                command.Parameters.Add("@5", System.Data.DbType.String).Value = p_Supplier.Phone;
                command.Parameters.Add("@6", System.Data.DbType.String).Value = p_Supplier.Company;
                command.Parameters.Add("@7", System.Data.DbType.String).Value = p_Supplier.Email;

                command.ExecuteNonQuery();
            }
        }

        public static ObservableCollection<Supplier> GetAllSuppliers()
        {
            ObservableCollection<Supplier> result = 
                new ObservableCollection<Supplier>();

            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} FROM {8}",
                ToolConstants.DB_SUPPLIER_ID,
                ToolConstants.DB_SUPPLIER_LAST_NAME,
                ToolConstants.DB_SUPPLIER_FIRST_NAME,
                ToolConstants.DB_SUPPLIER_ADRESS_ID,
                ToolConstants.DB_SUPPLIER_BIRTHDAY,
                ToolConstants.DB_SUPPLIER_PHONE,
                ToolConstants.DB_SUPPLIER_COMPANY,
                ToolConstants.DB_SUPPLIER_EMAIL,
                ToolConstants.DB_SUPPLIER_TABLE);

            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Supplier supplier = new Supplier();

                supplier.Id = Convert.ToInt32(reader.GetValue(0));
                supplier.LastName = Convert.ToString(reader.GetValue(1));
                supplier.FirstName = Convert.ToString(reader.GetValue(2));
                supplier.Adress = AdressMapper.GetAdressById(reader.GetInt32(3));
                supplier.Birthday = Convert.ToDateTime(reader.GetValue(4));
                supplier.Phone = Convert.ToString(reader.GetValue(5));
                supplier.Company = Convert.ToString(reader.GetValue(6));
                supplier.Email = Convert.ToString(reader.GetValue(7));

                result.Add(supplier);
            }
            return result;
        }

        public static Supplier GetSupplierById(int p_SupplierId)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} FROM {8}" +
                "WHERE {0} = @0",
                ToolConstants.DB_SUPPLIER_ID,
                ToolConstants.DB_SUPPLIER_LAST_NAME,
                ToolConstants.DB_SUPPLIER_FIRST_NAME,
                ToolConstants.DB_SUPPLIER_ADRESS_ID,
                ToolConstants.DB_SUPPLIER_BIRTHDAY,
                ToolConstants.DB_SUPPLIER_PHONE,
                ToolConstants.DB_SUPPLIER_COMPANY,
                ToolConstants.DB_SUPPLIER_EMAIL,
                ToolConstants.DB_SUPPLIER_TABLE);
            command.Parameters.Add("@0", System.Data.DbType.Int32).Value = p_SupplierId;

            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Supplier result = new Supplier();

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

        public static void DeleteSupplier(int p_SupplierId)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0} FROM {1} " +
                "WHERE {2} = @2",
                ToolConstants.DB_SUPPLIER_ADRESS_ID,
                ToolConstants.DB_SUPPLIER_TABLE,
                ToolConstants.DB_SUPPLIER_ID);
            command.Parameters.Add("@2", System.Data.DbType.Int32).Value = p_SupplierId;
            SQLiteDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                int adressId = reader.GetInt32(0);
                AdressMapper.DeleteAdress(adressId);
            }

            command.CommandText = string.Format(
                "DELETE FROM {0} " +
                "WHERE {1} = @1",
                ToolConstants.DB_SUPPLIER_TABLE,
                ToolConstants.DB_SUPPLIER_ID);

            command.Parameters.Add("@1", System.Data.DbType.Int32).Value = p_SupplierId;

            command.ExecuteNonQuery();
        }
    }
}
