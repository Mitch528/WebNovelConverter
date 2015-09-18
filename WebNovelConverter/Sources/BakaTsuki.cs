using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebNovelConverter.Sources
{
    public class BakaTsuki : WebNovelSource
    {
        public override string BaseUrl => "https://www.baka-tsuki.org";

        public static readonly string[] PossibleChapterNameParts =
        {
            "illustrations",
            "preface",
            "glossary",
            "prologue",
            "introduction",
            "chapter",
            "afterword",
            "epilogue",
            "interlude"
        };

        public override async Task<ChapterLink[]> GetLinks(string baseUrl)
        {
            string baseContent = await GetWebPage(baseUrl);

            HtmlDocument baseDoc = new HtmlDocument();
            baseDoc.LoadHtml(baseContent);

            HtmlNode contentNode = baseDoc.GetElementbyId("mw-content-text");

            if (contentNode == null)
                return null;

            var possibleChapters = contentNode.SelectNodes("//ul/li/a");

            if (possibleChapters == null)
                return new ChapterLink[0];

            var links = new List<ChapterLink>();

            foreach (HtmlNode possibleChapter in possibleChapters)
            {
                string chTitle = WebUtility.HtmlDecode(possibleChapter.InnerText);
                string chLink = possibleChapter.Attributes["href"].Value;
                chLink = new Uri(new Uri(BaseUrl), chLink).AbsoluteUri;

                ChapterLink link = new ChapterLink
                {
                    Name = chTitle,
                    Url = chLink,
                    Unknown = true
                };

                if (PossibleChapterNameParts.Any(p => chTitle.IndexOf(p, StringComparison.CurrentCultureIgnoreCase) >= 0))
                    link.Unknown = false;

                links.Add(link);
            }

            return links.ToArray();
        }


        public override Task<string> GetNovelCover(string baseUrl)
        {
            return Task.FromResult(string.Empty);
        }

        public override async Task<WebNovelChapter> GetChapterAsync(ChapterLink link)
        {
            string baseContent = await GetWebPage(link.Url);

            HtmlDocument baseDoc = new HtmlDocument();
            baseDoc.LoadHtml(baseContent);

            HtmlNode contentNode = baseDoc.GetElementbyId("mw-content-text");

            if (contentNode == null)
                return null;

            baseDoc.GetElementbyId("toc")?.Remove();

            foreach (HtmlNode linkNode in contentNode.SelectNodes(".//a"))
            {
                linkNode.SetAttributeValue("href", GetAbsoluteUrl(BaseUrl, WebUtility.HtmlDecode(linkNode.Attributes["href"].Value)));

                HtmlNode imgNode = linkNode.SelectSingleNode("img");

                if (imgNode != null)
                {
                    foreach (var attrib in imgNode.Attributes.Where(p => p.Name != "width" && p.Name != "height").ToList())
                        attrib.Remove();

                    string linkImgUrl = linkNode.Attributes["href"].Value;
                    string imgPageContent = await GetWebPage(linkImgUrl);

                    HtmlDocument imageDoc = new HtmlDocument();
                    imageDoc.LoadHtml(imgPageContent);

                    HtmlNode fullImageNode = imageDoc.DocumentNode.SelectSingleNode("//div[@class='fullMedia']/a");

                    if (fullImageNode == null)
                        continue;

                    string imageLink = fullImageNode.Attributes["href"].Value;

                    imgNode.SetAttributeValue("src", GetAbsoluteUrl(BaseUrl, imageLink));
                }
            }

            return new WebNovelChapter
            {
                Url = link.Url,
                Content = contentNode.InnerHtml
            };
        }

        private static string GetAbsoluteUrl(string baseUrl, string url)
        {
            Uri uri = new Uri(url, UriKind.RelativeOrAbsolute);

            if (!uri.IsAbsoluteUri)
                uri = new Uri(new Uri(baseUrl), uri);

            return uri.ToString();
        }
    }
}
