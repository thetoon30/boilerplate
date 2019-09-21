using InternalModule.Boilerplate.Core.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalModule.Boilerplate.Core.Helper
{
    public enum FtpCommad
    {
        Upload,
        Download,
        Delete,
    }

    /// <summary>
    /// Type of SSL to use.
    /// </summary>
    public enum EncryptionMode
    {
        /// <summary>
        /// Explicit is TLS. 
        /// </summary>
        Explicit,
        /// <summary>
        /// Implicit is SSL. 
        /// </summary>
        Implicit
    }

    public class ProtocolBase
    {
        private readonly FtpBase _ftp;
        private readonly FtpCommad _cmd;
        private readonly int _retryCount;
        private readonly int _retryTimeout;

        public ProtocolBase(Protocol protocol, FtpCommad cmd)
        {
            string server = ConfigurationManager.AppSettings["FtpServer"];
            int port = int.Parse(ConfigurationManager.AppSettings["FtpPort"]);
            string user = ConfigurationManager.AppSettings["FtpUser"];
            string password = ConfigurationManager.AppSettings["FtpPassword"];
            string path = ConfigurationManager.AppSettings["FtpPart"];

            switch (protocol)
            {
                case Protocol.Ftp:
                    _ftp = new ProtocolFTP(server, port, user, password, path);
                    break;
                case Protocol.Ftps:
                    EncryptionMode encryptionMode = (EncryptionMode)Enum.Parse(typeof(EncryptionMode), ConfigurationManager.AppSettings["FtpEncryption"], true);
                    _ftp = new ProtocolFTPS(server, port, user, password, path, encryptionMode);
                    break;
                case Protocol.Sftp:
                    string privateKeyPath = ConfigurationManager.AppSettings["FtpPrivateKeyPath"];
                    string passphrase = ConfigurationManager.AppSettings["FtpPassphrase"];
                    _ftp = new ProtocolSFTP(server, port, user, password, path, privateKeyPath, passphrase);
                    break;
            }

            _cmd = cmd;
            _retryCount = int.Parse(ConfigurationManager.AppSettings["FtpRetry"]);
            _retryTimeout = int.Parse(ConfigurationManager.AppSettings["FtpTimeout"]);
        }

        public List<FileInf> GetFiles()
        {
            Console.WriteLine("Processing files...");

            List<FileInf> results = new List<FileInf>();
            bool success = true;
            bool atLeastOneSucceed = false;

            int r = 0;
            while (r <= _retryCount)
            {
                try
                {
                    FileInf[] files = _ftp.List();
                    results.AddRange(files);

                    atLeastOneSucceed = true;
                    break;
                }
                catch (Exception e)
                {
                    r++;

                    if (r > _retryCount)
                    {
                        Console.WriteLine("An error occured while listing files. Error: {0}", e.Message);
                        success = false;
                    }
                    else
                    {
                        Console.WriteLine("An error occured while listing files. Error: {0}. The task will tray again.", e.Message);
                    }
                }
            }

            if (success == false && atLeastOneSucceed == true)
            {
                Console.WriteLine("Warning");
            }
            else if (success == false)
            {
                Console.WriteLine("Error");
            }

            Console.WriteLine("Finished");

            return results;
        }

        public void Run(FileInf[] files)
        {
            Console.WriteLine("Processing files...");

            bool success = true;
            bool atLeastOneSucceed = false;

            for (int i = files.Length - 1; i > -1; i--)
            {
                FileInf file = files[i];

                int r = 0;
                while (r <= _retryCount)
                {
                    try
                    {
                        switch (_cmd)
                        {
                            case FtpCommad.Upload:
                                _ftp.Upload(file);
                                break;
                            case FtpCommad.Download:
                                _ftp.Download(file);
                                break;
                            case FtpCommad.Delete:
                                _ftp.Delete(file);
                                break;
                        }

                        atLeastOneSucceed = true;
                        break;
                    }
                    catch (Exception e)
                    {
                        r++;

                        if (r > _retryCount)
                        {
                            Console.WriteLine("An error occured while processing the file {0}. Error: {1}", file.Path, e.Message);
                            success = false;
                        }
                        else
                        {
                            Console.WriteLine("An error occured while processing the file {0}. Error: {1}. The task will tray again.", file.Path, e.Message);
                        }
                    }
                }
            }

            if (success == false && atLeastOneSucceed == true)
            {
                Console.WriteLine("Warning");
            }
            else if (success == false)
            {
                Console.WriteLine("Error");
            }

            Console.WriteLine("Finished");
        }
    }
}
