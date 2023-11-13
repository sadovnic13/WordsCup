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
using System.Windows.Shapes;
using WordsCup.DB;
using WordsCup.Pages.Basic;


namespace WordsCup
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void TextBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegPage rg = new RegPage();
            rg.Left = this.Left;
            rg.Top = this.Top;
            rg.Width = this.ActualWidth;
            rg.Height = this.ActualHeight;
            rg.WindowState = this.WindowState;
            rg.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string log = login.Text;
            string pass = password.Password;

           if(DataAccess.UserExists(log, pass))
            {
                GlobalValues.user = DataAccess.GetUser(log);


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
                SuccessPage sp = new SuccessPage("fail.png");
                sp.Owner = this;
                sp.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                sp.ShowDialog();
            }

        }
    }
}
