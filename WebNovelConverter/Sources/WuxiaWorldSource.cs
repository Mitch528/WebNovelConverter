using System;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Extensions;

namespace WebNovelConverter.Sources
{
    public class WuxiaWorldSource : WordPressSource
    {
        public override string BaseUrl => "http://wuxiaworld.com/";

        private static readonly string[] LinksToRemove =
        {
            "Next Chapter",
            "Previous Chapter"
        };

        protected override void RemoveBloat(IElement element)
        {
            base.RemoveBloat(element);

            var toRemove = (from p in element.Descendents<IElement>()
                            where p.LocalName == "p"
                            from link in p.Descendents<IElement>()
                            where link.LocalName == "a"
                            where LinksToRemove.Any(x => x == link.Text().Trim())
                            select link).ToList();

            foreach (IElement e in toRemove)
            {
                e.Remove();
            }
        }
    }
}
