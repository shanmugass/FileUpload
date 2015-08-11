using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Windows;
using System.Xml;

namespace FTP_Upload
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string server = ConfigurationManager.AppSettings["Domain"];
                string folder = ConfigurationManager.AppSettings["FolderPath"];
                string userName = ConfigurationManager.AppSettings["UserName"];
                string password = ConfigurationManager.AppSettings["Password"];

                var client = new FtpClient(server, folder, userName, password);

                var xmlDocument = new XmlDocument();

                xmlDocument.Load("test.xml");

                client.Send(xmlDocument, DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xml");
                MessageBox.Show("File successfully uploaded to server");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new WebClient();
                Stream data = client.OpenRead(ConfigurationManager.AppSettings["UrlToRead"]);
                var reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                MessageBox.Show(s);
                data.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                string server = ConfigurationManager.AppSettings["Domain"];
                string folder = ConfigurationManager.AppSettings["FolderPath"];
                string userName = ConfigurationManager.AppSettings["UserName"];
                string password = ConfigurationManager.AppSettings["Password"];

                var client = new FtpClient(server, folder, userName, password);

                client.SendUsingWebClient("test.xml", DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xml");
                MessageBox.Show("File successfully uploaded to server");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
    }
}