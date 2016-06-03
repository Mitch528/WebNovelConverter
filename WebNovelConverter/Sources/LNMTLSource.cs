using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using WebNovelConverter.Sources.Models;
using AngleSharp.Extensions;
using WebNovelConverter.Extensions;

namespace WebNovelConverter.Sources
{
    public class LNMTLSource : WebNovelSource
    {
        public override string BaseUrl => "http://lnmtl.com";

        private static readonly List<string> ChapterTitleClasses = new List<string>
        {
            "chapter-title"
        };

        private static readonly List<string> ChapterClasses = new List<string>
        {
            "chapter-body"
        };

        private static readonly List<string> ChapterContentClasses = new List<string>
        {
            "translated"
        };

        public LNMTLSource() : base("LNMTL")
        {
        }

        public override async Task<WebNovelChapter> GetChapterAsync(ChapterLink link, ChapterRetrievalOptions options = default(ChapterRetrievalOptions),
            CancellationToken token = default(CancellationToken))
        {
            string content = await GetWebPageAsync(link.Url, token);

            IHtmlDocument doc = await Parser.ParseAsync(content, token);

            IElement titleElement = doc.DocumentElement.FirstWhereHasClass(ChapterTitleClasses);
            IElement chapterElement = doc.DocumentElement.FirstWhereHasClass(ChapterClasses);

            var chContentElements = chapterElement.WhereHasClass(ChapterContentClasses, element => element.LocalName == "sentence");

            string contents = string.Join("<br/><br/>", chContentElements.Select(p => p.InnerHtml));
            string nextChapter = doc.QuerySelector("ul.pager > li.next > a")?.GetAttribute("href");

            return new WebNovelChapter
            {
                ChapterName = titleElement?.TextContent,
                Content = contents,
                NextChapterUrl = nextChapter
            };
        }
    }
}
