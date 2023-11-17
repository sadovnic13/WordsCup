using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using WordsCup.DB;
using WordsCup.Pages.Additional;
using WordsCup.Pages.Basic;
using WordsCup.Programmes_modules;

namespace WordsCup
{
    /// <summary>
    /// Логика взаимодействия для WordSelectPage.xaml
    /// </summary>
    public partial class WordSelectPage : Window
    {
        public WordSelectPage()
        {
            InitializeComponent();

            if(GlobalValues.user.saveWord == null)
            {
                Continue.Visibility = Visibility.Collapsed;
            }
            Balance.Text += " " + GlobalValues.user.balance;
        }

        private async void Win_Initialized(object sender, EventArgs e)
        {
            GlobalValues.doc = await GeneratePages.ViewTextBrowser();
            GlobalValues.tempDoc = await GeneratePages.ViewTextBrowser();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Difficulty.SelectedIndex == -1)
            {
                SuccessPage sp = new SuccessPage("fail.png");
                sp.Owner = this;
                sp.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                sp.ShowDialog();
                return;
            }
            this.IsEnabled = false;
            
            GlobalValues.user.successPoint = Difficulty.SelectedIndex + 1;
            BlurEffect bE = new BlurEffect();
            bE.Radius = 5;
            Effect = bE;

            this.ResizeMode = ResizeMode.NoResize;
            DownloadAnimation dialog = new DownloadAnimation();

            dialog.Owner = this;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.Show();

            if(GlobalValues.doc == null)
            {
                GlobalValues.doc = await GeneratePages.ViewTextBrowser();
                GlobalValues.tempDoc = await GeneratePages.ViewTextBrowser();
            }
            
            var cleanString = Regex.Replace(GlobalValues.doc, "<.*?>", string.Empty);
            cleanString = Regex.Replace(cleanString, @"[^\w\s]", string.Empty);
            cleanString = Regex.Replace(cleanString, @"\d", string.Empty);

            // Разбить строку на слова
            string[] words = cleanString.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            Random random = new Random();
            string selectedWord = string.Empty;

            // Предположим, что comboBox.SelectedIndex возвращает выбранный пункт в ComboBox
            switch (Difficulty.SelectedIndex)
            {
                case 0:
                    words = words.Where(word => word.Length >= 3 && word.Length <= 4).ToArray();
                    break;
                case 1:
                    words = words.Where(word => word.Length >= 5 && word.Length <= 7).ToArray();
                    break;
                case 2:
                    words = words.Where(word => word.Length >= 7 && word.Length <= 9).ToArray();
                    break;
            }

            if (words.Length > 0)
            {
                selectedWord = words[random.Next(0, words.Length)].ToLower();
            }

            GlobalValues.user.saveWord = selectedWord;


            SearchPage sP = await SearchPage.CreateAsync();
            sP.Left = this.Left;
            sP.Top = this.Top;
            sP.Width = this.ActualWidth;
            sP.Height = this.ActualHeight;
            sP.WindowState = this.WindowState;

            sP.Show();
            this.Close();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.UpdateUser(GlobalValues.user);

            LoginPage lP = new LoginPage();
            lP.Left = this.Left;
            lP.Top = this.Top;
            lP.Width = this.ActualWidth;
            lP.Height = this.ActualHeight;
            lP.WindowState = this.WindowState;
            lP.Show();
            this.Close();
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

        private async void Continue_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;

            BlurEffect bE = new BlurEffect();
            bE.Radius = 5;
            Effect = bE;

            this.ResizeMode = ResizeMode.NoResize;
            DownloadAnimation dialog = new DownloadAnimation();

            dialog.Owner = this;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.Show();

            SearchPage sP = null;
            if (GlobalValues.doc == null)
            {
                sP = await SearchPage.CreateAsync();
                sP.Left = this.Left;
                sP.Top = this.Top;
                sP.Width = this.ActualWidth;
                sP.Height = this.ActualHeight;
                sP.WindowState = this.WindowState;
                sP.Show();
                this.Close();
            }
            else
            {
                sP = new SearchPage();
                sP.Left = this.Left;
                sP.Top = this.Top;
                sP.Width = this.ActualWidth;
                sP.Height = this.ActualHeight;
                sP.WindowState = this.WindowState;
                sP.Show();
                this.Close();
            }
            
        }

        
    }
}
