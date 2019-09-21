using FluentFTP;
using InternalModule.Boilerplate.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Helper
{
    public class ProtocolFTPS : FtpBase
    {
        private readonly FtpEncryptionMode _encryptionMode;

        public ProtocolFTPS(string server, int port, string user, string password, string path, EncryptionMode encryptionMode)
            : base(server, port, user, password, path)
        {
            switch (encryptionMode)
            {
                case EncryptionMode.Explicit:
                    _encryptionMode = FtpEncryptionMode.Explicit;
                    break;
                case EncryptionMode.Implicit:
                    _encryptionMode = FtpEncryptionMode.Implicit;
                    break;
            }
        }

        private static void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e)
        {
            e.Accept = true;
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

            client.DataConnectionType = FtpDataConnectionType.PASV;
            client.EncryptionMode = _encryptionMode;

            client.Connect();
            client.SetWorkingDirectory(Path);

            var ftpFiles = ProtocolFTP.ListFiles(client);

            client.Disconnect();

            return files.ToArray();
        }

        public override void Upload(FileInf file)
        {
            var client = new FtpClient
            {
                Host = Server,
                Port = Port,
                Credentials = new NetworkCredential(User, Password),
            };

            client.ValidateCertificate += OnValidateCertificate;
            client.DataConnectionType = FtpDataConnectionType.PASV;
            client.EncryptionMode = _encryptionMode;

            client.Connect();
            client.SetWorkingDirectory(Path);

            ProtocolFTP.UploadFile(client, file);

            client.Disconnect();
        }

        public override void Download(FileInf file)
        {
            var client = new FtpClient
            {
                Host = Server,
                Port = Port,
                Credentials = new NetworkCredential(User, Password)
            };

            client.EncryptionMode = _encryptionMode;

            client.Connect();
            client.SetWorkingDirectory(Path);

            ProtocolFTP.DownloadFile(client, file);

            client.Disconnect();
        }

        public override void Delete(FileInf file)
        {
            var client = new FtpClient
            {
                Host = Server,
                Port = Port,
                Credentials = new NetworkCredential(User, Password)
            };

            client.EncryptionMode = _encryptionMode;

            client.Connect();
            client.SetWorkingDirectory(Path);

            client.DeleteFile(file.Path);

            client.Disconnect();
        }
    }
}
