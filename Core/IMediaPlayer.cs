using MediaPlayer.Core.Files;
using MediaPlayer.Core.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.Core
{
    public interface IMediaPlayer
    {
        Task<bool> Play(File file);
        Task<bool> Replay();
        Task<bool> Pause();
        

    }
}
