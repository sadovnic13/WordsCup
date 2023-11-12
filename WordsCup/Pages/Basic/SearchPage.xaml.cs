using HtmlAgilityPack;
using mshtml;
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
            try
            {
                var htmlContent = await Task.Run(async () =>
                {
                    GlobalValues.GeneratePage();

                    //var bodyContent = GlobalValues.doc.DocumentNode.SelectSingleNode("//div[@class='mw-parser-output']");

                    //var nodes = bodyContent.SelectNodes("//h2|//p|//ul");
                    //
                    HtmlNode bodyContent;


                    while (true)
                    {
                        bodyContent = GlobalValues.doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[1]/div[2]/main/div/div/div/div[1]/div/div[2]/div[1]/div[1]/div/article/div[2]/div[2]/div[1]/div/div");

                        if (bodyContent == null)
                        {
                            GlobalValues.GeneratePage();
                        }
                        else
                        {
                            break;
                        } 
                    }
                    var nodes = bodyContent.ChildNodes;

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
            catch(NullReferenceException)
            {
                await ViewTextBrowser();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WordSelectPage sP = new WordSelectPage();
            sP.Left = this.Left;
            sP.Top = this.Top;
            sP.Width = this.ActualWidth;
            sP.Height = this.ActualHeight;
            sP.WindowState = this.WindowState;
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

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var doc = TB.Document as HTMLDocument;
            if (doc != null)
            {
                var currentSelection = doc.selection;
                if (currentSelection != null)
                {
                    dynamic selectionRange = currentSelection.createRange();
                    if (selectionRange != null)
                    {
                        SuccessPage sp;
                        var selectionText = (string)selectionRange.text;
                            
                        if (selectionText != null && selectionText.Trim() == "о" )
                        {
                            sp = new SuccessPage("success.png");
                        }
                        else
                        {
                            sp = new SuccessPage("fail.png");
                        }
                        sp.Owner = this;
                        sp.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        sp.ShowDialog();                        
                    }
                }
            }
        }
    }
}
