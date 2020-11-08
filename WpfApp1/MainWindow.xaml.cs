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
using HtmlAgilityPack;
using System.Diagnostics;

namespace WpfApp1
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

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            string VkLink = LinkTextBox.Text;
            WebClient wc = new WebClient();
            string answer = wc.DownloadString(VkLink); // Скачивание разметки страницы

            // Сохранение страницы в html формате
            using (StreamWriter writer = new StreamWriter("page.html"))
            {
                writer.Write(answer);
            }

            // Загрузка html документа 
            var doc = new HtmlDocument();
            doc.Load("page.html");

            //Получение статуса пользователя из html
            HtmlNode spanContainer = doc.DocumentNode.SelectSingleNode("//span[@class='pp_last_activity_text']");
            string status = spanContainer.InnerText;

            StatusText.Text = status;

            //Открытие ссылки в браузере
            try
            {
                Process.Start(VkLink);
            }
            catch
            {
                Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", VkLink);
            }
            

        }
    }
}
