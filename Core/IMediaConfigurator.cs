using System;
using System.Collections.Generic;
using System.Text;

namespace MediaPlayer.Core
{
    public interface IMediaConfigurator
    {
        void SetVolume(double volume);
        void Mute();
        void Unmute();
    }
}
