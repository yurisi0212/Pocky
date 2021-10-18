using GalaSoft.MvvmLight.Command;
using Pocky.MVVM.View;
using System.Windows;
using System.Windows.Input;

namespace Pocky.MVVM.ViewModel {
    public class MainWindowViewModel : DependencyObject {

        private MainWindow _parentWindow;

        public static readonly DependencyProperty YoutubeURLTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(MainWindowViewModel), new UIPropertyMetadata(""));

        public string YoutubeURLText {
            get => (string)GetValue(YoutubeURLTextProperty);
            set => SetValue(YoutubeURLTextProperty, value);
        }

        private ICommand OpenFolderCommand { get; }
        private ICommand ExitCommand { get; }
        private ICommand VersionInfoCommand { get; }

        public MainWindowViewModel(MainWindow window) {
            _parentWindow = window;

            OpenFolderCommand = new RelayCommand(() => OpenFolder());
            ExitCommand = new RelayCommand(() => Application.Current.Shutdown());
            VersionInfoCommand = new RelayCommand(() => VersionInfo());
;       }

        private void OpenFolder() {

        }

        private void VersionInfo() {

        }
    }
}
