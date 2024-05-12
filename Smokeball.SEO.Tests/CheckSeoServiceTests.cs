using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Smokeball.SEO.Services;

namespace Smokeball.SEO.Tests
{
    [TestClass]
    public class CheckSeoServiceTests
    {
        private readonly ICheckSeoService checkSeoService;

        public CheckSeoServiceTests()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();
            services.AddTransient<ICheckSeoService, CheckSeoService>();
            var serviceProvider = services.BuildServiceProvider();
            checkSeoService = serviceProvider.GetRequiredService<ICheckSeoService>();
        }

        [TestMethod]
        public void TestCheckUrlSeoTrue()
        {
            var searchEngineUrl = "https://www.google.com.au";
            var keywords = "conveyancing software";
            var limit = 100;
            var urlToFind = "www.google.com.au";
            var result = checkSeoService.CheckUrlSeo(searchEngineUrl, keywords, limit, urlToFind);
            result.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void TestCheckUrlSeoFalse()
        {
            var searchEngineUrl = "https://wrong-url";
            var keywords = "conveyancing software";
            var limit = 100;
            var urlToFind = "www.smokeball.com.au";
            var result = checkSeoService.CheckUrlSeo(searchEngineUrl, keywords, limit, urlToFind);
            result.Should().NotBe(1);
        }
    }
}