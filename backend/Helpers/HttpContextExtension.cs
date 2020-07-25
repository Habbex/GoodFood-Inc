
using System.Linq;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace backend.Helpers
{
    public static class HttpContextExtension
    {
        public static int GetUserLoginId(this HttpContext context)
        {  
            var userLogin= (UserLogin)context.Items["UserLogin"];
            return userLogin.Id;
        }

         public static string GetUserLoginRole(this HttpContext context)
        {  
            var userLogin= (UserLogin)context.Items["UserLogin"];
            return userLogin.Role;
        }
    }
}