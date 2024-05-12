using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.Net.Http.Headers;

namespace Smokeball.SEO.Services;

internal class Helpers
{
    internal static List<string> GetSearchResultsLinks(string html)
    {
        List<string> hrefTags = [];

        var regxSearch = new Regex("<div class=\"[^\"]*?MjjYud[^\"]*?\">(.*?)<\\/div>");
        var regxHref = new Regex(@"href\s*=\s*(?:[""'](?<1>[^""']*)[""']|(?<1>[^>\s]+))");
        
        // Find only Search Result items. Skip the rest of the page content
        foreach (Match match in regxSearch.Matches(html))
        {
            // Take Result item DIV
            var div = match.Groups[0].ToString();
            // Take Href element inside this div
            var href = regxHref.Matches(div).FirstOrDefault();
            if (href != null)
            {
                var tag = href.Groups[1].ToString();
                hrefTags.Add(tag);
            }
        }
        return hrefTags;
    }
}
