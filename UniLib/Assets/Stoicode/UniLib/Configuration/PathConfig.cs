using System.Collections.Generic;
using System.IO;

namespace Stoicode.UniLib.Configuration
{
    public class PathConfig
    {
        public Dictionary<string, string> Directories { get; protected set; }
        public Dictionary<string, string> Files { get; protected set; }


        public PathConfig()
        {
            Directories = new Dictionary<string, string>();
            Files = new Dictionary<string, string>();
        }

        public PathConfig(Dictionary<string, string> directories, Dictionary<string, string> files)
        {
            Directories = directories;

            foreach (var p in directories)
                Directory.CreateDirectory(p.Value);

            Files = files;
        }
    }
}