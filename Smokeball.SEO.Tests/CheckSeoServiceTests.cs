using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Smokeball.SEO.Services;
using Moq;
using Moq.Protected;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Smokeball.SEO.Tests;

[TestClass]
public class CheckSeoServiceTests
{
    private readonly ICheckSeoService mockService;

    public CheckSeoServiceTests()
    {
        mockService = MockCheckSeoService();
    }

    [TestMethod]
    public void CheckSeo_Success()
    {
        var searchEngineUrl = "https://www.google.com.au/";
        var keywords = "conveyancing software";
        var limit = 100;
        var urlToFind = "www.google.com.au";

        var result = mockService.CheckUrlSeo(searchEngineUrl, keywords, limit, urlToFind);
        result.Success.Should().BeTrue();
        result.Count.Should().BeGreaterThan(0);
    }

    [TestMethod]
    public void CheckSeo_Fail()
    {
        var searchEngineUrl = "https://www/";
        var keywords = "conveyancing software";
        var limit = 100;
        var urlToFind = "www.google.com.au";

        var result = mockService.CheckUrlSeo(searchEngineUrl, keywords, limit, urlToFind);
        result.Success.Should().BeFalse();
    }

    private static CheckSeoService MockCheckSeoService()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        var fileContent = (File.ReadAllBytes("SearchPageExample.html"));
        var stream = new MemoryStream(fileContent);
        var response = new HttpResponseMessage();

        handlerMock
            .Protected()
            .Setup<HttpResponseMessage>(
                "Send",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .Returns(response)
            .Verifiable();

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://www.google.com.au/")
        };

        return new CheckSeoService(httpClient);
    }
}