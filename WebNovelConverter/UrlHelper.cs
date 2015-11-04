using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNovelConverter
{
    public static class UrlHelper
    {
        public static bool IsAbsoluteUrl(string url)
        {
            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result);
        }

        public static string ToAbsoluteUrl(string baseUrl, string relativeUrl)
        {
            if (IsAbsoluteUrl(relativeUrl))
                return relativeUrl;
            
            Uri result;
            if (Uri.TryCreate(new Uri(baseUrl), relativeUrl, out result))
                return result.AbsoluteUri;

            return null;
        }
    }
}
