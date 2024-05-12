using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smokeball.SEO;

public class AppSettings
{
    public string SearchEngineUri { get; set; } = "https://www.google.com.au";
    public int ResultLimit { get; set; } = 100;
    public string UrlToCheck { get; set; } = "smokeball.com.au";
    public string Keywords { get; set; } = "conveyancing software";
}
