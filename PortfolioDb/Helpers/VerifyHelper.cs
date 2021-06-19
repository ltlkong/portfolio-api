using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioDb.Helpers
{
    public static class VerifyHelper
    {
        private const string myPassword = "abcdefg";

        public static bool Verify(HttpContext context)
        {
            if (context.Request.Query["password"].Equals(myPassword))
                return true;
            return false;
        }
    }
}
