using System;
using System.Collections.Generic;
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
using System.Net.Http;
using System.Net;
using System.IO;

namespace AsyncExampleWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            startButton.IsEnabled = false;
            resultsTextBox.Clear();
            await SumPageSizesAsync();
            resultsTextBox.Text += "\r\nControl returned to startButton_Click.";
            startButton.IsEnabled = true;
        }

        private async Task SumPageSizesAsync()
        {
            List<string> urlList = SetUpURLList();
            HttpClient client = new HttpClient() { MaxResponseContentBufferSize = 1000000 };
            var total = 0;
            foreach (var url in urlList)
            {
                byte[] urlContents = await client.GetByteArrayAsync(url);
                DisplayResults(url, urlContents);
                total += urlContents.Length;
            }

            resultsTextBox.Text += string.Format("\r\n\r\nTotal bytes returned:  {0}\r\n", total);
        }
        private List<string> SetUpURLList()
        {
            var urls = new List<string>()
            {
               "http://msdn.microsoft.com/library/windows/apps/br211380.aspx",
               "http://msdn.microsoft.com",
               "http://msdn.microsoft.com/library/hh290136.aspx",
               "http://msdn.microsoft.com/library/ee256749.aspx",
               "http://msdn.microsoft.com/library/hh290138.aspx",
               "http://msdn.microsoft.com/library/hh290140.aspx",
               "http://msdn.microsoft.com/library/dd470362.aspx",
               "http://msdn.microsoft.com/library/aa578028.aspx",
               "http://msdn.microsoft.com/library/ms404677.aspx",
               "http://msdn.microsoft.com/library/ff730837.aspx"
             };
            return urls;
        }

        private void DisplayResults(string url, byte[] content)
        {
            var bytes = content.Length;
            var displayURL = url.Replace("http://", "");
            resultsTextBox.Text += string.Format("\n{0,-58} {1,8}", displayURL, bytes);
        }

    }
}
