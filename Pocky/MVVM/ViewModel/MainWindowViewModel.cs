using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;
using Pocky.MVVM.Model;
using Pocky.MVVM.View;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pocky.MVVM.ViewModel {
    public class MainWindowViewModel : DependencyObject {

        private MainWindow _parentWindow;

        private DirectoryHelper _directory;

        public static readonly DependencyProperty YoutubeURLTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(MainWindowViewModel), new UIPropertyMetadata(""));

        public string YoutubeURLText {
            get => (string)GetValue(YoutubeURLTextProperty);
            set => SetValue(YoutubeURLTextProperty, value);
        }

        public ICommand OpenFolderCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand VersionInfoCommand { get; }
        public ICommand DownLoadButtonCommand { get; }

        public MainWindowViewModel(MainWindow window) {
            _parentWindow = window;
            _directory = new DirectoryHelper();

            OpenFolderCommand = new RelayCommand(() => Process.Start("explorer.exe", _directory.Path));
            ExitCommand = new RelayCommand(() => Application.Current.Shutdown());
            VersionInfoCommand = new RelayCommand(() => VersionInfoAsync());
        }

        private async Task VersionInfoAsync() {
            await _parentWindow.ShowMessageAsync("Pocky", "Pocky version 1.0.0\n\nCopylight(C)2021 yurisi\nAll rights reserved.\n\n本ソフトウェアはオープンソースソフトウェアです。\nGPL-3.0 Licenseに基づき誰でも複製や改変ができます。\n\nGithub\nhttps://github.com/yurisi0212/Pocky");
        }
    }
}   