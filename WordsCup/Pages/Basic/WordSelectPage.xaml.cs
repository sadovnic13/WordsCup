using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WordsCup.DB;
using WordsCup.Pages.Additional;
using WordsCup.Pages.Basic;

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

        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;

            BlurEffect bE = new BlurEffect();
            bE.Radius = 5;
            Effect = bE;

            DownloadAnimation dialog = new DownloadAnimation();
            this.ResizeMode = ResizeMode.NoResize;

            dialog.Owner = this;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.Show();

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
    }
}
