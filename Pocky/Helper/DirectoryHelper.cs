using System;
using System.IO;

namespace Pocky.Helper {
    public class DirectoryHelper {

        public string Path { get; }

        public DirectoryHelper() {
            Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\yurisi\Pocky\";
            Directory.CreateDirectory(Path);
        }
    }
}
