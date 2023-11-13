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

namespace WordsCup.Pages.Basic
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Window
    {

        public RegPage()
        {
            InitializeComponent();
        }

        private void TextBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginPage lg = new LoginPage();
            lg.Left = this.Left;
            lg.Top = this.Top;
            lg.Width = this.ActualWidth;
            lg.Height = this.ActualHeight;
            lg.WindowState = this.WindowState;
            lg.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string log = login.Text;
            string pass = password.Password;
            string secondPass = secondPassword.Password;

            if (string.IsNullOrEmpty(log) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(secondPass))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (log.Length < 4)
            {
                MessageBox.Show("Логин должен содержать не менее 4 символов.");
                return;
            }
            
            if (pass.Length < 8)
            {
                MessageBox.Show("Пароль должны содержать не менее 8 символов.");
                return;
            }

            if (pass != secondPass)
            {
                MessageBox.Show("Пароли не совпадают.");
                return;
            }

            try
            {
                DataAccess.AddUser(log, pass);
            }
            catch (Microsoft.Data.Sqlite.SqliteException ex)
            {
                if (ex.SqliteErrorCode == 19)
                {
                    MessageBox.Show("Выбранный логин уже занят. Пожалуйста, выберите другой логин.");
                    return;
                }
            }

            SuccessPage sp = new SuccessPage("success.png");
            sp.Owner = this;
            sp.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            sp.ShowDialog();

            LoginPage lg = new LoginPage();
            lg.Left = this.Left;
            lg.Top = this.Top;
            lg.Width = this.ActualWidth;
            lg.Height = this.ActualHeight;
            lg.WindowState = this.WindowState;
            lg.Show();
            this.Close();
        }
    }
}
