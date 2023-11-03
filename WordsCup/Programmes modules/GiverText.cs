using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using HtmlAgilityPack;

namespace WordsCup
{
    class GiverText
    {
        static GiverText()
        {
        }

        public static string GetHeadlines()
        {      
            HtmlNodeCollection headline = GlobalValues.doc.DocumentNode.SelectNodes("//span[contains(@class, 'mw-page-title-main')]");

            return headline[0].InnerText;
        }

        public static string GetBodylines()
        {
            HtmlNodeCollection headline = GlobalValues.doc.DocumentNode.SelectNodes("//div[@id='bodyContent']");
            //string all = "";
            //foreach (var item in headline)
            //{
            //    all += item.InnerText;
            //}
            return headline[0].InnerText;
        }
    }
}
