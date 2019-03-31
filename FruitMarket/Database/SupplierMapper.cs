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
    class SupplierMapper
    {
        public static void SaveSuppliers(ObservableCollection<Supplier> p_Suppliers)
        {
            foreach (Supplier supplier in p_Suppliers)
            {
                SaveSupplier(supplier);
            }
        }

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

            if (alreadyExists)
            {
                command.CommandText = string.Format(
                    "UPDATE {0} SET " +
                    "{1} = @1, " +
                    "{2} = @2, " +
                    "{3} = @3, " +
                    "{4} = @4 " +
                    "WHERE {5} = @5",
                    ToolConstants.DB_SUPPLIER_TABLE,
                    ToolConstants.DB_SUPPLIER_LAST_NAME,
                    ToolConstants.DB_SUPPLIER_FIRST_NAME,
                    ToolConstants.DB_SUPPLIER_COMPANY,
                    ToolConstants.DB_SUPPLIER_DATA,
                    ToolConstants.DB_SUPPLIER_ID
                    );

                MemoryStream memoryStream = new MemoryStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, p_Supplier);

                byte[] supplierData = memoryStream.ToArray();
                memoryStream.Close();

                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Supplier.LastName;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Supplier.FirstName;
                command.Parameters.Add("@3", System.Data.DbType.String).Value = p_Supplier.Company;
                command.Parameters.Add("@4", System.Data.DbType.Binary, supplierData.Length).Value = supplierData;
                command.Parameters.Add("@5", System.Data.DbType.Int32).Value = p_Supplier.Id;

                command.ExecuteNonQuery();
            }
            else
            {
                command.CommandText = string.Format(
                "INSERT INTO {0} ({1}, {2}, {3}, {4}) " +
                "VALUES (@1, @2, @3, @4)",
                ToolConstants.DB_SUPPLIER_TABLE,
                ToolConstants.DB_SUPPLIER_LAST_NAME,
                ToolConstants.DB_SUPPLIER_FIRST_NAME,
                ToolConstants.DB_SUPPLIER_COMPANY,
                ToolConstants.DB_SUPPLIER_DATA);

                MemoryStream memoryStream = new MemoryStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, p_Supplier);

                byte[] supplierData = memoryStream.ToArray();
                memoryStream.Close();

                command.Parameters.Add("@1", System.Data.DbType.String).Value = p_Supplier.LastName;
                command.Parameters.Add("@2", System.Data.DbType.String).Value = p_Supplier.FirstName;
                command.Parameters.Add("@3", System.Data.DbType.String).Value = p_Supplier.Company;
                command.Parameters.Add("@4", System.Data.DbType.Binary, supplierData.Length).Value = supplierData;

                command.ExecuteNonQuery();
            }
        }

        public static ObservableCollection<Supplier> GetAllSuppliers()
        {
            ObservableCollection<Supplier> result = new ObservableCollection<Supplier>();

            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

            command.CommandText = string.Format(
                "SELECT {0}, {1}, {2}, {3}, {4} FROM {5}",
                ToolConstants.DB_SUPPLIER_ID,
                ToolConstants.DB_SUPPLIER_LAST_NAME,
                ToolConstants.DB_SUPPLIER_FIRST_NAME,
                ToolConstants.DB_SUPPLIER_COMPANY,
                ToolConstants.DB_SUPPLIER_DATA,
                ToolConstants.DB_SUPPLIER_TABLE);

            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                MemoryStream memoryStream = new MemoryStream((byte[])reader.GetValue(4));
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                Supplier supplier = (Supplier)binaryFormatter.Deserialize(memoryStream);

                supplier.Id = Convert.ToInt32(reader.GetValue(0));
                supplier.LastName = Convert.ToString(reader.GetValue(1));
                supplier.FirstName = Convert.ToString(reader.GetValue(2));
                supplier.Company = Convert.ToString(reader.GetValue(3));

                result.Add(supplier);
            }
            return result;
        }

        public static void DeleteSupplier(int p_SupplierId)
        {
            SQLiteConnection con = Connection.GetConnection();
            SQLiteCommand command = new SQLiteCommand(con);

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
