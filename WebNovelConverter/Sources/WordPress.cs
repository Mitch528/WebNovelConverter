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
        public override async Task<List<WebNovelChapter>> GetChaptersAsync(string baseUrl, IProgress<string> progress)
        {
            string baseContent = await GetWebPage(baseUrl);

            HtmlDocument baseDoc = new HtmlDocument();
            baseDoc.LoadHtml(baseContent);

            HtmlNode entryNode = baseDoc.DocumentNode.SelectSingleNode("//div[@class='entry-content']");

            if (entryNode == null)
                entryNode = baseDoc.DocumentNode.SelectSingleNode("//div[@class='entry']");

            var linkNodes = entryNode.SelectNodes(".//a")
                .Where(p => p.InnerText.IndexOf("chapter", StringComparison.CurrentCultureIgnoreCase) >= 0);

            var tasks = new List<Task>();
            var chapters = new List<WebNovelChapter>();

            int ctr = 1;
            foreach (HtmlNode linkNode in linkNodes)
            {
                int ctr0 = ctr;

                tasks.Add(Task.Run(async () =>
                {
                    string chapterName = linkNode.InnerText.Trim();

                    try
                    {
                        WebNovelChapter chapter = await GetChapterAsync(linkNode.Attributes["href"].Value);
                        chapter.ChapterId = ctr0;

                        chapters.Add(chapter);

                        progress.Report(string.Format("{0} has been processed ({1})", chapterName, ctr0));
                    }
                    catch (Exception ex)
                    {
                        progress.Report(string.Format("Error processing {0}", chapterName));
                        progress.Report(ex.ToString());
                    }
                }));

                ctr++;
            }

            await Task.WhenAll(tasks);

            progress.Report("Finished processing chapters!");

            return chapters;
        }

        public override async Task<WebNovelChapter> GetChapterAsync(string url)
        {
            string pageContent = await GetWebPage(url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);

            HtmlNode postNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'page')]");
            HtmlNode articleNode = doc.DocumentNode.SelectSingleNode("//article");

            string content = string.Empty;

            if (articleNode == null)
            {
                if (postNode != null)
                {
                    content = postNode.SelectSingleNode(".//*[contains(@class, 'title')]").OuterHtml;

                    if (string.IsNullOrEmpty(content))
                        content = postNode.SelectSingleNode(".//*[contains(@class, 'entry-title']").OuterHtml;
                }
                else
                {
                    HtmlNode contentNode = doc.GetElementbyId("content");

                    if (contentNode != null)
                    {
                        HtmlNode headLineNode = contentNode.SelectSingleNode(".//*[@class='entry-headline']");

                        content = headLineNode.OuterHtml;
                    }
                }

                HtmlNode entryNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'postbody')]");
                
                if (entryNode == null)
                    entryNode = doc.DocumentNode.SelectSingleNode("//div[@class='entry-content']");

                var paraNodes = entryNode.SelectNodes("p");

                if (paraNodes == null)
                    paraNodes = entryNode.SelectNodes(".//p");

                content += string.Join(string.Empty,
                        paraNodes.SelectMany(p => p.OuterHtml));
            }
            else
            {
                content = articleNode.InnerHtml;
            }

            return new WebNovelChapter
            {
                Url = url,
                Content = content
            };
        }
    }
}
