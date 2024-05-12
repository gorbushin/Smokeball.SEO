using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.Net.Http.Headers;

namespace Smokeball.SEO.Services;

internal class Helpers
{
    internal static List<string> GetAnchorTags(string html)
    {
        List<string> hrefTags = [];

        Regex regxHref = new Regex(@"(?inx)
                <a \s [^>]*
                    href \s* = \s*
                        (?<q> ['""] )
                            (?<url> [^""]+ )
                        \k<q>
                [^>]* >");

        foreach (Match match in regxHref.Matches(html))
        {
            hrefTags.Add(match.Groups["url"].ToString());
        }

        return hrefTags;
    }
}
