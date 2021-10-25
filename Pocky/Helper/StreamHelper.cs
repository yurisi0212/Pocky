using Pocky.MVVM.Model;
using System;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos.Streams;

namespace Pocky.Helper {
    public class StreamHelper {

        public static async Task<Audio> GetAudio(string url) {
            var youtubeClient = new YoutubeClient();
            var video = await youtubeClient.Videos.GetAsync(url);
            var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(video.Id);
            var streamUrl = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate().Url;
            return new Audio { Path = streamUrl, Title = video.Title, Url = video.Url };
        }


        public static async Task<string> getHighestBitrateUrl(YoutubeExplode.Videos.VideoId id) {
            var youtubeClient = new YoutubeClient();
            var streamManifest = await youtubeClient.Videos.Streams.GetManifestAsync(id);
            return streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate().Url;
        }

        public static async Task<YoutubeType> GetType(string url) {
            var youtube = new YoutubeClient();
            YoutubeType value = YoutubeType.NotFound;
            try {
                var playlist = await youtube.Playlists.GetAsync(url);
                youtube.Playlists.GetVideosAsync(playlist.Id);
                value = YoutubeType.Playlist;
            } catch (System.Exception e) {
                if (e is ArgumentException || e is YoutubeExplode.Exceptions.PlaylistUnavailableException) {
                    try {
                        var youtubeClient = new YoutubeClient();
                        var video = await youtubeClient.Videos.GetAsync(url);
                        value = YoutubeType.Video;
                    } catch (ArgumentException) {
                        value = YoutubeType.NotFound;
                    }
                }
            }
            return value;
        }

        public static PlaylistClient GetPlaylistClient() {
            return new YoutubeClient().Playlists;
        }

        public static async Task<Playlist> GetPlaylists(string url) {
            var youtubeClient = new YoutubeClient();
            return await youtubeClient.Playlists.GetAsync(url);
        }
    }
}
