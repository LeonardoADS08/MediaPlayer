using MediaPlayer.Core.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace MediaPlayer.Core.Files
{
    public class File
    {
        public string Directory { get; private set; }
        public string Filename { get; private set; }
        public string Extension { get; private set; }

        public File(string directory)
        {
            Directory = directory;
            if (System.IO.File.Exists(directory))
            {
                Filename = Path.GetFileName(directory);
                Extension = Path.GetExtension(directory);
            }
            else
            {
                Logs.SaveLog(string.Format("File at directory not found {0}", directory), Logs.GetDirection());
                Filename = string.Empty;
                Extension = string.Empty;
            }
        }

    }
}
