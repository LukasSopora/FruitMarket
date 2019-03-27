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
