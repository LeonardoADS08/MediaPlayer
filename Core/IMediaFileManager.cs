using MediaPlayer.Core.Files;
using MediaPlayer.Core.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaPlayer.Core
{
    public interface IMediaFileManager
    {
        File GetFile(int index);
        List<File> GetFiles(string name);
        Core.Indexing.Index GetIndex(File file);
    }
}
