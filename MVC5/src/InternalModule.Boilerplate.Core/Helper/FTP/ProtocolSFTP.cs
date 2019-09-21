using InternalModule.Boilerplate.Core.Model;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Helper
{
    public class ProtocolSFTP : FtpBase
    {
        public string PrivateKeyPath { get; set; }
        public string Passphrase { get; set; }

        public ProtocolSFTP(string server, int port, string user, string password, string path, string privateKeyPath, string passphrase)
            : base(server, port, user, password, path)
        {
            PrivateKeyPath = privateKeyPath;
            Passphrase = passphrase;
        }

        private ConnectionInfo GetConnectionInfo()
        {
            // Setup Credentials and Server Information
            ConnectionInfo connInfo;

            if (!string.IsNullOrEmpty(PrivateKeyPath) && !string.IsNullOrEmpty(Passphrase))
            {
                connInfo = new ConnectionInfo(Server, Port, User, new PasswordAuthenticationMethod(User, Password), new PrivateKeyAuthenticationMethod(User, new PrivateKeyFile(PrivateKeyPath, Passphrase)));
            }
            else
            {
                connInfo = new ConnectionInfo(Server, Port, User, new PasswordAuthenticationMethod(User, Password));
            }

            return connInfo;
        }

        public override FileInf[] List()
        {
            var files = new List<FileInf>();

            using (var client = new SftpClient(GetConnectionInfo()))
            {
                client.Connect();
                client.ChangeDirectory(Path);

                var sftpFiles = client.ListDirectory(Path);
                foreach (SftpFile file in sftpFiles)
                {
                    if (file.IsRegularFile)
                    {
                        files.Add(new FileInf(file.FullName));
                    }
                }

                client.Disconnect();
            }

            return files.ToArray();
        }

        public override void Upload(FileInf file)
        {
            using (var client = new SftpClient(GetConnectionInfo()))
            {
                client.Connect();
                client.ChangeDirectory(Path);

                using (FileStream fileStream = File.OpenRead(file.Path))
                {
                    client.UploadFile(fileStream, file.RenameOrFileName, true);
                }

                client.Disconnect();
            }
        }

        public override void Download(FileInf file)
        {
            using (var client = new SftpClient(GetConnectionInfo()))
            {
                client.Connect();
                client.ChangeDirectory(Path);

                var destFileName = System.IO.Path.Combine("LocalPath", file.FileName);

                using (FileStream ostream = File.Create(destFileName))
                {
                    client.DownloadFile(file.Path, ostream);
                }

                client.Disconnect();
            }
        }

        public override void Delete(FileInf file)
        {
            using (var client = new SftpClient(GetConnectionInfo()))
            {
                client.Connect();
                client.ChangeDirectory(Path);

                client.DeleteFile(file.Path);

                client.Disconnect();
            }
        }
    }
}
