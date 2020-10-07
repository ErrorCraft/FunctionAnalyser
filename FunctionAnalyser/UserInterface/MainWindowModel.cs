using FunctionAnalyser;
using System;
using System.ComponentModel;

namespace UserInterface
{
    public class MainWindowModel
    {
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

        public static bool SkipFunctionOnError
        {
            get { return FunctionReader.Options.SkipFunctionOnError; }
            set
            {
                if (value != FunctionReader.Options.SkipFunctionOnError)
                {
                    FunctionReader.Options.SkipFunctionOnError = value;
                }
            }
        }

        public static bool ShowCommandErrors
        {
            get { return FunctionReader.Options.ShowCommandErrors; }
            set
            {
                if (value != FunctionReader.Options.ShowCommandErrors)
                {
                    FunctionReader.Options.ShowCommandErrors = value;
                }
            }
        }

        public static bool ShowEmptyFunctions
        {
            get { return FunctionReader.Options.ShowEmptyFunctions; }
            set
            {
                if (value != FunctionReader.Options.ShowEmptyFunctions)
                {
                    FunctionReader.Options.ShowEmptyFunctions = value;
                }
            }
        }

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
