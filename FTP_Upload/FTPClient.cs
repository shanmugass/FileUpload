using System;
using System.IO;
using System.Net;
using System.Xml;

namespace FTP_Upload
{
    public class FtpClient
    {
        private readonly NetworkCredential _credentials;
        private readonly string _path;
        private readonly string _server;

        public FtpClient(string server, string path, string ftpUserName, string ftpPassword)
        {
            _path = path;
            _server = server;
            _credentials = new NetworkCredential(ftpUserName, ftpPassword);
        }

        public void Send(XmlDocument document, string fileName)
        {
            string fullPath = BuildFullPath(fileName);

            ValidateFileName(fileName, fullPath);

            var request = (FtpWebRequest) WebRequest.Create(fullPath);

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = _credentials;
            request.KeepAlive = true;
            request.EnableSsl = false;
            request.Proxy = null;
            request.UsePassive = false;
            request.Timeout = 120000;
            request.ReadWriteTimeout = 120000;

            using (Stream stream = request.GetRequestStream())
            {
                document.Save(stream);
            }
        }

        public void SendUsingWebClient(string localPath, string fileName)
        {
            string fullPath = BuildFullPath(fileName);

            ValidateFileName(fileName, fullPath);

            using (var client = new WebClient())
            {
                client.Credentials = _credentials;
                client.UploadFile(fullPath, "STOR", "test.xml");
            }
        }


        private void ValidateFileName(string fileName, string fullPath)
        {
            if (fileName.Length != 0) return;

            var exception =
                new Exception(string.Format("Failed to ftp file to remote server. File name is missing. Path: {0}",
                                            fullPath));

            throw exception;
        }

        private string BuildFullPath(string name)
        {
            return (_path.Replace("/", "").Length > 0)
                       ? string.Concat("ftp://", _server, _path, "/", name)
                       : string.Concat("ftp://", _server, "/", name);
        }
    }
}