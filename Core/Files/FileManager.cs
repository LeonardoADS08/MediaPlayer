using MediaPlayer.Core.Extensions.Linq;
using MediaPlayer.Core.Indexing;
using MediaPlayer.Core.Tools;
using F23.StringSimilarity;
using F23.StringSimilarity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPlayer.Core.Files
{
    public class FileManager : IFileManager
    {
        #region Singleton
        private static IFileManager _instance;
        public static IFileManager Instance
        {
            get
            {
                if (_instance == null) _instance = new FileManager();
                return _instance;
            }
        }

        #endregion

        private const double MIN_SEARCH_BOUND = 0.625d;

        private List<File> _files = new List<File>();
        private Dictionary<string, File> _indexedFiles = new Dictionary<string, File>();

        public bool ValidExtension(File file) => Configurations.Instance.AllowedExtensions.Contains(file.Extension);

        public FileManager()
        {
            Configurations.Instance.Folders.ForEach(directory =>
            {
                TraverseFolder(directory, folder =>
                {
                    _files.AddRange(folder.Files.Where(file => ValidExtension(file)));
                });
            });

            _files.ForEach(file => _indexedFiles.Add(file.Directory, file));
        }

        public List<File> GetFiles() => _files;

        public List<File> GetFiles(string name)
        {
            name = name.ToLower();
            IStringSimilarity stringSimilarity = new F23.StringSimilarity.JaroWinkler();
            List<Tuple<File, double>> results = new List<Tuple<File, double>>();
            _files.ForEach(file => results.Add(new Tuple<File, double>(file, stringSimilarity.Similarity(file.Filename.ToLower(), name))));
            return results.Where(result => result.Item2 > MIN_SEARCH_BOUND).Select(result => result.Item1).ToList();
        }

        public void TraverseFolder(string folderDirectory, Action<Folder> action) => TraverseFolder(new Folder(folderDirectory), action);

        private void TraverseFolder(Folder folder, Action<Folder> action)
        {
            action(folder);
            folder.Folders.ForEach(nextFolder => TraverseFolder(nextFolder, action));
        }
    }
}
