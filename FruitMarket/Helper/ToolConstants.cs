using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public const string DB_PRODUCT_AMOUNT = "Amount";
        public const string DB_PRODUCT_SORT = "Sort";
        public const string DB_PRODUCT_ORIGIN = "Origin";
        public const string DB_PRODUCT_PRODUCER_ID = "ProducerId";
        public const string DB_PRODUCT_SUPPLIER_ID = "SupplierId";
        public const string DB_PRODUCT_DATA = "Data";

        public static string DB_PRODUCT = string.Format(
            "CREATE TABLE IF NOT EXISTS {0} (" +
            "{1} INTEGER PRIMARY KEY AUTOINCREMENT, " +
            "{2} INTEGER NOT NULL, " +
            "{3} TEXT references {4}({5}), " +
            "{6} TEXT references {7}({8}), " +
            "{9} INTEGER references {10}({11}), " +
            "{12} INTEGER references {13}({14}), " +
            "{15} BLOB);", new string[]
            {
                DB_PRODUCT_TABLE,        //0
                DB_PRODUCT_ID,           //1
                DB_PRODUCT_AMOUNT,       //2
                DB_PRODUCT_SORT,         //3
                DB_SORT_TABLE,           //4
                DB_SORT_SORTNAME,        //5
                DB_PRODUCT_ORIGIN,       //6
                DB_COUNTRY_TABLE,        //7
                DB_COUNTRY_COUNTRYNAME,  //8
                DB_PRODUCT_PRODUCER_ID,  //9
                DB_PRODUCER_TABLE,       //10
                DB_PRODUCER_ID,          //11
                DB_PRODUCT_SUPPLIER_ID,  //12
                DB_SUPPLIER_TABLE,       //13
                DB_SUPPLIER_ID,          //14
                DB_PRODUCT_DATA          //15
            });
        #endregion

        #region Adress
        public const string DB_ADRESS_TABLE = "Adressen";
        public const string DB_ADRESS_ID = "Id";
        public const string DB_ADRESS_STREET = "Street";
        public const string DB_ADRESS_POSTCODE = "PostCode";
        public const string DB_ADRESS_PLACE = "Place";

        public static string DB_ADRESS = string.Format(
            "CREATE TABLE IF NOT EXISTS {0} (" +
            "{1} INTEGER PRIMARY KEY AUTOINCREMENT, " +
            "{2} TEXT, " +
            "{3} TEXT, " +
            "{4} TEXT)", new string[]
            {
                DB_ADRESS_TABLE,
                DB_ADRESS_ID,
                DB_ADRESS_STREET,
                DB_ADRESS_POSTCODE,
                DB_ADRESS_PLACE
            });
        #endregion

        #region Supplier
        public const string DB_SUPPLIER_TABLE = "Supplier";
        public const string DB_SUPPLIER_ID = "Id";
        public const string DB_SUPPLIER_LAST_NAME = "LastName";
        public const string DB_SUPPLIER_FIRST_NAME = "FirstName";
        public const string DB_SUPPLIER_ADRESS_ID = "AdressId"; 
        public const string DB_SUPPLIER_COMPANY = "Company";
        public const string DB_SUPPLIER_BIRTHDAY = "Birthday"; 
        public const string DB_SUPPLIER_PHONE = "Phone"; 
        public const string DB_SUPPLIER_EMAIL = "EMail";

        public static string DB_SUPPLIER = string.Format(
            "CREATE TABLE IF NOT EXISTS {0} (" +
            "{1} INTEGER PRIMARY KEY AUTOINCREMENT, " +
            "{2} TEXT, " +
            "{3} TEXT, " +
            "{4} INTEGER references {5}({6}), " +
            "{7} TEXT, " +
            "{8} TEXT, " +
            "{9} TEXT, " +
            "{10} TEXT)", new string[]
            {
                DB_SUPPLIER_TABLE,
                DB_SUPPLIER_ID,
                DB_SUPPLIER_LAST_NAME,
                DB_SUPPLIER_FIRST_NAME,
                DB_SUPPLIER_ADRESS_ID,
                DB_ADRESS_TABLE,
                DB_ADRESS_ID,
                DB_SUPPLIER_COMPANY,
                DB_SUPPLIER_BIRTHDAY,
                DB_SUPPLIER_PHONE,
                DB_SUPPLIER_EMAIL
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
            "{1} INTEGER PRIMARY KEY AUTOINCREMENT, " +
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
            "{1} INTEGER PRIMARY KEY AUTOINCREMENT, " +
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
            "{1} INTEGER PRIMARY KEY AUTOINCREMENT, " +
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

        #region Sort
        public const string DB_SORT_TABLE = "FruitSort";
        public const string DB_SORT_SORTNAME = "SortName";

        public static string DB_SORT = string.Format(
            "CREATE TABLE IF NOT EXISTS {0} (" +
            "{1} TEXT PRIMARY KEY);", new string[]
            {
                DB_SORT_TABLE,
                DB_SORT_SORTNAME
            });
        #endregion

        #region Origin
        public const string DB_COUNTRY_TABLE = "Country";
        public const string DB_COUNTRY_COUNTRYNAME = "CountryName";

        public static string DB_COUNTRY = string.Format(
            "CREATE TABLE IF NOT EXISTS {0} (" +
            "{1} TEXT PRIMARY KEY);", new string[]
            {
                DB_COUNTRY_TABLE,
                DB_COUNTRY_COUNTRYNAME
            });
        #endregion

        #endregion

        #region PageDescription

        #region ProductImport
        public const string PRODUCT_IMPORT_CHOSE_SUPPLIER_PRODUCER_DESC =
            "Auswahl über den Produzenten und Lieferanten der eingehenden Produkte.\n" +
            "Produzenten/Lieferanten können über die entsprechenden Buttons\n" +
            "in der obigen Suchleiste hinzugefügt oder gelöscht werden.";

        public const string PRODUCT_IMPORT_CHOSE_INCOMING_PRODUCTS_DESC =
            "Angabe der eingehenden Produkte. Ist die Sorte eines Produkts noch nicht\n" +
            "in der Datenbank gespeichert, kann auch eine neue Sorte angegeben werden.\n" +
            "Diese wird dann in die Datenbank ünernommen.";

        public const string PRODUCT_IMPORT_CONFIRM_AND_SEND_DESC =
            "Hier erfolgt die Bestätigung der Einlagerung.\n" +
            "Wahlweise kann ein eingescannter Lieferschein hochgeladen, kein Lieferscheib angegeben\n" +
            "oder ein neuer automatisch erstellt werden.";
        #endregion

        #region ProductExport
        public const string PRODUCT_EXPORT_CHOSE_SUPPLIER_COSTUMER_DESC =
            "Auswahl über den Kunden und Lieferanten der ausgehenden Produkte.\n" +
            "Kunden/Lieferanten können über die entsprechenden Buttons\n" +
            "in der obigen Suchleiste hinzugefügt oder gelöscht werden.";

        public const string PRODUCT_EXPORT_CHOSE_OUTGOING_PRODUCTS_DESC =
            "Angabe der ausgehenden Produkte. Ist die Sorte eines Produkts noch nicht\n" +
            "in der Datenbank gespeichert, kann auch eine neue Sorte angegeben werden.\n" +
            "Diese wird dann in die Datenbank ünernommen.";

        public const string PRODUCT_EXPORT_CONFIRM_AND_SEND_DESC =
            "Hier erfolgt die Bestätigung der Auslagerung.\n" +
            "Es muss der passende Lieferschein ausgewählt werden.\n" +
            "Dann kann der Auftrag abgesehdet werden";
        #endregion

        #region FilterView

        public const string FILTER_VIEW_DESC =
            "Hier können die Produkte aus dem Lager gefiltert werden.\n" +
            "Es können neue Filter hinzugefügt werden, die dann in einer\n" +
            "Liste angezeigt werden, deaktiviert und wieder gelöscht werden können.";

        #endregion

        #region MainListView

        public const string MAIN_LIST_VIEW_DESC =
            "Hier sind alle Produkte aus dem Lager mit ihren Eigenschaften aufgelistet.\n";

        #endregion

        #region ProductListView
        public const string PRODUCT_LIST_VIEW_DESC =
            "Hier findet die Ausgabe sämtlicher Produkte aller Lieferanten statt (auch mit Bestand 0).\n" +
            "Die Liste kann in eine PDF-Datei exportiert werden.";
        #endregion

        #region DeliveryView
        public const string DELIVERY_VIEW_DESC =
            "Hier kann ein Lieferschein erstellt werden. Dazu muss ein Kunde und das Versandatum " +
            "ausgewählt werden. Anschließend können die entsprechenden Produkte ausgewählt werden. " +
            "Dabei wird die Positionssumme und die Gesamtsumme der Verkäufe immer berechnet und angezeigt.";
        #endregion

        #endregion

        #region Filter
        public static ObservableCollection<string> FILTER_CRITERIA = new ObservableCollection<string>
        {
            "Sorte",
            "Kategorie",
            "MHD",
            "Reifungsdauer",
            "Herkunft",
            "Einkaufspreis",
            "Verkaufspreis",
            "Bestand",
            "Lieferant",
            "Produzent",
            "Kunde",
            "Lieferschein"
        };
        #endregion

        #region DefaultVaules

        #region Fruits
        public static ObservableCollection<string> DEFAULT_FRUITS = new ObservableCollection<string>
        {
            "Apfel",
            "Banane",
            "Birne",
            "Kirsche",
            "Ananas",
            "Orange",
            "Aubergine",
            "Pfirsich",
            "Erdbeere",
            "Heidelbeere"
        };

        public static ObservableCollection<string> DEFAULT_ORIGINS = new ObservableCollection<string>
        {
            "Deutschland",
            "Spanien",
            "Griechenland",
            "Brasilien",
            "Norwegen",
            "Italien",
            "Portugal",
            "Schottland",
            "Österreich",
            "Belgien",
        };

        public static ObservableCollection<string> DEFAULT_FRUIT_CATEGORIES = new ObservableCollection<string>
        {
            "Kl 1",
            "Kl 2",
            "Kl 3"
        };
        #endregion

        #endregion
    }
}
