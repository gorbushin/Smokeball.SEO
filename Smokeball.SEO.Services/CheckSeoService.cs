using System.Web;

namespace Smokeball.SEO.Services;

public class CheckSeoService : ICheckSeoService
{
    private readonly HttpClient _httpClient;

    public CheckSeoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public SeoResult CheckUrlSeo(string searchEngineUri, string keywords, int limit, string urlToFind)
    {
        try
        {
            var html = QuerySearchEngine(searchEngineUri, keywords, limit);
            if (string.IsNullOrEmpty(html))
            {
                return new SeoResult()
                {
                    Error = "Empty response from server"
                };
            }

            return new SeoResult()
            {
                Success = true,
                Count = CountUrlInHtml(html, urlToFind)
            };
        }
        catch (Exception ex)
        {
            return new SeoResult()
            {
                Error = ex.Message
            };
        }
    }

    private static int CountUrlInHtml(string html, string urlToFind)
    {
        var linksOnThePage = Helpers.GetAnchorTags(html);
        var urlFound = linksOnThePage.Where(x => x.Contains(urlToFind)).Count();
        return urlFound;
    }

    private string? QuerySearchEngine(string searchEngineUri, string keywords, int limit)
    {
        var query = $"{searchEngineUri}search?num={limit}&{HttpUtility.UrlEncode(keywords)}";
        return Helpers.ScrapPage(_httpClient, query);
    }
}
