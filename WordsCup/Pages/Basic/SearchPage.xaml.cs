using HtmlAgilityPack;
using mshtml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using WordsCup.DB;
using WordsCup.Pages.Additional;
using WordsCup.Programmes_modules;

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

            Word.Text = GlobalValues.user.saveWord;
            Balance.Text += " " + GlobalValues.user.balance;

            TB.NavigateToString(GlobalValues.doc);
        }

        public static async Task<SearchPage> CreateAsync()
        {
            SearchPage sP = new SearchPage();
            GlobalValues.doc = GlobalValues.tempDoc;
            GlobalValues.tempDoc = await GeneratePages.ViewTextBrowser();
            return sP;
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
            this.ResizeMode = ResizeMode.NoResize;
            this.IsEnabled = false;
            TB.Visibility = Visibility.Hidden;
            dialog.Show();

            GlobalValues.doc = GlobalValues.tempDoc;
            TB.NavigateToString(GlobalValues.doc);

            dialog.Close();

            TB.Visibility = Visibility.Visible;
            IsEnabled = true;
            this.ResizeMode = ResizeMode.CanResize;
            bE.Radius = 0;
            Effect = bE;
                
            GlobalValues.tempDoc = await GeneratePages.ViewTextBrowser();
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
                            
                        //if (selectionText != null && selectionText.Trim() == "о")
                        if (selectionText != null && selectionText.Trim() == GlobalValues.user.saveWord)
                        {
                            sp = new SuccessPage("success.png");
                            sp.Owner = this;
                            sp.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                            sp.ShowDialog();

                            GlobalValues.user.balance += GlobalValues.user.successPoint;
                            GlobalValues.user.saveWord = null;

                            WordSelectPage ws = new WordSelectPage();
                            ws.Left = this.Left;
                            ws.Top = this.Top;
                            ws.Width = this.ActualWidth;
                            ws.Height = this.ActualHeight;
                            ws.WindowState = this.WindowState;
                            ws.Show();
                            this.Close();
                        }
                        else
                        {
                            sp = new SuccessPage("fail.png");
                            sp.Owner = this;
                            sp.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                            sp.ShowDialog();
                        }
                                                
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InformationalPage iP = new InformationalPage();
            iP.Owner = this;
            iP.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            iP.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataAccess.UpdateUser(GlobalValues.user);
        }
    }
}
