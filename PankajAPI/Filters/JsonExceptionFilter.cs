using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using PankajAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PankajAPI.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        public JsonExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void OnException(ExceptionContext context)
        {
            var isDevelopement = _env.IsDevelopment();
            var error = new ApiError()
            {
                Message = isDevelopement ? context.Exception.Message : "A server error occured",
                Details = isDevelopement ? context.Exception.StackTrace : context.Exception.Message
            };

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}
