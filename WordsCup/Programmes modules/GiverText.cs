using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WordsCup
{
    class GiverText
    {
        static GiverText()
        {
            var result = GetHeadlines().Result;
            Console.WriteLine(result);
        }

        public static async Task<string> GetHeadlines()
        {
            var url = "https://ru.wikipedia.org/wiki/%D0%A8%D1%80%D0%B0%D0%B5%D1%80-%D0%9F%D0%B5%D1%82%D1%80%D0%BE%D0%B2,_%D0%94%D0%B0%D0%B2%D0%B8%D0%B4_%D0%9F%D0%B5%D1%82%D1%80%D0%BE%D0%B2%D0%B8%D1%87";

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            HtmlNode headline = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'mw-page-title-main')]");

            return headline.InnerText;
        }
    }
}
