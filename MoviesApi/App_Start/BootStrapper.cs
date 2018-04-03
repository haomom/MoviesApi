using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MoviesApi
{
    public class BootStrapper
    {
        public static void Run()
        {
            IoCConfig.Initialize(GlobalConfiguration.Configuration);
        }
    }
}