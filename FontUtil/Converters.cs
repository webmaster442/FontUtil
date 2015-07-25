using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace FontUtil
{
    public class FileNameConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            string input = System.Convert.ToString(value);
            return System.IO.Path.GetFileName(input);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class FontFamilyConverter: IValueConverter
    {
        private static string GetFontNameFromFile(string filename)
        {
            PrivateFontCollection fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile(filename);
            return fontCollection.Families[0].Name;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            string input = System.Convert.ToString(value);
            string name = GetFontNameFromFile(input);
            var uri = new System.Uri(input);
            var ret = new FontFamily(new Uri(uri.AbsoluteUri), name);
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
