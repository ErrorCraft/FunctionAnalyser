using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UserInterface.ViewModels
{
    public class CrashWindowViewModel : INotifyPropertyChanged
    {
        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CrashReportFile { get; }

        public CrashWindowViewModel(Exception exception, string crashReportFile)
        {
            Description = $"{exception.GetType().FullName}:\n{exception.Message}";
            CrashReportFile = crashReportFile;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
