using FruitMarket.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FruitMarket.View
{
    /// <summary>
    /// Interaction logic for SupplierView.xaml
    /// </summary>
    public partial class SupplierView : UserControl
    {
        public SupplierView()
        {
            InitializeComponent();
        }

        private void View_Mouse_Enter(object sender, MouseEventArgs e)
        {
            UserIcon.Foreground = FindResource("PrimaryHueMidBrush") as Brush;
            SupplierViewBorder.BorderBrush = FindResource("PrimaryHueMidBrush") as Brush;
        }

        private void View_Mouse_Leave(object sender, MouseEventArgs e)
        {
            UserIcon.Foreground = Brushes.White;
            SupplierViewBorder.BorderBrush = Brushes.White;
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.BorderBrush = FindResource("PrimaryHueMidBrush") as Brush;
        }

        private void TextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.BorderBrush = FindResource("MaterialDesignBodyLight") as Brush;
        }

        private void DatePickerBox_MouseEnter(object sender, MouseEventArgs e)
        {
            var datePicker = sender as DatePicker;
            datePicker.BorderBrush = FindResource("PrimaryHueMidBrush") as Brush;
        }

        private void DatePickerBox_MouseLeave(object sender, MouseEventArgs e)
        {
            var datePicker = sender as DatePicker;
            datePicker.BorderBrush = FindResource("MaterialDesignBodyLight") as Brush;
        }
    }
}
