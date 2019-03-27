using FruitMarket.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FruitMarket.Converter
{
    public class EmptyCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.GetType() == typeof(ObservableCollection<Filter>))
            {
                ObservableCollection<Filter> filters = value as ObservableCollection<Filter>;
                if(filters.Count == 0)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
