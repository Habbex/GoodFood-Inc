
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace backend.Helpers
{
    public static class HttpContextExtension
    {
        public static string GetUserLoginId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return string.Empty;
            }

            return httpContext.User.Claims.Single(x => x.Type == "id").Value;
        }
    }
}