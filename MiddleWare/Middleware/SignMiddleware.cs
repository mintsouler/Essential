using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWare.Middleware
{
    public class SignMiddleware
    {
        private readonly RequestDelegate _next;
        public SignMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)//通过反射，调用Invoke or InvokeAsync
        {
            IQueryCollection query = context.Request.Query;
            string str = "";
            foreach (var key in query.Keys)
            {
                if (key.Equals("sign"))
                    continue;
                str += key + query[key];
            }

            str += "keyidid";

            string sign = GetMd5(str);

            string requestSign = query["sign"];
            if (sign == requestSign)
            {
                await _next(context);
            }
            else
            {
                await context.Response.WriteAsync("request no valid!");
            }

        }


        public string GetMd5(string s)
        {
            using (MD5 provider = MD5.Create())
            {
                byte[] result = provider.ComputeHash(Encoding.UTF8.GetBytes(s));
                string strResult = BitConverter.ToString(result);
                return strResult.Replace("-", string.Empty).ToLower();
            }
        }
    }
}
