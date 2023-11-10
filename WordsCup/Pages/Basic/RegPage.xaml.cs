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

    }
}
