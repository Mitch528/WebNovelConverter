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

            var linkNodes = entryNode.SelectNodes(".//a");

            var links = new List<ChapterLink>();
            foreach (HtmlNode linkNode in linkNodes)
            {
                if (string.IsNullOrWhiteSpace(linkNode.InnerText))
                    continue;

                ChapterLink link = new ChapterLink
                {
                    Name = linkNode.InnerText,
                    Url = linkNode.Attributes["href"].Value,
                    Unknown = true
                };

                if (link.Name.IndexOf("chap", StringComparison.CurrentCultureIgnoreCase) >= 0)
                    link.Unknown = false;

                links.Add(link);
            }

            return links.ToArray();
        }

        public override async Task<List<WebNovelChapter>> GetChaptersAsync(string baseUrl, int delayPer, IProgress<string> progress)
        {
            ChapterLink[] links = await GetLinks(baseUrl);
            var chapters = new List<WebNovelChapter>();

            int ctr = 1;
            foreach (ChapterLink link in links)
            {
                try
                {
                    WebNovelChapter chapter = await GetChapterAsync(link.Url);
                    chapter.ChapterId = ctr;

                    chapters.Add(chapter);

                    progress.Report(string.Format("{0} has been processed ({1})", link.Name, ctr));
                }
                catch (Exception ex)
                {
                    progress.Report(string.Format("Error processing {0}", link.Name));
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
                HtmlNode ifNode = articleNode.SelectSingleNode(".//iframe");

                if (ifNode != null)
                {
                    string ifUrl = ifNode.Attributes["src"].Value;

                    content = await GetWebPage(ifUrl);
                }
                else
                {
                    content = articleNode.InnerHtml;
                }
            }
            else if (pageNode != null)
            {
                HtmlNode titleNode = pageNode.SelectSingleNode(".//*[contains(@class, 'title')]");

                if (titleNode == null)
                    titleNode = pageNode.SelectSingleNode(".//*[contains(@class, 'entry-title')]");

                if (titleNode != null)
                    content = titleNode.OuterHtml;
            }
            else if (postNode != null)
            {
                HtmlNode titleNode = postNode.SelectSingleNode(".//*[contains(@class, 'post-title')]");

                if (titleNode == null)
                    titleNode = postNode.SelectSingleNode(".//*[contains(@class, 'entry-title')]");

                if (titleNode != null)
                    content = titleNode.OuterHtml;
            }
            else if (contentNode != null)
            {
                HtmlNode headLineNode = contentNode.SelectSingleNode(".//*[contains(@class, 'entry-headline')]");

                if (headLineNode != null)
                    content = headLineNode.OuterHtml;
            }

            if (articleNode == null)
            {
                HtmlNode entryNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'postbody')]");

                if (entryNode == null)
                    entryNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'entry-content')]");

                if (entryNode == null)
                    entryNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'post-content')]");

                if (entryNode == null)
                    entryNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'post-entry')]");

                if (entryNode != null)
                {
                    HtmlNode ifNode = entryNode.SelectSingleNode(".//iframe");

                    if (ifNode != null)
                    {
                        content = ifNode.InnerHtml;
                    }
                    else
                    {
                        var paraNodes = entryNode.SelectNodes(".//p|.//h1|.//h2|.//h3");

                        if (paraNodes != null)
                            content += string.Join(string.Empty,
                                    paraNodes.SelectMany(p => p.OuterHtml));
                    }
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
