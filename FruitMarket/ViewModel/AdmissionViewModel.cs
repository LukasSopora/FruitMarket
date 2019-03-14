﻿using FruitMarket.Model;
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
        private bool m_IsEditing = false;

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

        private void InitializeCommands()
        {
            NewSupplierCommand = new DelegateCommand(OnNewSupplier);
            RaisePropertyChanged(nameof(NewSupplierCommand));
            SaveSupplierCommand = new DelegateCommand(OnSaveSupplier);
            RaisePropertyChanged(nameof(SaveSupplierCommand));
            DeleteSupplierCommand = new DelegateCommand(OnDeleteSupplier);
            RaisePropertyChanged(nameof(DeleteSupplierCommand));
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

        public AdmissionViewModel()
        {
            InitializeCommands();
            m_Suppliers.Add(new Supplier("Peter"));
            m_Suppliers.Add(new Supplier("Hans"));
            m_Suppliers.Add(new Supplier("Dieter"));
            m_Suppliers.Add(new Supplier("Paul"));

            if (m_Suppliers.Count == 0)
            {
                OnNewSupplier();
            }
        }
    }
}
