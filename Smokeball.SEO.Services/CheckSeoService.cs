using System.Web;

namespace Smokeball.SEO.Services;

public class CheckSeoService(HttpClient httpClient) : ICheckSeoService
{
    private readonly HttpClient _httpClient = httpClient;

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
                    Error = "Empty response from the server"
                };
            }

            return new SeoResult()
            {
                Success = true,
                Positions = DiscoverRequiredUrl(html, urlToFind)
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

    private static string DiscoverRequiredUrl(string html, string urlToCheck)
    {
        var linksInSearch = Helpers.GetSearchResultsLinks(html);

        if (linksInSearch.Count == 0)
            return "Search provided no results";

        string foundPositions = "";

        for (int i = 0; i < linksInSearch.Count; i++)
        {
            if (linksInSearch[i].Contains(urlToCheck))
            { 
                foundPositions += $"{i} "; 
            }
        }

        if (string.IsNullOrEmpty(foundPositions))
        {
            return "URL not found in search results";
        }
        return foundPositions;
    }

    private HttpResponseMessage QuerySearchEngine(string searchEngineUri, string keywords, int limit)
    {
        var query = $"{searchEngineUri}search?num={limit}&{HttpUtility.UrlEncode(keywords)}";
        return _httpClient.GetAsync(query).Result;
    }
}
