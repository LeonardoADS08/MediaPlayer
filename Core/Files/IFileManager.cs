using System;
using System.Collections.Generic;
using System.Text;

namespace MediaPlayer.Core.Files
{
    public interface IFileManager
    {
        List<File> GetFiles();
        List<File> GetFiles(string name);
        bool ValidExtension(File file);
        void TraverseFolder(string folderDirectory, Action<Folder> action);
    }
}
