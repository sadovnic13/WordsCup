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
    /// Логика взаимодействия для WordSelectPage.xaml
    /// </summary>
    public partial class WordSelectPage : Window
    {
        public WordSelectPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GLobalValues.userDefinedText = UserWord.Text;

            SearchPage sP = new SearchPage();
            sP.Show();
            this.Close();
        }
    }
}
