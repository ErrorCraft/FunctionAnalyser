using System;
using System.IO;

namespace UserInterface
{
    class Program
    {
        private const int PROGRAM_VERSION = 0;

        [STAThread]
        static void Main()
        {
            string basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/FunctionAnalyser";

            // Base directory
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
                // Get files from the web (all)
            } else
            {
                if (!File.Exists(basePath + "/version.json"))
                {
                    // Get files from the web (all)
                }
                else
                {
                    // Get version.json
                    // Get update information from the web
                    // if programVersion > PROGRAM_VERSION
                    //  Make user download new version
                    //  Close program
                    // if commandVersion > commandVersion from version.json
                    //  Get files from the web (all)
                }
            }

            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
