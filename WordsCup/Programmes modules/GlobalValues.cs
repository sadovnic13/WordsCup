using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HtmlAgilityPack;
using WordsCup.DB;

namespace WordsCup
{
    public class User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int balance { get; set; }
        public string saveWord { get; set; }
    }

    class GlobalValues
    {
        public static User user;

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

                foreach (var node in GlobalValues.doc.DocumentNode.DescendantsAndSelf())
                {
                    if (node.NodeType == HtmlNodeType.Element)
                    {
                        node.Attributes.RemoveAll();
                    }
                }
            }
            catch (WebException e)
            {
                MessageBox.Show(e.Message);
            }
        }


    }
}
