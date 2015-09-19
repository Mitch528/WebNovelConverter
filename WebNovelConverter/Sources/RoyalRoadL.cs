using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebNovelConverter.Sources
{
    public class RoyalRoadL : WebNovelSource
    {
        public override string BaseUrl => "http://royalroadl.com/";

        public override async Task<ChapterLink[]> GetLinks(string baseUrl)
        {
            string baseContent = await GetWebPage(baseUrl);

            HtmlDocument baseDoc = new HtmlDocument();
            baseDoc.LoadHtml(baseContent);

            var chapterNodes = baseDoc.DocumentNode.SelectNodes("//li[@class='chapter']");

            var links = new List<ChapterLink>();
            foreach (HtmlNode chapterNode in chapterNodes)
            {
                HtmlNode linkNode = chapterNode.SelectSingleNode(".//a");

                string title = linkNode.Attributes["title"].Value;

                ChapterLink link = new ChapterLink
                {
                    Name = title,
                    Url = linkNode.Attributes["href"].Value,
                    Unknown = false
                };

                links.Add(link);
            }

            return links.ToArray();
        }

        public override async Task<WebNovelChapter> GetChapterAsync(ChapterLink link)
        {
            string pageContent = await GetWebPage(link.Url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);

            HtmlNode firstPostNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'post_body')]");

            RemoveNonTables(firstPostNode);
            
            return new WebNovelChapter
            {
                Url = link.Url,
                Content = firstPostNode.InnerHtml
            };
        }

        public override async Task<string> GetNovelCover(string baseUrl)
        {
            string baseContent = await GetWebPage(baseUrl);

            HtmlDocument baseDoc = new HtmlDocument();
            baseDoc.LoadHtml(baseContent);

            return baseDoc.GetElementbyId("fiction-header")?.SelectSingleNode("img").Attributes["src"].Value;
        }

        protected virtual void RemoveNonTables(HtmlNode rootNode)
        {
            var nodes = rootNode.Descendants("div").ToList();
            
            HtmlNode currentNode = nodes.FirstOrDefault();
            while (currentNode != null)
            {
                if (!currentNode.Descendants("table").Any())
                {
                    currentNode.Remove();
                    nodes.Remove(currentNode);
                }
                
                currentNode = currentNode.NextSibling;
            }

            nodes.Last().Remove();
        }
    }
}
