using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace angular2inter.Core.Helper
{
    public static class CompressionHelper
    {
        public static void ExtractFileToDirectory(string zipFileName, string outputDirectory)
        {
            ZipFile zip = ZipFile.Read(zipFileName);
            Directory.CreateDirectory(outputDirectory);
            foreach (ZipEntry e in zip)
            {
                // check if you want to extract e or not
                //if (e.FileName == "TheFileToExtract")
                    e.Extract(outputDirectory, ExtractExistingFileAction.OverwriteSilently);
            }
        }
    }
}
