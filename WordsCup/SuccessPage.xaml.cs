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

namespace WordsCup
{
    /// <summary>
    /// Логика взаимодействия для SuccessPage.xaml
    /// </summary>
    public partial class SuccessPage : Window
    {
        public SuccessPage(string nameImage)
        {
            InitializeComponent();
            img.Source = new BitmapImage(new Uri("Images/System_icons/" + nameImage, UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
