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
    public class AdmissionViewModel : BindableBase
    {
        private ObservableCollection<Supplier> m_Suppliers =
            new ObservableCollection<Supplier>();
        private ObservableCollection<Fruit> m_Fruits =
            new ObservableCollection<Fruit>();


        private Supplier m_CurrentSupplier = null;
        private bool m_IsEditing = false;

        public ObservableCollection<Fruit> Fruits
        {
            get { return m_Fruits; }
            set { m_Fruits = value; }
        }
        public bool IsEditing
        {
            get { return m_IsEditing; }
            set { m_IsEditing = value; }
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
        public DelegateCommand AddFruitsCommand { get; private set; }

        private void InitializeCommands()
        {
            NewSupplierCommand = new DelegateCommand(OnNewSupplier);
            RaisePropertyChanged(nameof(NewSupplierCommand));
            SaveSupplierCommand = new DelegateCommand(OnSaveSupplier);
            RaisePropertyChanged(nameof(SaveSupplierCommand));
            DeleteSupplierCommand = new DelegateCommand(OnDeleteSupplier);
            RaisePropertyChanged(nameof(DeleteSupplierCommand));
            AddFruitsCommand = new DelegateCommand(OnAddFruits);
            RaisePropertyChanged(nameof(AddFruitsCommand));
        }

        private void OnAddFruits()
        {
            if(CheckSupplier() && CheckFruits())
            {
                //Hier gehts weiter
            }
        }

        private void OnDeleteSupplier()
        {
            if(m_Suppliers.Contains(m_CurrentSupplier))
            {
                if(System.Windows.MessageBox.Show(
                    string.Format("Doy you want to Delete Supplier \"{0}\"?", m_CurrentSupplier), "Delete Supplier?",System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                {
                    m_Suppliers.Remove(m_CurrentSupplier);
                    RaisePropertyChanged(nameof(Suppliers));
                }
            }
            m_CurrentSupplier = null;
            RaisePropertyChanged(nameof(CurrentSupplier));

            m_IsEditing = false;
            RaisePropertyChanged(nameof(IsEditing));
        }

        private void OnSaveSupplier()
        {
            if(CheckSupplier())
            {
                m_Suppliers.Add(m_CurrentSupplier);
                RaisePropertyChanged(nameof(CurrentSupplier));

                m_IsEditing = false;
                RaisePropertyChanged(nameof(IsEditing));
            }
        }

        private void OnNewSupplier()
        {
            m_CurrentSupplier = new Supplier();
            RaisePropertyChanged(nameof(CurrentSupplier));


            m_IsEditing = true;
            RaisePropertyChanged(nameof(IsEditing));
        }

        private bool CheckSupplier()
        {
            if(m_CurrentSupplier.FirstName == null || m_CurrentSupplier.FirstName == "")
            {
                System.Windows.MessageBox.Show("First Name cannot be empty.");
                return false;
            }
            if(m_CurrentSupplier.LastName == null || m_CurrentSupplier.FirstName == "")
            {
                System.Windows.MessageBox.Show("Las tName cannot be empty.");
                return false;
            }
            if (m_CurrentSupplier.Adress == null || m_CurrentSupplier.FirstName == "")
            {
                System.Windows.MessageBox.Show("Adress cannot be empty.");
                return false;
            }
            if (m_CurrentSupplier.Phone == null || m_CurrentSupplier.FirstName == "")
            {
                System.Windows.MessageBox.Show("Phone cannot be empty.");
                return false;
            }
            if (m_CurrentSupplier.Company == null || m_CurrentSupplier.FirstName == "")
            {
                System.Windows.MessageBox.Show("Company cannot be empty.");
                return false;
            }

            return true;
        }

        private bool CheckFruits()
        {
            if(m_Fruits.Count == 0)
            {
                return false;
            }
            
            foreach(Fruit f in m_Fruits)
            {
                if (f.Sort == null || f.Sort == "")
                {
                    System.Windows.MessageBox.Show("Sort cannot be empty.");
                    return false;
                }
                if(f.Amount <= 0)
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
                if(f.Origin == null || f.Origin == "")
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

        public AdmissionViewModel()
        {
            InitializeCommands();
            m_Suppliers.Add(new Supplier("Lustig", "Peter", "Musterstrasse 12, 11111 Musterhausen", DateTime.Parse("1985-03-03"), "+49 666 666", "9Live"));
            m_Suppliers.Add(new Supplier("Wurst", "Hans", "Zur Fielbecke 5, 57413 Finnentrop", DateTime.Parse("1975-02-08"), "+49 234 234", "Metten"));
            m_Suppliers.Add(new Supplier("Schnösel", "Godehardth", "Auf der Kö 69, 40210 Düsseldorf", DateTime.Parse("1969-06-09"), "+49 6969 6969", "Apple"));

            if (m_Suppliers.Count == 0)
            {
                OnNewSupplier();
            }
            RaisePropertyChanged(nameof(Fruits));
            RaisePropertyChanged(nameof(IsEditing));
        }
    }
}
