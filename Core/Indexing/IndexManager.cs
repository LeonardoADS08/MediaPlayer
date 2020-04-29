using MediaPlayer.Core.Extensions.Linq;
using MediaPlayer.Core.Files;
using MediaPlayer.Core.Tools;
using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MediaPlayer.Core.Indexing
{
    [Serializable]
    public class IndexManager : IIndexManager
    {
        #region Singleton
        private static IIndexManager _instance;
        public static IIndexManager Instance
        {
            get
            {
                if (_instance == null) _instance = new IndexManager();
                return _instance;
            }
        }
        #endregion

        public const string COLLECTION_INDEXES = "Indexes";
        private List<Index> _indexes = new List<Index>();
        private Dictionary<int, File> _indexedFiles = new Dictionary<int, File>();

        public IndexManager()
        {
            _indexes = GetIndexes();
            List<File> files = FileManager.Instance.GetFiles();

            _indexes.ForEach(index =>
            {
                var file = files.Find(file => file.Directory == index.Directory);
                if (file != null) index.File = file;
            });

            files.Where(file =>
                !_indexes.Exists(index =>
                    index.Directory == file.Directory
                )
            ).ForEach(file => _indexes.Add(IndexFile(file)));

            _indexes.ForEach(index => _indexedFiles.Add(index.IndexID, index.File));
        }

        public Index IndexFile(File file)
        {
            using (var db = new LiteDatabase(Configurations.Instance.DBDirectory))
            {
                var collection = db.GetCollection<Index>(COLLECTION_INDEXES);
                var index = collection.FindOne(index => index.Directory == file.Directory);
                if (index == null)
                {
                    Index newIndex = new Index(file);
                    collection.Insert(newIndex);
                    return newIndex;
                }
                else return index;
            }
        }

        public List<Index> GetIndexes()
        {
            List<Index> result = new List<Index>();
            using (var db = new LiteDatabase(Configurations.Instance.DBDirectory))
            {
                var collection = db.GetCollection<Index>(COLLECTION_INDEXES);
                result = collection.FindAll()
                                   .ToList();
                result.ForEach(index => index.File = new File(index.Directory));
            }
            return result;
        }

        public File GetFileByIndex(int index) => _indexedFiles.ContainsKey(index) ? _indexedFiles[index] : null;

    }
}
