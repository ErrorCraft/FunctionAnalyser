using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UserInterface.ViewModels
{
    public class UpdateWindowViewModel : INotifyPropertyChanged
    {
        private string _Version;
        public string Version
        {
            get { return _Version; }
            set
            {
                if (_Version != value)
                {
                    _Version = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _Changelog;
        public string Changelog
        {
            get { return _Changelog; }
            set
            {
                if (_Changelog != value)
                {
                    _Changelog = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
