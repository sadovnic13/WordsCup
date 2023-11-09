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
                int num = rnd.Next(526960, 626960);
                    
                GlobalValues.doc = new HtmlWeb().Load(url + num);

                foreach (var link in GlobalValues.doc.DocumentNode.DescendantsAndSelf("a"))
                {
                    link.Attributes.Remove("href");
                }
            }
            catch (WebException e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
