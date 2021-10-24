using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;
using Pocky.MVVM.Model;
using Pocky.MVVM.View;
using System.Diagnostics;
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
        public ICommand DownloadButtonCommand { get; }

        public MainWindowViewModel(MainWindow window) {
            _parentWindow = window;
            _directory = new DirectoryHelper();

            OpenFolderCommand = new RelayCommand(() => Process.Start("explorer.exe", _directory.Path));
            ExitCommand = new RelayCommand(() => Application.Current.Shutdown());
            VersionInfoCommand = new RelayCommand(() => VersionInfoAsync());
            DownloadButtonCommand = new RelayCommand(() => Download());
        }

        private async void VersionInfoAsync() {
            await _parentWindow.ShowMessageAsync(
                    "Pocky", 
                    "Pocky version 1.0.0\n\n" +
                    "Copylight(C)2021 yurisi All rights reserved.\n" +
                    "本ソフトウェアはJellyParfaitの一部機能を削除したものです。\n\n" +
                    "本ソフトウェアはオープンソースソフトウェアです。\n" +
                    "GPL-3.0 Licenseに基づき誰でも複製や改変ができます。\n\n" +
                    "Github\n" +
                    "https://github.com/yurisi0212/Pocky"
                );
        }

        private void Download() {

        }
    }
}   