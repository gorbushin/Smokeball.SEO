using System.Web;

namespace Smokeball.SEO.Services;

public class CheckSeoService : ICheckSeoService
{
    private readonly HttpClient _httpClient;

    public CheckSeoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public SeoResult CheckUrlSeo(string searchEngineUri, string keywords, int limit, string urlToCheck)
    {
        try
        {
            var response = QuerySearchEngine(searchEngineUri, keywords, limit);
            return CheckResponseforSeo(response, urlToCheck);
        }
        catch (Exception ex)
        {
            return new SeoResult()
            {
                Error = ex.Message
            };
        }
    }

    private static SeoResult CheckResponseforSeo(HttpResponseMessage response, string urlToFind)
    {
        if (response.IsSuccessStatusCode)
        {
            var html = response.Content.ReadAsStringAsync().Result;

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
        else
        {
            return new SeoResult()
            {
                Error = $"Error code: {response.StatusCode}"
            };
        }
    }

    private static int CountUrlInHtml(string html, string urlToCheck)
    {
        var linksOnThePage = Helpers.GetAnchorTags(html);
        var foundItemsCount = linksOnThePage.Where(x => x.Contains(urlToCheck)).Count();
        return foundItemsCount;
    }

    private HttpResponseMessage QuerySearchEngine(string searchEngineUri, string keywords, int limit)
    {
        var query = $"{searchEngineUri}search?num={limit}&{HttpUtility.UrlEncode(keywords)}";
        return _httpClient.GetAsync(query).Result;
    }
}
