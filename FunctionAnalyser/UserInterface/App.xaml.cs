using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Crash(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            string basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
            string file = "FunctionAnalyser-Crash-" + DateTime.Now.ToString("yyyy-MM-dd_hh.mm.ss") + ".txt";

            using (StreamWriter writer = new StreamWriter(basePath + file))
            {
                writer.WriteLine($"Time: {DateTime.Now:yyyy-MM-dd hh:mm:ss}");
                writer.WriteLine($"Description: {e.Exception.Message}");
                writer.WriteLine();
                writer.WriteLine(e.Exception.GetType().FullName);
                writer.WriteLine(e.Exception.StackTrace);
            }

            e.Handled = true;

            CrashWindow crashWindow = new CrashWindow(e.Exception, basePath + file);
            crashWindow.ShowDialog();
            MainWindow.Close();

            Shutdown(-1);
        }
    }
}
