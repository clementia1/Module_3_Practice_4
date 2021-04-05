using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Module_3_Practice_4.Services.Abstractions;

namespace Module_3_Practice_4.Services
{
    public class DirectoryService : IDirectoryService
    {
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public void Create(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void CreateIfNotExists(string path)
        {
            if (!Exists(path))
            {
                Create(path);
            }
        }
    }
}
