using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HtmlAgilityPack;

namespace WordsCup
{
    class GlobalValues
    {
        public static string url = "https://habr.com/ru/articles/";
        public static HtmlDocument doc {  get; set; }
        
        public static string userDefinedText { get; set; }

        public static void GeneratePage()
        {
            try
            {
                Random rnd = new Random();
                //GlobalValues.url += rnd.Next(0, 99999);
                    
                GlobalValues.doc = new HtmlWeb().Load(url + 4444);
            }
            catch (WebException e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
