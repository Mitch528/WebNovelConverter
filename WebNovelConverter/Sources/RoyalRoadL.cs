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

        public override Task<List<WebNovelChapter>> GetChaptersAsync(string url, int delayPer, IProgress<string> progress)
        {
            return null;
        }

        public override async Task<WebNovelChapter> GetChapterAsync(ChapterLink link)
        {
            string pageContent = await GetWebPage(link.Url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);

            var postsNode = doc.GetElementbyId("posts");
            var postNodes = postsNode.SelectNodes(".//div[contains(@class, 'post_body')]");
            HtmlNode firstPostNode = postNodes.First();

            RemoveNav(firstPostNode);

            string content = string.Format("<h1>{0}</h1>", link.Name);
            content += firstPostNode.InnerHtml;

            return new WebNovelChapter
            {
                Url = link.Url,
                Content = content
            };
        }

        private void RemoveNav(HtmlNode node)
        {
            var divNodes = node.SelectNodes("div");

            divNodes.Last().Remove();
        }
    }
}
