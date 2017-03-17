using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Helper
{
    public class FileSystemUtility : IFileSystemUtility
    {
        private Random _random = new Random();
        private string _charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public string GenerateDirectoryPathWithCurrentTime()
        {
            var dateTime = DateTime.UtcNow;
            return dateTime.Year.ToString("0000") + "\\" + dateTime.Month.ToString("00") + "\\" + dateTime.Day.ToString("00") + "\\" + dateTime.Hour.ToString("00") + dateTime.Minute.ToString("00");
        }

        public string GenerateFilenameWithCurrentTime(string extension = "")
        {
            var dateTime = DateTime.UtcNow;
            if (extension.Trim() == "")
                return dateTime.Year.ToString("0000") + dateTime.Month.ToString("00") + dateTime.Day.ToString("00") + dateTime.Hour.ToString("00") + dateTime.Minute.ToString("00") + dateTime.Second.ToString("00") + "_" + RandomString(4);
            else
                return dateTime.Year.ToString("0000") + dateTime.Month.ToString("00") + dateTime.Day.ToString("00") + dateTime.Hour.ToString("00") + dateTime.Minute.ToString("00") + dateTime.Second.ToString("00") + "_" + RandomString(4) + "." + extension;
        }

        public string RandomString(int length)
        {
            string output = "";

            for (int i = 0; i < length; i++)
                output += _charSet[_random.Next(_charSet.Length)];

            return output;
        }

        /// <summary>
        /// Ensure specified directory existance. Function will be created specified folder in case the folder is not exists
        /// </summary>
        /// <param name="directory">Directory path to be ensured</param>
        /// <returns></returns>
        public bool EnsureDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Recursive delete empty directory under rootPath
        /// </summary>
        /// <param name="rootPath">Root path of directory that's required to be cleaned up</param>
        public void DeleteBlankDirectory(string rootPath)
        {
            foreach (var directory in Directory.GetDirectories(rootPath))
            {
                DeleteBlankDirectory(directory);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                    Directory.Delete(directory, false);
            }
        }

        /// <summary>
        /// Recursive delete empty inactive directory under rootPath with inactive condition
        /// </summary>
        /// <param name="rootPath">Root path of directory that's required to be cleaned up</param>
        /// <param name="inactiveThreshold">Number of second threshold to classify active/inactive directory. 
        /// e.g. If this parameter set with 10 it means this function will only deleted the folder in the case that
        /// the folder is blanked and the latest access timestamp on this folder happened more than 10 seconds ago.</param>
        public void DeleteBlankDirectoryWithInactiveAccess(string rootPath, int inactiveThreshold = 0)
        {
            DateTime now = DateTime.UtcNow;

            foreach (var directory in Directory.GetDirectories(rootPath))
            {
                DeleteBlankDirectoryWithInactiveAccess(directory, inactiveThreshold);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                    if (directoryInfo.LastAccessTimeUtc < now.AddSeconds(-inactiveThreshold))
                        Directory.Delete(directory, false);
                }
            }
        }

        /// <summary>
        /// Recursive delete empty inactive directory under rootPath with inactive condition
        /// </summary>
        /// <param name="rootPath">Root path of directory that's required to be cleaned up</param>
        /// <param name="inactiveWriteThreshold">Number of second threshold to classify active/inactive directory. 
        /// e.g. If this parameter set with 10 it means this function will only deleted the folder in the case that
        /// the folder is blanked and the latest write operation on this folder happened more than 10 seconds ago.</param>
        public void DeleteBlankDirectoryWithInactiveWrite(string rootPath, int inactiveWriteThreshold)
        {
            DateTime now = DateTime.UtcNow;

            foreach (var directory in Directory.GetDirectories(rootPath))
            {
                DeleteBlankDirectoryWithInactiveAccess(directory, inactiveWriteThreshold);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                    if (directoryInfo.LastWriteTimeUtc < now.AddSeconds(-inactiveWriteThreshold))
                        Directory.Delete(directory, false);
                }
            }
        }

        /// <summary>
        /// Move file from source to destination with replacement condition
        /// </summary>
        /// <param name="sourcePath">Source path of file</param>
        /// <param name="destinationPath">Destination path of file</param>
        /// <param name="replaceIfDestinationExists">Condition where allow function to replace (delete and move)
        /// in case the file already exists on the destination path</param>
        /// <returns>Returns true if move operation succeed otherwise returns false</returns>
        public bool MoveFile(string sourcePath, string destinationPath, bool replaceIfDestinationExists = false)
        {
            if (File.Exists(destinationPath))
                if (replaceIfDestinationExists)
                    File.Delete(destinationPath);
                else
                    return false;

            File.Move(sourcePath, destinationPath);
            return true;
        }


        public bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}
