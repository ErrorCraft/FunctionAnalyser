using FunctionAnalyser;
using System;
using System.ComponentModel;

namespace UserInterface
{
    public class MainWindowModel
    {
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

        private static bool _EnableOptions = true;
        public static bool EnableOptions
        {
            get { return _EnableOptions; }
            set
            {
                _EnableOptions = value;
                StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(EnableOptions)));
            }
        }

        public static string SkipFunctionOnErrorTooltip { get; } = "Skips the function if it contains a command error.\nIt will not contribute to the information found.";
        public static string ShowCommandErrorsTooltip { get; } = "Shows command errors if they are found.\nA function may contain multiple errors.";
        public static string ShowEmptyFunctionsTooltip { get; } = "Shows empty functions if they are found.\nA function is empty if it does not contain any commands.";
    }
}
