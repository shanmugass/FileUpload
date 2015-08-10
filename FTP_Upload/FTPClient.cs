using System;
using System.Net;
using System.Windows;
using System.Xml;

namespace FTP_Upload
{
    public class FtpClient
    {
        private readonly string _path;
        private readonly string _server;
        private readonly NetworkCredential _credentials;

        public FtpClient(string server, string path, string ftpUserName, string ftpPassword)
        {
            _path = path;
            _server = server;
            _credentials = new NetworkCredential(ftpUserName, ftpPassword);
        }

        public void Send(XmlDocument document, string fileName)
        {
            try
            {


                var fullPath = BuildFullPath(fileName);

                ValidateFileName(fileName, fullPath);

                var request = (FtpWebRequest)WebRequest.Create(fullPath);

                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UsePassive = false;
                request.Credentials = _credentials;

                using (var stream = request.GetRequestStream())
                {
                    document.Save(stream);
                }

                MessageBox.Show("File successfully uploaded to server");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);

                throw;
            }


        }

        private void ValidateFileName(string fileName, string fullPath)
        {
            if (fileName.Length != 0) return;

            var exception = new Exception(string.Format("Failed to ftp file to remote server. File name is missing. Path: {0}", fullPath));

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
