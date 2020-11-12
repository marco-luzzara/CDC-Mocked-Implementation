using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CDCImplementation.Infrastructure.FileSystem
{
    public class DirectoryUtils
    {
        public static int DeleteFilesByExt(string baseDirectory, string ext, bool recursive = true)
        {
            string[] files = Directory.GetFiles(baseDirectory, $"*.{ext}", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                File.Delete(file);
            }

            return files.Length;
        }

        public static int RenameFilesByExt(string baseDirectory, string oldExt, string newExt, bool recursive = true)
        {
            string[] files = Directory.GetFiles(baseDirectory, $"*.{oldExt}", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                var newFile = $"{Path.GetFileNameWithoutExtension(file)}.{newExt}";
                var newPath = Path.Join(Path.GetDirectoryName(file), newFile);
                File.Move(file, newPath, true);
            }

            return files.Length;
        }
    }
}
