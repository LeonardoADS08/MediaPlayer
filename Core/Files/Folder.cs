using MediaPlayer.Core.Extensions.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaPlayer.Core.Files
{
    public class Folder
    {
        public Folder Parent { get; private set; }
        public string Directory { get; private set; }
        public List<File> Files = new List<File>();
        public List<Folder> Folders = new List<Folder>();

        public Folder(string directory)
        {
            if (!System.IO.Path.IsPathRooted(directory)) Directory = System.IO.Path.GetFullPath(directory);
            else Directory = directory;
            Parent = null;
            AnalyzeDirectory(Directory);
        }

        public Folder(string directory, Folder parent)
        {
            Directory = directory;
            Parent = parent;
            AnalyzeDirectory(directory);
        }

        private void AnalyzeDirectory(string directory)
        {
            if (System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.EnumerateFiles(Directory)
                                   .ForEach(directory => Files.Add(new File(directory)));

                System.IO.Directory.GetDirectories(directory)
                                   .ForEach(directory => Folders.Add(new Folder(directory, this)));
            }
        }

        public List<File> FindFiles() => FindFiles(this, new List<File>());

        private List<File> FindFiles(Folder folder, List<File> files)
        {
            files.AddRange(folder.Files);
            folder.Folders.ForEach(folderDirectory => FindFiles(folderDirectory, files));
            return files;
        }

        public override string ToString() => Directory;
    }
}
