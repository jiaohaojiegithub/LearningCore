using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningCore.Api.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IMyScopedService svc)
        {
            svc.MyProperty = 1000;
            if (svc.IsMyProperty())
                return _next(httpContext);
            else
                return httpContext.Response.WriteAsync($"{nameof(svc.MyProperty)}<1000");

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
