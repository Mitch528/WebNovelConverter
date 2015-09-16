using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebNovelConverter.Sources
{
    public class WordPress : WebNovelSource
    {
        public override string BaseUrl => null;

        public override async Task<ChapterLink[]> GetLinks(string baseUrl)
        {
            string baseContent = await GetWebPage(baseUrl);

            HtmlDocument baseDoc = new HtmlDocument();
            baseDoc.LoadHtml(baseContent);

            HtmlNode entryNode = baseDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'entry-content')]");

            if (entryNode == null)
                entryNode = baseDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'entry')]");

            if (entryNode == null)
                entryNode = baseDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'post-content')]");

            if (entryNode == null)
                entryNode = baseDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'the-content')]");

            if (entryNode == null)
                return null;

            var linkNodes = entryNode.SelectNodes(".//a");

            var links = new List<ChapterLink>();
            foreach (HtmlNode linkNode in linkNodes)
            {
                if (string.IsNullOrWhiteSpace(linkNode.InnerText))
                    continue;

                ChapterLink link = new ChapterLink
                {
                    Name = WebUtility.HtmlDecode(linkNode.InnerText),
                    Url = linkNode.Attributes["href"].Value,
                    Unknown = true
                };

                if (link.Name.IndexOf("chap", StringComparison.CurrentCultureIgnoreCase) >= 0)
                    link.Unknown = false;

                links.Add(link);
            }

            return links.ToArray();
        }

        public override async Task<WebNovelChapter> GetChapterAsync(ChapterLink link)
        {
            string pageContent = await GetWebPage(link.Url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);

            HtmlNode articleNode = doc.DocumentNode.SelectSingleNode("//article");

            string content = string.Empty;
            if (articleNode != null)
            {
                HtmlNode ifNode = articleNode.SelectSingleNode(".//iframe");

                if (ifNode != null)
                {
                    string ifUrl = ifNode.Attributes["src"].Value;

                    content = await GetWebPage(ifUrl);
                }
                else
                {
                    RemoveBloat(articleNode);
                    content = articleNode.InnerHtml;
                }
            }

            HtmlNode entryNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'post-entry')]");

            if (entryNode == null)
                entryNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'entry-content')]");

            if (entryNode == null)
                entryNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'post-content')]");

            if (entryNode == null)
                entryNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'postbody')]");

            if (entryNode != null)
            {
                HtmlNode ifNode = entryNode.SelectSingleNode(".//iframe");

                if (ifNode != null)
                {
                    content = ifNode.InnerHtml;
                }
                else
                {
                    RemoveBloat(entryNode);

                    content = entryNode.OuterHtml;
                }
            }

            return new WebNovelChapter
            {
                Url = link.Url,
                Content = content
            };
        }

        public override Task<string> GetNovelCover(string baseUrl)
        {
            return Task.FromResult(string.Empty);
        }

        protected virtual void RemoveBloat(HtmlNode node)
        {
            var shareNodes = node.SelectNodes(@".//div[contains(@class, 'sharedaddy')]|.//div[contains(@class, 'share-story-container')]
|.//div[contains(@class, 'code-block')]|.//div[contains(@class, 'comments-area')]");

            if (shareNodes != null)
                foreach (HtmlNode toRemove in shareNodes)
                    toRemove.Remove();
        }
    }
}
