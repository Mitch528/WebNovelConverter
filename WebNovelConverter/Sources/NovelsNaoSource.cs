using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Extensions;

namespace WebNovelConverter.Sources
{
    public class NovelsNaoSource : WordPressSource
    {
        public override string BaseUrl => "http://novelsnao.com";

        private static readonly string[] _postClasses =
        {
            "CommonWhiteTypeOne",
            "CommonWhiteBg"
        };

        private static readonly string[] _pageClasses =
        {
            "CommonWhiteTypeOne",
            "CommonWhiteBg"
        };

        private static readonly string[] LinksToRemove =
        {
            "Next Chapter »",
            "« Previous Chapter",
            "Table of Contents"
        };

        public NovelsNaoSource() : base("NovelsNao")
        {
            PostClasses.AddRange(_postClasses);
            PageClasses.AddRange(_pageClasses);
        }

        protected override void RemoveNavigation(IElement element)
        {
            base.RemoveBloat(element);

            var toRemove = (from e in element.Descendents<IElement>()
                            where e.LocalName == "a"
                            where LinksToRemove.Any(x => x == e.Text().Trim())
                            select e).ToList();

            foreach (IElement e in toRemove)
            {
                e.Remove();
            }
        }
    }
}
