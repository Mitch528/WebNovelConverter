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

            string content = $"<h1 class='chapter'>{link.Name}</h1>";
            content += firstPostNode.InnerHtml;

            return new WebNovelChapter
            {
                Url = link.Url,
                Content = content
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
            var nodes = rootNode.SelectNodes(".//div");

            if (nodes == null)
                return;

            foreach (HtmlNode node in nodes.ToList())
            {
                var tableNodes = node.SelectNodes(".//table");

                if (tableNodes == null || !tableNodes.Any())
                {
                    node.Remove();
                    nodes.Remove(node);
                }
            }
            
            nodes.Last().Remove();
        }
    }
}
