using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Model
{
    public class FileInf
    {
        private string _path;

        /// <summary>
        /// File path.
        /// </summary>
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                FileName = System.IO.Path.GetFileName(value);
            }
        }
        /// <summary>
        /// File name.
        /// </summary>
        public string FileName { get; private set; }
        /// <summary>
        /// RenameOrFileName.
        /// </summary>
        public string RenameOrFileName { get; private set; }

        /// <summary>
        /// Creates a new instance of FileInf.
        /// </summary>
        /// <param name="path">File path.</param>
        public FileInf(string path)
        {
            Path = path;
            RenameOrFileName = FileName;
        }

        /// <summary>
        /// Creates a new instance of FileInf.
        /// </summary>
        /// <param name="path">File path.</param>
        /// <param name="reName">New file name.</param>
        public FileInf(string path, string reName)
        {
            Path = path;
            RenameOrFileName = reName;
        }
    }
}
