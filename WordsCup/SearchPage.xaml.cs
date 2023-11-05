using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WordsCup
{
    /// <summary>
    /// Логика взаимодействия для SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Window
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        public static async Task<SearchPage> CreateAsync()
        {
            SearchPage sP = new SearchPage();
            await sP.ViewTextBrowser();
            return sP;
        }

        private async Task ViewTextBrowser()
        {
            var htmlContent = await Task.Run(() =>
            {
                GlobalValues.GeneratePage();
                foreach (var link in GlobalValues.doc.DocumentNode.DescendantsAndSelf("a"))
                {
                    link.Attributes.Remove("href");
                }

                var bodyContent = GlobalValues.doc.DocumentNode.SelectSingleNode("//div[@class='mw-parser-output']");
                var nodes = bodyContent.SelectNodes("//h2|//p|//ul");

                // Объединить HTML всех выбранных узлов в одну строку
                return string.Join("\n", nodes.Select(node => node.OuterHtml));
            });

            string html = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='UTF-8'>
                </head>
                <body>
                    {htmlContent}
                </body>
                </html>";
            TB.NavigateToString(html);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WordSelectPage sP = new WordSelectPage();
            sP.Show();
            this.Close();
        }

        private async void MoreTextButton(object sender, RoutedEventArgs e)
        {
            BlurEffect bE = new BlurEffect();
            bE.Radius = 5;
            Effect = bE;  

            DownloadAnimation dialog = new DownloadAnimation();

            dialog.Owner = this;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            this.IsEnabled = false;
            TB.Visibility = Visibility.Hidden;
            dialog.Show();
            await ViewTextBrowser();            

            dialog.Close();

            TB.Visibility = Visibility.Visible;
            IsEnabled = true;
            bE.Radius = 0;
            Effect = bE;

        }
    }
}
