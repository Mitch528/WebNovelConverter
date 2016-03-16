using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Extensions;

namespace WebNovelConverter.Extensions
{
    public static class ElementExtensions
    {
        public static IElement FirstWhereHasClass(
            this IElement rootElement,
            IList<string> classes,
            Func<IElement, bool> filter = default(Func<IElement, bool>))
        {
            return (from e in rootElement.Descendents<IElement>()
                    where e.HasAttribute("class")
                    where filter?.Invoke(e) ?? true
                    let names = e.GetAttribute("class").Split(' ')
                    from name in classes
                    where names.Any(p => p.Equals(name, StringComparison.OrdinalIgnoreCase))
                    orderby classes.IndexOf(name)
                    select e).FirstOrDefault();
        }

        public static IEnumerable<IElement> WhereHasClass(
            this IElement rootElement,
            IList<string> classes,
            Func<IElement, bool> filter = default(Func<IElement, bool>))
        {
            return (from e in rootElement.Descendents<IElement>()
                    where e.HasAttribute("class")
                    where filter?.Invoke(e) ?? true
                    let names = e.GetAttribute("class").Split(' ')
                    from name in classes
                    where names.Any(p => p.Equals(name, StringComparison.OrdinalIgnoreCase))
                    orderby classes.IndexOf(name)
                    select e).Distinct().ToList();
        }
    }
}
