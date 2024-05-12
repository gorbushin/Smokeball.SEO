using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smokeball.SEO.Services;

public interface ICheckSeoService
{
    public SeoResult CheckUrlSeo(string searchEngineUrl, string keywords, int limit, string urlToFind);
}
