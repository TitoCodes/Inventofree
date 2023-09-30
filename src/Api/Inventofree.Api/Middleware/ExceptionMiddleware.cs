using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Inventofree.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(exception: ex, message:"An error occurred: {ErrorMessage}", ex.Message);
            await ReturnErrorResponse(context, errorMessage:ex.Message);
        }
    }

    private static async Task ReturnErrorResponse(HttpContext context, string errorMessage)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        var errorObject = new 
        {
            errors = new { errorMessage }
        };
            
        await context.Response.WriteAsJsonAsync(errorObject);
    }
}