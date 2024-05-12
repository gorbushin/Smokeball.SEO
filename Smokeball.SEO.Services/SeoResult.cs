using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smokeball.SEO.Services
{
    public class SeoResult
    {
        public string? Positions { get; set; }
        public bool Success { get; set; } = false;
        public string? Error { get; set; }
    }
}
