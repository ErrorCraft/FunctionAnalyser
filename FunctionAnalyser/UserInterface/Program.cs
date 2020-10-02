using CommandVerifier;
using CommandVerifier.Commands.Collections;
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
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
