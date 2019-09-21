using FluentFTP;
using InternalModule.Boilerplate.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Helper
{
    public class ProtocolFTP : FtpBase
    {
        private const int BufferSize = 8192;

        public ProtocolFTP(string server, int port, string user, string password, string path)
            : base(server, port, user, password, path)
        {
        }

        public override FileInf[] List()
        {
            var files = new List<FileInf>();

            var client = new FtpClient
            {
                Host = Server,
                Port = Port,
                Credentials = new NetworkCredential(User, Password)
            };

            client.Connect();
            client.SetWorkingDirectory(Path);

            var ftpFiles = ListFiles(client);

            client.Disconnect();

            return files.ToArray();
        }

        public static FileInf[] ListFiles(FtpClient client)
        {
            var files = new List<FileInf>();

            var ftpListItems = client.GetListing();

            foreach (FtpListItem item in ftpListItems)
            {
                if (item.Type == FtpFileSystemObjectType.File)
                {
                    files.Add(new FileInf(item.FullName));
                }
            }

            return files.ToArray();
        }

        public override void Upload(FileInf file)
        {
            var client = new FtpClient
            {
                Host = Server,
                Port = Port,
                Credentials = new NetworkCredential(User, Password)
            };

            client.Connect();
            client.SetWorkingDirectory(Path);

            UploadFile(client, file);

            client.Disconnect();
        }

        public static void UploadFile(FtpClient client, FileInf file)
        {
            using (Stream istream = File.Open(file.Path, FileMode.Open, FileAccess.Read))
            {
                using (Stream ostream = client.OpenWrite(file.RenameOrFileName, FtpDataType.Binary))
                {
                    var buffer = new byte[BufferSize];
                    int r;

                    while ((r = istream.Read(buffer, 0, BufferSize)) > 0)
                    {
                        ostream.Write(buffer, 0, r);
                    }
                }
            }
        }

        public override void Download(FileInf file)
        {
            var client = new FtpClient
            {
                Host = Server,
                Port = Port,
                Credentials = new NetworkCredential(User, Password)
            };

            client.Connect();
            client.SetWorkingDirectory(Path);

            DownloadFile(client, file);

            client.Disconnect();
        }

        public static void DownloadFile(FtpClient client, FileInf file)
        {
            var destFileName = System.IO.Path.Combine("LocalPath", file.FileName);

            using (Stream istream = client.OpenRead(file.Path))
            {
                using (Stream ostream = File.Create(destFileName))
                {
                    // istream.Position is incremented accordingly to the reads you perform
                    // istream.Length == file size if the server supports getting the file size
                    // also note that file size for the same file can vary between ASCII and Binary
                    // modes and some servers won't even give a file size for ASCII files! It is
                    // recommended that you stick with Binary and worry about character encodings
                    // on your end of the connection.
                    var buffer = new byte[BufferSize];
                    int r;

                    while ((r = istream.Read(buffer, 0, BufferSize)) > 0)
                    {
                        ostream.Write(buffer, 0, r);
                    }
                }
            }
        }

        public override void Delete(FileInf file)
        {
            var client = new FtpClient
            {
                Host = Server,
                Port = Port,
                Credentials = new NetworkCredential(User, Password)
            };

            client.Connect();
            client.SetWorkingDirectory(Path);

            client.DeleteFile(file.Path);

            client.Disconnect();
        }
    }
}
