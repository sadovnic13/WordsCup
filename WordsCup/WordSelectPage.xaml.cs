using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                var url = "https://ru.wikipedia.org/wiki/Special:Random";
                GlobalValues.doc = new HtmlWeb().Load(url);
            }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GlobalValues.userDefinedText = UserWord.Text;

            SearchPage sP = new SearchPage();
            sP.Show();
            this.Close();
        }
    }
}
