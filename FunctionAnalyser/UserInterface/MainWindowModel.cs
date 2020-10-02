using FunctionAnalyser;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserInterface
{
    public class MainWindowModel
    {
        public bool SkipFunctionOnError
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

        public bool ShowCommandErrors
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

        public bool ShowEmptyFunctions
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
    }
}
