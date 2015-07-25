using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FontUtil
{
    /// <summary>
    /// Interaction logic for Preview.xaml
    /// </summary>
    public partial class Preview : UserControl
    {
        public Preview()
        {
            InitializeComponent();
        }

        private void Rb_Click(object sender, RoutedEventArgs e)
        {
            string source = ((RadioButton)sender).Name;
            switch (source)
            {
                case "RbEnglish":
                    InputText.Text = "The quick brown fox jumps over the lazy dog";
                    break;
                case "RbHun":
                    InputText.Text = "Árvíztűrő tükörfúrógép";
                    break;
            }
        }

        public static DependencyProperty PreviewFontFamilyProperty = DependencyProperty.Register("PreviewFontFamily", typeof(FontFamily), typeof(Preview));

        public FontFamily PreviewFontFamily
        {
            get { return (FontFamily)GetValue(PreviewFontFamilyProperty); }
            set { SetValue(PreviewFontFamilyProperty, value); }
        }
    }
}
