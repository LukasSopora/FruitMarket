using FruitMarket.Database;
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
        private ObservableCollection<Costumer> m_Costumers =
            new ObservableCollection<Costumer>();
        private ObservableCollection<Product> m_Products =
            new ObservableCollection<Product>();
        private ObservableCollection<string> m_Sorts =
            new ObservableCollection<string>();
        private ObservableCollection<string> m_Categories =
            new ObservableCollection<string>();
        private ObservableCollection<string> m_Origins =
            new ObservableCollection<string>();

        private DateTime m_ExportDate = DateTime.Now;
        private Costumer m_CurrentCostumer = null;
        private string m_NewSort = null;
        private int m_PageIndex = 0;
        private bool m_NewCostumerVisible = false;
        private string m_CostumerHeader = "Kunde";


        public bool NewCostumerVisible
        {
            get { return m_NewCostumerVisible; }
            set { SetProperty(ref m_NewCostumerVisible, value); }
        }

        public string CostumerHeader
        {
            get { return m_CostumerHeader; }
            set { SetProperty(ref m_CostumerHeader, value); }
        }

        public ObservableCollection<string> Origins
        {
            get { return m_Origins; }
            set { SetProperty(ref m_Origins, value); }
        }

        public ObservableCollection<string> Categories
        {
            get { return m_Categories; }
            set { SetProperty(ref m_Categories, value); }
        }

        public string PageDescription
        {
            get
            {
                switch (m_PageIndex)
                {
                    case 0: return ToolConstants.PRODUCT_EXPORT_CHOSE_SUPPLIER_COSTUMER_DESC;
                    case 1: return ToolConstants.PRODUCT_EXPORT_CHOSE_OUTGOING_PRODUCTS_DESC;
                    case 2: return ToolConstants.PRODUCT_EXPORT_CONFIRM_AND_SEND_DESC;
                    default: return "";
                }
            }
        }

        public int PageIndex
        {
            get { return m_PageIndex; }
            set
            {
                SetProperty(ref m_PageIndex, value);
                RaisePropertyChanged(nameof(PageDescription));
            }
        }

        public string NewSort
        {
            get { return m_NewSort; }
            set { SetProperty(ref m_NewSort, value); }
        }

        public ObservableCollection<string> Sorts
        {
            get { return m_Sorts; }
            set { SetProperty(ref m_Sorts, value); }
        }

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

        public ObservableCollection<Product> Products
        {
            get { return m_Products; }
            set { SetProperty(ref m_Products, value); }
        }

        public DelegateCommand NewCostumerCommand { get; private set; }
        public DelegateCommand SaveCostumerCommand { get; private set; }
        public DelegateCommand DeleteCostumerCommand { get; private set; }
        public DelegateCommand CancelNewCostumerCommand { get; private set; }

        public DelegateCommand AddProductCommand { get; private set; }
        public DelegateCommand ExportProductsCommand { get; private set; }

        private void InitializeCommands()
        {
            NewCostumerCommand = new DelegateCommand(OnNewCostumer);
            RaisePropertyChanged(nameof(NewCostumerCommand));
            SaveCostumerCommand = new DelegateCommand(OnSaveCostumer);
            RaisePropertyChanged(nameof(SaveCostumerCommand));
            DeleteCostumerCommand = new DelegateCommand(OnDeleteCostumer);
            RaisePropertyChanged(nameof(DeleteCostumerCommand));
            CancelNewCostumerCommand = new DelegateCommand(OnCancelNewCostumer);
            RaisePropertyChanged(nameof(CancelNewCostumerCommand));

            AddProductCommand = new DelegateCommand(OnAddProduct);
            RaisePropertyChanged(nameof(AddProductCommand));
            ExportProductsCommand = new DelegateCommand(OnExportProducts);
            RaisePropertyChanged(nameof(ExportProductsCommand));
        }


        #region Costumer Methods
        private void OnNewCostumer()
        {
            m_CurrentCostumer = new Costumer();
            RaisePropertyChanged(nameof(CurrentCostumer));

            m_CostumerHeader = "Neuen Kunden anlegen";
            RaisePropertyChanged(nameof(CostumerHeader));

            m_NewCostumerVisible = true;
            RaisePropertyChanged(nameof(NewCostumerVisible));
        }

        private void OnCancelNewCostumer()
        {
            m_CurrentCostumer = null;
            RaisePropertyChanged(nameof(CurrentCostumer));

            m_CostumerHeader = "Kunde";
            RaisePropertyChanged(nameof(CostumerHeader));

            m_NewCostumerVisible = false;
            RaisePropertyChanged(nameof(NewCostumerVisible));
        }

        private void OnSaveCostumer()
        {
            if (CheckCostumer())
            {
                if(!m_Costumers.Contains(m_CurrentCostumer))
                {
                    m_Costumers.Add(m_CurrentCostumer);
                    CostumerMapper.SaveCostumer(m_CurrentCostumer);

                    RaisePropertyChanged(nameof(Costumers));
                }
                m_CurrentCostumer = null;
                RaisePropertyChanged(nameof(CurrentCostumer));

                m_CostumerHeader = "Kunde";
                RaisePropertyChanged(nameof(CostumerHeader));

                m_NewCostumerVisible = false;
                RaisePropertyChanged(nameof(NewCostumerVisible));
            }
        }

        private void OnDeleteCostumer()
        {
            if (m_Costumers.Contains(m_CurrentCostumer))
            {
                if (System.Windows.MessageBox.Show(
                    string.Format("Möchten Sie den Produzenten \"{0}\" löschen?", m_CurrentCostumer), "Delete Costumer?", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                {
                    m_Costumers.Remove(m_CurrentCostumer);
                    RaisePropertyChanged(nameof(Costumers));

                    CostumerMapper.DeleteCostumer(m_CurrentCostumer.Id);

                    m_CurrentCostumer = null;
                    RaisePropertyChanged(nameof(CurrentCostumer));
                }
            }
        }
        #endregion


        private void OnAddProduct()
        {
            m_Products.Add(new Product());
            RaisePropertyChanged(nameof(Products));
        }

        private void OnExportProducts()
        {
            if (CheckCostumer() && CheckProducts())
            {
                //Hier gehts weiter
            }
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
            if (m_CurrentCostumer.Birthday == null)
            {
                System.Windows.MessageBox.Show("Ungültiges Geburtsdatum.");
                return false;
            }
            return true;
        }

        private bool CheckProducts()
        {
            if (m_Products.Count == 0)
            {
                return false;
            }

            foreach (Product f in m_Products)
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
                if (f.PurchaseDate == null)
                {
                    System.Windows.MessageBox.Show("Purchase Date cannot be empty.");
                    return false;
                }
                if (f.Expiration == null)
                {
                    System.Windows.MessageBox.Show("Expiration Date cannot be empty.");
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
            m_Sorts = SortMapper.GetAllSorts();
            m_Categories = ToolConstants.DEFAULT_FRUIT_CATEGORIES;
            m_Costumers = CostumerMapper.GetAllCostumers();
            m_Products = ProductMapper.GetAllProducts();

            InitializeCommands();

            RaisePropertyChanged(nameof(Products));
            RaisePropertyChanged(nameof(Sorts));
            RaisePropertyChanged(nameof(Categories));
            RaisePropertyChanged(nameof(Origins));
        }
    }
}
