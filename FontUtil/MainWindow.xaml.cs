using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Collections.Generic;

namespace FontUtil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        ObservableCollection<string> _fontsToInstall;
        ObservableCollection<string> _fontsToPreview;

        public MainWindow()
        {
            InitializeComponent();
            _fontsToInstall = new ObservableCollection<string>();
            _fontsToPreview = new ObservableCollection<string>();
            LbItems.ItemsSource = _fontsToInstall;
            LbPreview.ItemsSource = _fontsToPreview;
        }

        private void BtnAddFonts_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "Font Files|*.ttf;*.otf";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var item in ofd.FileNames)
                {
                    _fontsToInstall.Add(item);
                }
            }
        }

        private void BtnClearList_Click(object sender, RoutedEventArgs e)
        {
            var q = MessageBox.Show("Clear List", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (q == MessageBoxResult.Yes)
            {
                _fontsToInstall.Clear();
            }
        }

        private async void BtnInstall_Click(object sender, RoutedEventArgs e)
        {
            var q = MessageBox.Show("Install fonts?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (q == MessageBoxResult.Yes)
            {
                await FontInstaller.InstallFontsTask(_fontsToInstall.ToArray());
            }
        }

        private void BtnOpenDir_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fb = new System.Windows.Forms.FolderBrowserDialog();
            fb.Description = "Select folder to view";
            fb.RootFolder = System.Environment.SpecialFolder.Desktop;
            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _fontsToPreview.Clear();
                List<string> files = new List<string>();
                files.AddRange(Directory.GetFiles(fb.SelectedPath, "*.ttf"));
                files.AddRange(Directory.GetFiles(fb.SelectedPath, "*.otf"));
                files.Sort();
                foreach (var f in files)
                {
                    _fontsToPreview.Add(f);
                }
            }
        }

        private void BtnOpenWebSite_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/webmaster442/FontUtil");
            
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Font Util by Webmaster442\nThis program is free software\nUses Icons by Icons8.com", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
