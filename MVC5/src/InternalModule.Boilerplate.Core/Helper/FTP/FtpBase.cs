using InternalModule.Boilerplate.Core.Model;

namespace InternalModule.Boilerplate.Core.Helper
{
    public enum Protocol
    {
        Ftp,
        Ftps,
        Sftp
    }

    public abstract class FtpBase
    {
        public string Server { get; private set; }
        public int Port { get; private set; }
        public string User { get; private set; }
        public string Password { get; private set; }
        public string Path { get; private set; }

        protected FtpBase(string server, int port, string user, string password, string path)
        {
            Server = server;
            Port = port;
            User = user;
            Password = password;
            Path = path;
        }

        public abstract FileInf[] List();
        public abstract void Upload(FileInf file);
        public abstract void Download(FileInf file);
        public abstract void Delete(FileInf file);
    }
}
