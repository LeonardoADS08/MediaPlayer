using MediaPlayer.Core.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaPlayer.Core.Media
{
    public class Playlist
    {
        public int PlaylistID { get; set; }
        public string Name { get; set; }
        public List<File> Files { get; set; }
    }
}
