﻿using FruitMarket.Database;
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
    public class ProductImportViewModel : BindableBase
    {
        private ObservableCollection<Supplier> m_Suppliers =
            new ObservableCollection<Supplier>();
        private ObservableCollection<Producer> m_Producers =
            new ObservableCollection<Producer>();
        private ObservableCollection<Product> m_Products =
            new ObservableCollection<Product>();
        private ObservableCollection<string> m_Sorts =
            new ObservableCollection<string>();
        private ObservableCollection<string> m_Categories =
            new ObservableCollection<string>();
        private ObservableCollection<string> m_Origins =
            new ObservableCollection<string>();

        private DateTime m_ImportDate = DateTime.Now;
        private Producer m_CurrentProducer = null;
        private Supplier m_CurrentSupplier = null;
        private string m_NewSort = null;
        private int m_PageIndex = 0;

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
                switch(m_PageIndex)
                {
                    case 0: return ToolConstants.PRODUCT_IMPORT_CHOSE_SUPPLIER_PRODUCER_DESC;
                    case 1: return ToolConstants.PRODUCT_IMPORT_CHOSE_INCOMING_PRODUCTS_DESC;
                    case 2: return ToolConstants.PRODUCT_IMPORT_CONFIRM_AND_SEND_DESC;
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

        public DateTime ImportDate
        {
            get { return m_ImportDate; }
            set { SetProperty(ref m_ImportDate, value); }
        }

        public Producer CurrentProducer
        {
            get { return m_CurrentProducer; }
            set { SetProperty(ref m_CurrentProducer, value); }
        }

        public ObservableCollection<Producer> Producers
        {
            get { return m_Producers; }
            set { SetProperty(ref m_Producers, value); }
        }

        public ObservableCollection<Product> Products
        {
            get { return m_Products; }
            set { SetProperty(ref m_Products, value); }
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
        public DelegateCommand AddProductCommand { get; private set; }
        public DelegateCommand ShowHelpCommand { get; private set; }
        public DelegateCommand SaveProductsCommand { get; private set; }
        public DelegateCommand AddNewSortCommand { get; private set; }

        private void InitializeCommands()
        {
            NewSupplierCommand = new DelegateCommand(OnNewSupplier);
            RaisePropertyChanged(nameof(NewSupplierCommand));
            SaveSupplierCommand = new DelegateCommand(OnSaveSupplier);
            RaisePropertyChanged(nameof(SaveSupplierCommand));
            DeleteSupplierCommand = new DelegateCommand(OnDeleteSupplier);
            RaisePropertyChanged(nameof(DeleteSupplierCommand));
            NewProducerCommand = new DelegateCommand(OnNewProducer);
            RaisePropertyChanged(nameof(NewProducerCommand));
            SaveProducerCommand = new DelegateCommand(OnSaveProducer);
            RaisePropertyChanged(nameof(SaveProducerCommand));
            DeleteProducerCommand = new DelegateCommand(OnDeleteProducer);
            RaisePropertyChanged(nameof(DeleteProducerCommand));
            AddProductCommand = new DelegateCommand(OnAddProduct);
            RaisePropertyChanged(nameof(AddProductCommand));
            SaveProductsCommand = new DelegateCommand(OnSaveProducts);
            RaisePropertyChanged(nameof(SaveProductsCommand));
            AddNewSortCommand = new DelegateCommand(OnAddNewSort);
            RaisePropertyChanged(nameof(AddNewSortCommand));
        }

        private void OnAddProduct()
        {
            m_Products.Add(new Product());
            RaisePropertyChanged(nameof(Products));
        }

        private void OnAddNewSort()
        {
            if(m_NewSort == null || m_NewSort == "")
            {
                return;
            }
            if(!m_Sorts.Contains(m_NewSort))
            {
                m_Sorts.Add(m_NewSort);
            }
            RaisePropertyChanged(nameof(Sorts));
        }

        private void OnDeleteProducer()
        {
            if (m_Producers.Contains(m_CurrentProducer))
            {
                if (System.Windows.MessageBox.Show(
                    string.Format("Möchten Sie den Produzenten \"{0}\" löschen?", m_CurrentProducer), "Delete Producer?", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                {
                    m_Producers.Remove(m_CurrentProducer);
                    RaisePropertyChanged(nameof(Producers));
                }
            }
            m_CurrentProducer = null;
            RaisePropertyChanged(nameof(CurrentProducer));
        }

        private void OnSaveProducer()
        {
            if (CheckProducer())
            {
                m_Producers.Add(m_CurrentProducer);
                m_CurrentProducer = null;
                RaisePropertyChanged(nameof(CurrentProducer));
            }
        }

        private void OnNewProducer()
        {
            m_CurrentProducer = new Producer();
            m_CurrentProducer.Editing = true;
            RaisePropertyChanged(nameof(CurrentProducer));
        }

        private void OnSaveProducts()
        {
            if (CheckSupplier() && CheckFruits())
            {
                foreach(Product p in m_Products)
                {
                    p.ProducerId = m_CurrentProducer.Id;
                    p.SupplierId = m_CurrentSupplier.Id;
                }
                ProductMapper.SaveProducts(m_Products);
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

        private bool CheckProducer()
        {
            if (m_CurrentProducer == null)
            {
                System.Windows.MessageBox.Show("Kein Produzent.");
                return false;
            }
            if (m_CurrentProducer.FirstName == null || m_CurrentProducer.FirstName == "")
            {
                System.Windows.MessageBox.Show("Ungültiger Vorname.");
                return false;
            }
            if (m_CurrentProducer.LastName == null || m_CurrentProducer.LastName == "")
            {
                System.Windows.MessageBox.Show("Ungültiger Nachname.");
                return false;
            }
            if (m_CurrentProducer.Adress == null ||
                m_CurrentProducer.Adress.Street == null || m_CurrentProducer.Adress.Street == "" ||
                m_CurrentProducer.Adress.PostCode == null || m_CurrentProducer.Adress.PostCode == "" ||
                m_CurrentProducer.Adress.Place == null || m_CurrentProducer.Adress.Place == "")
            {
                System.Windows.MessageBox.Show("Ungültige Adresse.");
                return false;
            }
            if (m_CurrentProducer.Phone == null || m_CurrentProducer.Phone == "")
            {
                System.Windows.MessageBox.Show("Ungültige Telefonnummer.");
                return false;
            }
            if (m_CurrentProducer.Company == null || m_CurrentProducer.Company == "")
            {
                System.Windows.MessageBox.Show("Ungültige Firma.");
                return false;
            }
            if (m_CurrentProducer.Email == null || m_CurrentProducer.Email == "")
            {
                System.Windows.MessageBox.Show("Ungültige Email.");
                return false;
            }
            if (m_CurrentProducer.Birthday == null || m_CurrentProducer.Birthday == DateTime.MinValue)
            {
                System.Windows.MessageBox.Show("Ungültiges Geburtsdatum.");
                return false;
            }
            return true;
        }

        private bool CheckFruits()
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
                if (f.Mature == null || f.Mature.Days == 0 && f.Mature.Hours.Hour == 0)
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

        public ProductImportViewModel()
        {
            InitializeCommands();
            m_Suppliers = TestDataReader.GetDefaultSuppliers();
            m_Producers = ProducerMapper.GetAllProduers();
            m_Sorts = ToolConstants.DEFAULT_FRUITS;
            m_Categories = ToolConstants.DEFAULT_FRUIT_CATEGORIES;
            m_Origins = ToolConstants.DEFAULT_ORIGINS;

            m_CurrentSupplier = new Supplier();
            m_CurrentProducer = new Producer();

            m_Products = ProductMapper.GetAllProducts();


            if (m_Suppliers.Count == 0)
            {
                OnNewSupplier();
            }
            if (m_Producers.Count == 0)
            {
                OnNewProducer();
            }

            RaisePropertyChanged(nameof(Products));
            RaisePropertyChanged(nameof(Sorts));
            RaisePropertyChanged(nameof(Categories));
            RaisePropertyChanged(nameof(Origins));
        }
    }
}
