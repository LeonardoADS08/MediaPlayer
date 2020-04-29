using MediaPlayer.Core.Files;
using MediaPlayer.Core.Indexing;
using MediaPlayer.Core.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unosquare.FFME;

namespace MediaPlayer.Core
{
    public class Player : IMediaPlayer, IMediaFileManager, IMediaConfigurator
    {
        private MediaElement _media;

        public Player(MediaElement media)
        {
            _media = media;
        }

        public void Mute() => _media.IsMuted = true;
        public void Unmute() => _media.IsMuted = false;
        public void SetVolume(double volume) => _media.Volume = Math.Clamp(volume, 0.0d, 1.0d);

        public async Task<bool> Pause() => await _media.Pause();

        public async Task<bool> Play(File file) => await _media.Open(new Uri(file.Directory));

        public async Task<bool> Play() => await _media.Play();

        public async Task<bool> Replay()
        {
            await _media.Pause();
            _media.Position = TimeSpan.FromSeconds(0);
            return await Play();
        }

        public File GetFile(int index) => IndexManager.Instance.GetFileByIndex(index);

        public List<File> GetFiles(string name) => FileManager.Instance.GetFiles(name);

        public Core.Indexing.Index GetIndex(File file) => IndexManager.Instance.IndexFile(file);
    }
}
