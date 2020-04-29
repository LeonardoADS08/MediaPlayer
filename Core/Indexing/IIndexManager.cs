using MediaPlayer.Core.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaPlayer.Core.Indexing
{
    public interface IIndexManager
    {
        Index IndexFile(File file);
        List<Index> GetIndexes();
        File GetFileByIndex(int index);
    }
}
