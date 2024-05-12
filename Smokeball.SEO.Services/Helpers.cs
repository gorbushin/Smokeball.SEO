using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.Net.Http.Headers;

namespace Smokeball.SEO.Services;

internal class Helpers
{
    internal static HttpContent? ScrapPage(HttpClient httpClient, string url)
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url)
        {
            Headers =
            {
                { HeaderNames.Accept, "text/html" },
                { HeaderNames.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36" }
            }
        };

        var httpResponseMessage = httpClient.Send(httpRequestMessage);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            return httpResponseMessage.Content;
        }

        return null;
    }

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
