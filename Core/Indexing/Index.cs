using MediaPlayer.Core.Files;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using LiteDB;
using System.Numerics;

namespace MediaPlayer.Core.Indexing
{
    public class Index
    {
        [BsonId]
        public int IndexID { get; set; }
        [BsonField]
        public string Directory { get; set; }
        public File File { get; set; }

        public Index()
        {
        }

        public Index(File file)
        {
            File = file;
            Directory = file.Directory;
        }
    }
}
