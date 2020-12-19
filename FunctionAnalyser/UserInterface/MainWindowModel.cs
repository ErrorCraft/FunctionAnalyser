﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace UserInterface
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        public string SkipFunctionOnErrorTooltip { get; } = "Skips the function if it contains a command error.\nIt will not contribute to the information found.";
        private bool _SkipFunctionOnErrorEnabled = false;
        private bool _SkipFunctionOnError = false;
        public bool SkipFunctionOnErrorEnabled
        {
            get { return _SkipFunctionOnErrorEnabled; }
            set
            {
                if (_SkipFunctionOnErrorEnabled != value)
                {
                    _SkipFunctionOnErrorEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool SkipFunctionOnError
        {
            get { return _SkipFunctionOnError; }
            set
            {
                if (_SkipFunctionOnError != value)
                {
                    _SkipFunctionOnError = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ShowCommandErrorsTooltip { get; } = "Shows command errors if they are found.\nA function may contain multiple errors.";
        private bool _ShowCommandErrorsEnabled = false;
        private bool _ShowCommandErrors = true;
        public bool ShowCommandErrorsEnabled
        {
            get { return _ShowCommandErrorsEnabled; }
            set
            {
                if (_ShowCommandErrorsEnabled != value)
                {
                    _ShowCommandErrorsEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool ShowCommandErrors
        {
            get { return _ShowCommandErrors; }
            set
            {
                if (_ShowCommandErrors != value)
                {
                    _ShowCommandErrors = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ShowEmptyFunctionsTooltip { get; } = "Shows empty functions if they are found.\nA function is empty if it does not contain any commands.";
        private bool _ShowEmptyFunctionsEnabled = false;
        private bool _ShowEmptyFunctions = true;
        public bool ShowEmptyFunctionsEnabled
        {
            get { return _ShowEmptyFunctionsEnabled; }
            set
            {
                if (_ShowEmptyFunctionsEnabled != value)
                {
                    _ShowEmptyFunctionsEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool ShowEmptyFunctions
        {
            get { return _ShowEmptyFunctions; }
            set
            {
                if (_ShowEmptyFunctions != value)
                {
                    _ShowEmptyFunctions = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _AnalyseEnabled = false;
        public bool AnalyseEnabled
        {
            get { return _AnalyseEnabled; }
            set
            {
                if (_AnalyseEnabled != value)
                {
                    _AnalyseEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _SelectFolderEnabled = false;
        public bool SelectFolderEnabled
        {
            get { return _SelectFolderEnabled; }
            set
            {
                if (_SelectFolderEnabled != value)
                {
                    _SelectFolderEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _ExportDataEnabled = false;
        public bool ExportDataEnabled
        {
            get { return _ExportDataEnabled; }
            set
            {
                if (_ExportDataEnabled != value)
                {
                    _ExportDataEnabled = value;
                    OnPropertyChanged();
                }
            }
        }


        private bool _VersionsEnabled = false;
        private List<CommandVersionViewModel> _Versions;
        private int _VersionsSelectedIndex = 0;
        public bool VersionsEnabled
        {
            get { return _VersionsEnabled; }
            set
            {
                if (_VersionsEnabled != value)
                {
                    _VersionsEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<CommandVersionViewModel> Versions
        {
            get { return _Versions; }
            set
            {
                if (_Versions != value)
                {
                    _Versions = value;
                    VersionsSelectedIndex = 0;
                    OnPropertyChanged();
                }
            }
        }
        public int VersionsSelectedIndex
        {
            get { return _VersionsSelectedIndex; }
            set
            {
                if (_VersionsSelectedIndex != value)
                {
                    _VersionsSelectedIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<TextBlock> _Blocks = new ObservableCollection<TextBlock>();
        public ObservableCollection<TextBlock> Blocks
        {
            get { return _Blocks; }
            set
            {
                if (_Blocks != value)
                {
                    _Blocks = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FolderPath { get; set; }

        public void DisableOptions()
        {
            SkipFunctionOnErrorEnabled = false;
            ShowCommandErrorsEnabled = false;
            ShowEmptyFunctionsEnabled = false;
            VersionsEnabled = false;
        }

        public void EnableOptions()
        {
            SkipFunctionOnErrorEnabled = true;
            ShowCommandErrorsEnabled = true;
            ShowEmptyFunctionsEnabled = true;
            VersionsEnabled = true;
        }

        public void DisableButtons()
        {
            AnalyseEnabled = false;
            SelectFolderEnabled = false;
            ExportDataEnabled = false;
        }

        public void EnableButtons()
        {
            AnalyseEnabled = true;
            SelectFolderEnabled = true;
            ExportDataEnabled = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
