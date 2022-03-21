﻿using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;
using Pocky.Helper;
using Pocky.MVVM.Model;
using Pocky.MVVM.View;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using YoutubeExplode;
using YoutubeExplode.Converter;

namespace Pocky.MVVM.ViewModel {
    public class MainWindowViewModel : DependencyObject {

        private MainWindow _parentWindow;

        private ProgressDialogController _dialog;

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

        private async void Download() {
            var type = StreamHelper.GetType(YoutubeURLText);
            Debug.Print(type.ToString());
            _dialog = await _parentWindow.ShowProgressAsync("Pocky", "ダウンロード中...", true);
            switch (type.Result) {
                case YoutubeType.Video:
                    await SingleVideoDownloadAsync(YoutubeURLText);
                    _dialog.SetProgress(1);
                    break;
                case YoutubeType.Playlist:
                    await PlaylistDownloadAsync();
                    break;
                case YoutubeType.NotFound:
                    await ShowErrorMessageAsync();
                    break;             
            }
            await _dialog.CloseAsync();
        }

        private async Task SingleVideoDownloadAsync(string url) {
            var youtubeClient = new YoutubeClient();
            var video = await youtubeClient.Videos.GetAsync(url);
            var music = _directory.Path + video.Title + ".mp3";
            if (File.Exists(music)) File.Delete(music);
            await youtubeClient.Videos.DownloadAsync(video.Id, music);
        }

        private async Task PlaylistDownloadAsync() {
            var youtube = new YoutubeClient();
            var playlist = await youtube.Playlists.GetAsync(YoutubeURLText);
            var videos = youtube.Playlists.GetVideosAsync(playlist.Id);
            var playlistcount = 0;
            await foreach (var video in videos) {
                playlistcount += 1;
            }
            _dialog.SetMessage("ダウンロード中...(1/"+(playlistcount + 1)+")");
            var count = 0;
            await foreach (var video in videos) {
                count += 1;
                await SingleVideoDownloadAsync(video.Url);
                _dialog.SetProgress((float)count / (float)playlistcount);
                _dialog.SetMessage("ダウンロード中...("+(count + 1)+"/" + (playlistcount + 1) + ")");
            }
        }

        private async Task ShowErrorMessageAsync() {
            MessageBox.Show("無効なURLです。", "Pocky", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}   