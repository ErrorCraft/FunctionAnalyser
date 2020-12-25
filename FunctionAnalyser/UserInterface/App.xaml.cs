using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace UserInterface
{
    public partial class App : Application
    {
        private void Crash(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            DateTime time = DateTime.Now;
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            Exception exception = e.Exception;

            string basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string file = $"FunctionAnalyser-Crash-{time:yyyy-MM-dd_hh.mm.ss}.txt";
            string path = Path.Combine(basePath, file);

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine($"Version: {version}");
                writer.WriteLine($"Time: {time:yyyy-MM-dd hh:mm:ss}");
                writer.WriteLine($"Description: {exception.Message}");
                writer.WriteLine();
                writer.WriteLine(exception.GetType().FullName);
                writer.WriteLine(exception.StackTrace);
            }

            e.Handled = true;
            CrashWindow crashWindow = new CrashWindow(exception, path);
            crashWindow.ShowDialog();
            MainWindow.Close();

            Shutdown(-1);
        }
    }
}
