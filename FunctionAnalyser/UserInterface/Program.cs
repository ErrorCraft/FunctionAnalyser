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
            // Get version.json
            // if version::program_version > PROGRAM_VERSION
            //  Make user download new version
            //  Close program
            // else
            //  Get files from the web (all)

            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
