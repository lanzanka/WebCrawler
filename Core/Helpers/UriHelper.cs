using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Helpers
{
    public class UriHelper : IUriHelper
    {
        public List<Uri> GetProperUris(Uri baseUri, List<string> urls)
        {
            var result = urls.Select(url => GetProperUri(baseUri, url)).ToList();
            result.RemoveAll(x => x == null || x == baseUri || x.IsFile);
            return result;
        }

        public Uri GetProperUri(Uri baseUri, string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                return new Uri(baseUri, url);
            }
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                return new Uri(url);
            }
            return null;
        }
    }
}

