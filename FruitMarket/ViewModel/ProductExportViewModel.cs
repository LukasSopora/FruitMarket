using FruitMarket.Helper;
using FruitMarket.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitMarket.ViewModel
{
    public class ProductExportViewModel : BindableBase
    {
        private ObservableCollection<Supplier> m_Suppliers =
            new ObservableCollection<Supplier>();
        private ObservableCollection<Costumer> m_Costumers =
            new ObservableCollection<Costumer>();
        private ObservableCollection<Product> m_Fruits =
            new ObservableCollection<Product>();
        private DateTime m_ExportDate = DateTime.Now;


        private Costumer m_CurrentCostumer = null;
        private Supplier m_CurrentSupplier = null;

        public DateTime ExportDate
        {
            get { return m_ExportDate; }
            set { SetProperty(ref m_ExportDate, value); }
        }

        public Costumer CurrentCostumer
        {
            get { return m_CurrentCostumer; }
            set { SetProperty(ref m_CurrentCostumer, value); }
        }

        public ObservableCollection<Costumer> Costumers
        {
            get { return m_Costumers; }
            set { SetProperty(ref m_Costumers, value); }
        }

        public ObservableCollection<Product> Fruits
        {
            get { return m_Fruits; }
            set { SetProperty(ref m_Fruits, value); }
        }

        public Supplier CurrentSupplier
        {
            get { return m_CurrentSupplier; }
            set { SetProperty(ref m_CurrentSupplier, value); }
        }

        public ObservableCollection<Supplier> Suppliers
        {
            get { return m_Suppliers; }
            set { SetProperty(ref m_Suppliers, value); }
        }

        public DelegateCommand NewSupplierCommand { get; private set; }
        public DelegateCommand SaveSupplierCommand { get; private set; }
        public DelegateCommand DeleteSupplierCommand { get; private set; }
        public DelegateCommand NewProducerCommand { get; private set; }
        public DelegateCommand SaveProducerCommand { get; private set; }
        public DelegateCommand DeleteProducerCommand { get; private set; }
        public DelegateCommand AddFruitsCommand { get; private set; }

        private void InitializeCommands()
        {
            NewSupplierCommand = new DelegateCommand(OnNewSupplier);
            RaisePropertyChanged(nameof(NewSupplierCommand));
            SaveSupplierCommand = new DelegateCommand(OnSaveSupplier);
            RaisePropertyChanged(nameof(SaveSupplierCommand));
            DeleteSupplierCommand = new DelegateCommand(OnDeleteSupplier);
            RaisePropertyChanged(nameof(DeleteSupplierCommand));
            NewProducerCommand = new DelegateCommand(OnNewCostumer);
            RaisePropertyChanged(nameof(NewProducerCommand));
            SaveProducerCommand = new DelegateCommand(OnSaveCostumer);
            RaisePropertyChanged(nameof(SaveProducerCommand));
            DeleteProducerCommand = new DelegateCommand(OnDeleteCostumer);
            RaisePropertyChanged(nameof(DeleteProducerCommand));
            AddFruitsCommand = new DelegateCommand(OnAddFruits);
            RaisePropertyChanged(nameof(AddFruitsCommand));
        }

        private void OnDeleteCostumer()
        {
            if (m_Costumers.Contains(m_CurrentCostumer))
            {
                if (System.Windows.MessageBox.Show(
                    string.Format("Möchten Sie den Kunden \"{0}\" löschen?", m_CurrentCostumer), "Kunden löschen?", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                {
                    m_Costumers.Remove(m_CurrentCostumer);
                    RaisePropertyChanged(nameof(Costumers));
                }
            }
            m_CurrentCostumer = null;
            RaisePropertyChanged(nameof(CurrentCostumer));
        }

        private void OnSaveCostumer()
        {
            if (CheckCostumer())
            {
                m_Costumers.Add(m_CurrentCostumer);
                m_CurrentCostumer = null;
                RaisePropertyChanged(nameof(CurrentCostumer));
            }
        }

        private void OnNewCostumer()
        {
            m_CurrentCostumer = new Costumer();
            m_CurrentCostumer.Editing = true;
            RaisePropertyChanged(nameof(CurrentCostumer));
        }

        private void OnAddFruits()
        {
            if (CheckSupplier() && CheckFruits())
            {
                //Hier gehts weiter
            }
        }

        private void OnDeleteSupplier()
        {
            if (m_Suppliers.Contains(m_CurrentSupplier))
            {
                if (System.Windows.MessageBox.Show(
                    string.Format("Möchten Sie den Lieferanten \"{0}\" löschen?", m_CurrentSupplier), "Delete Supplier?", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                {
                    m_Suppliers.Remove(m_CurrentSupplier);
                    RaisePropertyChanged(nameof(Suppliers));
                }
            }
            m_CurrentSupplier = null;
            RaisePropertyChanged(nameof(CurrentSupplier));
        }

        private void OnSaveSupplier()
        {
            if (CheckSupplier())
            {
                m_Suppliers.Add(m_CurrentSupplier);
                m_CurrentSupplier = null;
                RaisePropertyChanged(nameof(CurrentSupplier));
            }
        }

        private void OnNewSupplier()
        {
            m_CurrentSupplier = new Supplier();
            m_CurrentSupplier.Editing = true;
            RaisePropertyChanged(nameof(CurrentSupplier));
        }

        private bool CheckSupplier()
        {
            if (m_CurrentSupplier == null)
            {
                System.Windows.MessageBox.Show("Kein Lieferant.");
                return false;
            }
            if (m_CurrentSupplier.FirstName == null || m_CurrentSupplier.FirstName == "")
            {
                System.Windows.MessageBox.Show("Ungültiger Vorname.");
                return false;
            }
            if (m_CurrentSupplier.LastName == null || m_CurrentSupplier.LastName == "")
            {
                System.Windows.MessageBox.Show("Ungültiger Nachname.");
                return false;
            }
            if (m_CurrentSupplier.Adress == null ||
                m_CurrentSupplier.Adress.Street == null || m_CurrentSupplier.Adress.Street == "" ||
                m_CurrentSupplier.Adress.PostCode == null || m_CurrentSupplier.Adress.PostCode == "" ||
                m_CurrentSupplier.Adress.Place == null || m_CurrentSupplier.Adress.Place == "")
            {
                System.Windows.MessageBox.Show("Ungültige Adresse.");
                return false;
            }
            if (m_CurrentSupplier.Phone == null || m_CurrentSupplier.Phone == "")
            {
                System.Windows.MessageBox.Show("Ungültige Telefonnummer.");
                return false;
            }
            if (m_CurrentSupplier.Company == null || m_CurrentSupplier.Company == "")
            {
                System.Windows.MessageBox.Show("Ungültige Firma.");
                return false;
            }
            if (m_CurrentSupplier.Email == null || m_CurrentSupplier.Email == "")
            {
                System.Windows.MessageBox.Show("Ungültige Email.");
                return false;
            }
            if (m_CurrentSupplier.Birthday == null || m_CurrentSupplier.Birthday == DateTime.MinValue)
            {
                System.Windows.MessageBox.Show("Ungültiges Geburtsdatum.");
                return false;
            }
            return true;
        }

        private bool CheckCostumer()
        {
            if (m_CurrentCostumer == null)
            {
                System.Windows.MessageBox.Show("Kein Produzent.");
                return false;
            }
            if (m_CurrentCostumer.FirstName == null || m_CurrentCostumer.FirstName == "")
            {
                System.Windows.MessageBox.Show("Ungültiger Vorname.");
                return false;
            }
            if (m_CurrentCostumer.LastName == null || m_CurrentCostumer.LastName == "")
            {
                System.Windows.MessageBox.Show("Ungültiger Nachname.");
                return false;
            }
            if (m_CurrentCostumer.Adress == null ||
                m_CurrentCostumer.Adress.Street == null || m_CurrentCostumer.Adress.Street == "" ||
                m_CurrentCostumer.Adress.PostCode == null || m_CurrentCostumer.Adress.PostCode == "" ||
                m_CurrentCostumer.Adress.Place == null || m_CurrentCostumer.Adress.Place == "")
            {
                System.Windows.MessageBox.Show("Ungültige Adresse.");
                return false;
            }
            if (m_CurrentCostumer.Phone == null || m_CurrentCostumer.Phone == "")
            {
                System.Windows.MessageBox.Show("Ungültige Telefonnummer.");
                return false;
            }
            if (m_CurrentCostumer.Email == null || m_CurrentCostumer.Email == "")
            {
                System.Windows.MessageBox.Show("Ungültige Email.");
                return false;
            }
            if (m_CurrentCostumer.Birthday == null || m_CurrentCostumer.Birthday == DateTime.MinValue)
            {
                System.Windows.MessageBox.Show("Ungültiges Geburtsdatum.");
                return false;
            }
            return true;
        }

        private bool CheckFruits()
        {
            if (m_Fruits.Count == 0)
            {
                return false;
            }

            foreach (Product f in m_Fruits)
            {
                if (f.Sort == null || f.Sort == "")
                {
                    System.Windows.MessageBox.Show("Sort cannot be empty.");
                    return false;
                }
                if (f.Amount <= 0)
                {
                    System.Windows.MessageBox.Show("Invalid amount.");
                    return false;
                }
                if (f.Category == null || f.Category == "")
                {
                    System.Windows.MessageBox.Show("Amount cannot be empty.");
                    return false;
                }
                if (f.PurchaseDate == null || f.PurchaseDate == DateTime.MinValue)
                {
                    System.Windows.MessageBox.Show("Purchase Date cannot be empty.");
                    return false;
                }
                if (f.Expiration == null || f.Expiration == DateTime.MinValue)
                {
                    System.Windows.MessageBox.Show("Expiration Date cannot be empty.");
                    return false;
                }
                if (f.Mature == null || f.Mature == TimeSpan.MinValue)
                {
                    System.Windows.MessageBox.Show("Mature cannot be empty.");
                    return false;
                }
                if (f.Origin == null || f.Origin == "")
                {
                    System.Windows.MessageBox.Show("Origin cannot be empty.");
                    return false;
                }
                if (f.PurchasePrice <= 0)
                {
                    System.Windows.MessageBox.Show("Invalid price.");
                    return false;
                }
            }
            return true;
        }

        public ProductExportViewModel()
        {
            InitializeCommands();
            //m_Suppliers.Add(new Supplier("Lustig", "Peter", new Adress("Musterstrasse 12", "11111", "Musterhausen"), DateTime.Parse("1985-03-03"), "+49 666 666", "9Live", "peter@lustig.com"));
            //m_Suppliers.Add(new Supplier("Wurst", "Hans", new Adress("Zur Fielbecke 5", "57413",  "Finnentrop"), DateTime.Parse("1975-02-08"), "+49 234 234", "Metten", "hans@wurst.de"));
            //m_Suppliers.Add(new Supplier("Schnösel", "Godehardth", new Adress("Auf der Kö 69", "40210", "Düsseldorf"), DateTime.Parse("1969-06-09"), "+49 6969 6969", "Apple", "der-godehardt@kö.de"));

            //m_Producers.Add(new Producer("Lustig", "Peter", new Adress("Musterstrasse 12", "11111", "Musterhausen"), DateTime.Parse("1985-03-03"), "+49 666 666", "9Live", "peter@lustig.com"));
            //m_Producers.Add(new Producer("Wurst", "Hans", new Adress("Zur Fielbecke 5", "57413", "Finnentrop"), DateTime.Parse("1975-02-08"), "+49 234 234", "Metten", "hans@wurst.de"));
            //m_Producers.Add(new Producer("Schnösel", "Godehardth", new Adress("Auf der Kö 69", "40210", "Düsseldorf"), DateTime.Parse("1969-06-09"), "+49 6969 6969", "Apple", "der-godehardt@kö.de"));

            m_Suppliers = TestDataReader.GetDefaultSuppliers();
            m_Costumers = TestDataReader.GetDefaultCostumers();

            m_CurrentSupplier = new Supplier();
            m_CurrentCostumer = new Costumer();

            m_Fruits.Add(new Product());

            if (m_Suppliers.Count == 0)
            {
                OnNewSupplier();
            }
            if (m_Costumers.Count == 0)
            {
                //OnNewProducer();
            }
            RaisePropertyChanged(nameof(Fruits));
        }
    }
}
