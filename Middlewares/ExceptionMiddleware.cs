using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using demo_tt.Responses;

namespace demo_tt.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var messages = GetMessages(ex, new());
                if (ex.StackTrace != null)
                {
                    messages?.Add(ex.StackTrace);
                }

              
                var exceptionResponse = new BaseResponse().Fail(messages.FirstOrDefault());
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var json = System.Text.Json.JsonSerializer.Serialize(exceptionResponse, options);
                await context.Response.WriteAsync(json);
            }
        }

        private static List<string>? GetMessages(Exception? ex, List<string> messages)
        {
            if (ex is null) return default;
            if (ex.InnerException is not null) GetMessages(ex.InnerException, messages);
            messages.Add(ex.Message);
            return messages;
        }
    }
}