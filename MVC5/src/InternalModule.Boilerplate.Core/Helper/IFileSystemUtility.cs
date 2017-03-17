using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Helper
{
    public interface IFileSystemUtility
    {
        string GenerateDirectoryPathWithCurrentTime();
        string GenerateFilenameWithCurrentTime(string extension = "");
        string RandomString(int length);
        bool EnsureDirectory(string directory);
        void DeleteBlankDirectory(string rootPath);
        void DeleteBlankDirectoryWithInactiveAccess(string rootPath, int inactiveThreshold);
        void DeleteBlankDirectoryWithInactiveWrite(string rootPath, int inactiveWriteThreshold);
        bool MoveFile(string sourcePath, string destinationPath, bool replaceIfDestinationExists = false);
        bool IsFileLocked(FileInfo file);
    }
}
