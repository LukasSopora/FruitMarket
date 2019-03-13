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
        private Supplier m_CurrentSupplier = null;

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
        public DelegateCommand CancelCommand { get; private set; }

        private void InitializeCommands()
        {
            NewSupplierCommand = new DelegateCommand(OnNewSupplier);
            RaisePropertyChanged(nameof(NewSupplierCommand));
            SaveSupplierCommand = new DelegateCommand(OnSaveSupplier);
            RaisePropertyChanged(nameof(SaveSupplierCommand));
            CancelCommand = new DelegateCommand(OnCancel);
            RaisePropertyChanged(nameof(CancelCommand));
        }

        private void OnCancel()
        {
            m_CurrentSupplier = null;
            RaisePropertyChanged(nameof(m_CurrentSupplier));
        }

        private void OnSaveSupplier()
        {
            if(CheckSupplier())
            {
                m_Suppliers.Add(m_CurrentSupplier);
                RaisePropertyChanged(nameof(Suppliers));
            }
        }

        private void OnNewSupplier()
        {
            m_CurrentSupplier = new Supplier();
            RaisePropertyChanged(nameof(CurrentSupplier));
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

        public AdmissionViewModel()
        {
            InitializeCommands();
            m_Suppliers.Add(new Supplier("Peter"));
            m_Suppliers.Add(new Supplier("Hans"));
            m_Suppliers.Add(new Supplier("Dieter"));
            m_Suppliers.Add(new Supplier("Paul"));
        }
    }
}
