using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            var server = ConfigurationManager.AppSettings["Server"];
            var folder = ConfigurationManager.AppSettings["FolderPath"];
            var userName = ConfigurationManager.AppSettings["UserName"];
            var password = ConfigurationManager.AppSettings["Password"];            

            var client = new FtpClient(server, folder, userName, password);

            var xmlDocument = new XmlDocument();

            xmlDocument.Load("test.xml");

            client.Send(xmlDocument, DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xml");

        }
    }
}
