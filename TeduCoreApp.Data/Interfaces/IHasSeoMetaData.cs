using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Data.Interfaces
{
    interface IHasSeoMetaData
    {
        string SeoPageTitle { get; set; }
        string SeoAlias { get; set; }
        string SeoKeywords { get; set; }
        string SeoDescription { get; set; }
    }
}
