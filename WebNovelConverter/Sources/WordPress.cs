using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebNovelConverter.Sources
{
    public class WordPress : WebNovelSource
    {
        public override async Task<List<WebNovelChapter>> GetChaptersAsync(string baseUrl, int delayPer, IProgress<string> progress)
        {
            string baseContent = await GetWebPage(baseUrl);

            HtmlDocument baseDoc = new HtmlDocument();
            baseDoc.LoadHtml(baseContent);

            HtmlNode entryNode = baseDoc.DocumentNode.SelectSingleNode("//div[@class='entry-content']");

            if (entryNode == null)
                entryNode = baseDoc.DocumentNode.SelectSingleNode("//div[@class='entry']");

            if (entryNode == null)
                entryNode = baseDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'post-content')]");

            var linkNodes = entryNode.SelectNodes(".//a")
                .Where(p => p.InnerText.IndexOf("chapter", StringComparison.CurrentCultureIgnoreCase) >= 0);

            var chapters = new List<WebNovelChapter>();

            int ctr = 1;
            foreach (HtmlNode linkNode in linkNodes)
            {
                string chapterName = linkNode.InnerText.Trim();

                try
                {
                    WebNovelChapter chapter = await GetChapterAsync(linkNode.Attributes["href"].Value);
                    chapter.ChapterId = ctr;

                    chapters.Add(chapter);

                    progress.Report(string.Format("{0} has been processed ({1})", chapterName, ctr));
                }
                catch (Exception ex)
                {
                    progress.Report(string.Format("Error processing {0}", chapterName));
                    progress.Report(ex.ToString());
                }

                ctr++;

                await Task.Delay(delayPer);
            }

            progress.Report("Finished processing chapters!");

            return chapters;
        }

        public override async Task<WebNovelChapter> GetChapterAsync(string url)
        {
            string pageContent = await GetWebPage(url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);

            HtmlNode contentNode = doc.GetElementbyId("content");
            HtmlNode pageNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'page')]");
            HtmlNode postNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'post')]");
            HtmlNode articleNode = doc.DocumentNode.SelectSingleNode("//article");

            string content = string.Empty;

            if (articleNode != null)
            {
                content = articleNode.InnerHtml;
            }
            else if (pageNode != null)
            {
                content = pageNode.SelectSingleNode(".//*[contains(@class, 'title')]").OuterHtml;

                if (string.IsNullOrEmpty(content))
                    content = pageNode.SelectSingleNode(".//*[contains(@class, 'entry-title']").OuterHtml;
            }
            else if (postNode != null)
            {
                HtmlNode titleNode = postNode.SelectSingleNode(".//*[contains(@class, 'post-title')]");

                if (titleNode != null)
                    content = titleNode.OuterHtml;
            }
            else if (contentNode != null)
            {
                HtmlNode headLineNode = contentNode.SelectSingleNode(".//*[@class='entry-headline']");

                if (headLineNode != null)
                    content = headLineNode.OuterHtml;
            }

            if (articleNode == null)
            {
                HtmlNode entryNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'postbody')]");

                if (entryNode == null)
                    entryNode = doc.DocumentNode.SelectSingleNode("//div[@class='entry-content']");

                if (entryNode == null)
                    entryNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'post-content')]");
                
                if (entryNode != null)
                {
                    var paraNodes = entryNode.SelectNodes("p|h1|h2|h3");

                    if (paraNodes == null)
                        paraNodes = entryNode.SelectNodes(".//p|.//h1|.//h2|.//h3");

                    if (paraNodes != null)
                        content += string.Join(string.Empty,
                                paraNodes.SelectMany(p => p.OuterHtml));
                }
            }

            return new WebNovelChapter
            {
                Url = url,
                Content = content
            };
        }
    }
}
