using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.Helper
{
    public class ToolConstants
    {
        #region Tool
        public static string DB_PATH = Environment.CurrentDirectory;
        public const string DB_FILENAME = "FruitMarket.sqlite";
        #endregion

        #region Database

        #region Product
        public const string DB_PRODUCT_TABLE = "Product";
        public const string DB_PRODUCT_ID = "Id";
        public const string DB_PRODUCT_SUPPLIER_ID = "SupplierId";
        public const string DB_PRODUCT_SORT = "Sort";
        public const string DB_PRODUCT_DATA = "Data";

        public static string DB_PRODUCT = string.Format(
            "CREATE TABLE IF NOT EXISTS {0} (" +
            "{1} INTEGER NOT NULL PRIMARY KEY, " +
            "{2} INTEGER NOT NULL, " +
            "{3} TEXT, " +
            "{4} BLOB);", new string[]
            {
                DB_PRODUCT_TABLE,
                DB_PRODUCT_ID,
                DB_PRODUCT_SUPPLIER_ID,
                DB_PRODUCT_SORT,
                DB_PRODUCT_DATA
            });
        #endregion

        #region Supplier
        public const string DB_SUPPLIER_TABLE = "Supplier";
        public const string DB_SUPPLIER_ID = "Id";
        public const string DB_SUPPLIER_LAST_NAME = "LastName";
        public const string DB_SUPPLIER_FIRST_NAME = "FirstName";
        public const string DB_SUPPLIER_COMPANY = "Company";
        public const string DB_SUPPLIER_DATA = "Data";

        public static string DB_SUPPLIER = string.Format(
            "CREATE TABLE IF NOT EXISTS {0} (" +
            "{1} INTEGER NOT NULL PRIMARY KEY, " +
            "{2} TEXT, " +
            "{3} TEXT, " +
            "{4} TEXT, " +
            "{5} BLOB);", new string[]
            {
                DB_SUPPLIER_TABLE,
                DB_SUPPLIER_ID,
                DB_SUPPLIER_LAST_NAME,
                DB_SUPPLIER_FIRST_NAME,
                DB_SUPPLIER_COMPANY,
                DB_SUPPLIER_DATA
            });
        #endregion

        #region Producer
        public const string DB_PRODUCER_TABLE = "Producer";
        public const string DB_PRODUCER_ID = "Id";
        public const string DB_PRODUCER_LAST_NAME = "LastName";
        public const string DB_PRODUCER_FIRST_NAME = "FirstName";
        public const string DB_PRODUCER_COMPANY = "Company";
        public const string DB_PRODUCER_DATA = "Data";

        public static string DB_PRODUCER = string.Format(
            "CREATE TABLE IF NOT EXISTS {0} (" +
            "{1} INTEGER NOT NULL PRIMARY KEY, " +
            "{2} TEXT, " +
            "{3} TEXT, " +
            "{4} TEXT, " +
            "{5} BLOB);", new string[]
            {
                DB_PRODUCER_TABLE,
                DB_PRODUCER_ID,
                DB_PRODUCER_LAST_NAME,
                DB_PRODUCER_FIRST_NAME,
                DB_PRODUCER_COMPANY,
                DB_PRODUCER_DATA
            });
        #endregion

        #region Costumer
        public const string DB_COSTUMER_TABLE = "Costumer";
        public const string DB_COSTUMER_ID = "Id";
        public const string DB_COSTUMER_LAST_NAME = "LastName";
        public const string DB_COSTUMER_FIRST_NAME = "FirstName";
        public const string DB_COSTUMER_COMPANY = "Company";
        public const string DB_COSTUMER_DATA = "Data";

        public static string DB_COSTUMER = string.Format(
            "CREATE TABLE IF NOT EXISTS {0} (" +
            "{1} INTEGER NOT NULL PRIMARY KEY, " +
            "{2} TEXT, " +
            "{3} TEXT, " +
            "{4} TEXT, " +
            "{5} BLOB);", new string[]
            {
                DB_COSTUMER_TABLE,
                DB_COSTUMER_ID,
                DB_COSTUMER_LAST_NAME,
                DB_COSTUMER_FIRST_NAME,
                DB_COSTUMER_COMPANY,
                DB_COSTUMER_DATA
            });
        #endregion

        #region Delivery
        public const string DB_DELIVERY_TABLE = "Costumer";
        public const string DB_DELIVERY_ID = "Id";
        public const string DB_DELIVERY_PRODUCT_ID = "ProductId";
        public const string DB_DELIVERY_COSTUMER_ID = "CostumerId";
        public const string DB_DELIVERY_DATA = "Data";

        public static string DB_DELIVERY = string.Format(
            "CREATE TABLE IF NOT EXISTS {0} (" +
            "{1} INTEGER NOT NULL PRIMARY KEY, " +
            "{2} TEXT, " +
            "{3} TEXT, " +
            "{4} BLOB);", new string[]
            {
                DB_DELIVERY_TABLE,
                DB_DELIVERY_ID,
                DB_DELIVERY_PRODUCT_ID,
                DB_DELIVERY_COSTUMER_ID,
                DB_DELIVERY_DATA
            });
        #endregion

        #endregion
    }
}
