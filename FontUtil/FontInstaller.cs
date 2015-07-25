using System;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FontUtil
{
    internal static class FontInstaller
    {
        public static void InstallFonts(params string[] fontfiles)
        {
            try
            {
                string temppath = Path.GetTempPath();
                string targetdir = Path.Combine(temppath, "fontinstaller");

                string installerscript = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FontInstaller.vbs");

                if (Directory.Exists(targetdir)) Directory.Delete(targetdir, true);
                if (!Directory.Exists(targetdir)) Directory.CreateDirectory(targetdir);

                foreach (var font in fontfiles)
                {
                    File.Copy(font, Path.Combine(targetdir, Path.GetFileName(font)));
                }

                Process p = new Process();
                p.StartInfo.FileName = "wscript.exe";
                p.StartInfo.Verb = "runas";
                p.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\"", installerscript, targetdir);
                p.Start();

                Thread.Sleep(100);
                Process process = null;

                try
                {
                    do
                    {
                        process = Process.GetProcessById(p.Id);
                    }
                    while (process != null);
                }
                catch (Exception) { }

                Directory.Delete(targetdir, true);

                MessageBox.Show("Fonts Installed succesfully", "Font Installer", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static Task InstallFontsTask(params string[] fontfiles)
        {
            return Task.Run(() => InstallFonts(fontfiles));
        }
    }
}
