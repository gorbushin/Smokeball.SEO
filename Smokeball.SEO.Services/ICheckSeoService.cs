using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smokeball.SEO.Services
{
    public interface ICheckSeoService
    {
        public string? QuerySearchEngine(string searchEngineUrl, string keywords, int limit);

        public int CountUrlInHtml(string html, string urlToFind);

        public int CheckUrlSeo(string searchEngineUrl, string keywords, int limit, string urlToFind);


    }
}
