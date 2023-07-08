using Microsoft.AspNetCore.Http.Extensions;
using MiddlewareTurtorial.Interfaces;
using MiddlewareTurtorial.Models;

namespace MiddlewareTurtorial.Middlewares
{
    public class CustomAuthenticationMiddleware
    { // Only used to authenticate user when they try to login, 
        private readonly RequestDelegate _next;
        public CustomAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string referringUrl = context.Request.Headers["Referer"].ToString();
            string loginUrl = "https://localhost:7217/Account";

            bool isLoginRequest = referringUrl.Equals(loginUrl) && context.Request.HasFormContentType;

            if (isLoginRequest)
            {
                var collection = await context.Request.ReadFormAsync();
                Login login = new() { Username = collection["Username"]!, Password = collection["Password"]! };
                bool userExists = UserExists(login, context);

                if (!userExists)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.Redirect("/Home/Error");
                    return;
                }
                SetCookies(login, context);
            }
            await _next(context);
        }
        private bool UserExists(Login login, HttpContext context)
        {
            IUserService userService = (IUserService)context.RequestServices.GetService(typeof(IUserService))!;
            return userService.Exists(login);
        }
        private void SetCookies(Login login, HttpContext context)
        {
            IUserService userService = (IUserService)context.RequestServices.GetService(typeof(IUserService))!;
            IResponseCookies responseCookies = context.Response.Cookies;

            Dictionary<string, string> cookies = userService.GetCookies(login);
            cookies.ToList().ForEach(cookie => responseCookies.Append(cookie.Key, cookie.Value));
        }
    }
    public static class CustomAuthenticationMiddlewareExension { 
        public static IApplicationBuilder UseCustomAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthenticationMiddleware>();
        }
    }
}
