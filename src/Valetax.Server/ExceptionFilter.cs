using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Valetax.Server;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;
    private readonly HttpRequestLogger _httpRequestLogger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger, HttpRequestLogger httpRequestLogger)
    {
        _logger = logger;
        _httpRequestLogger = httpRequestLogger;
    }

    public void OnException(ExceptionContext context)
    {
        _httpRequestLogger.LogTrace();
        
        _logger.LogError($"An exception occurred: {context.Exception}");

        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An internal server error occurred.";

        if (context.Exception is ValidationException validationException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = context.Exception.Message;
        }
        else if (context.Exception is KeyNotFoundException keyNotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
            message = keyNotFoundException.Message;
        }
        else if (context.Exception is ArgumentException argumentException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = argumentException.Message;
        }
        
        var response = new 
        {
            Message = message,
            Success = false
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = (int)statusCode
        };

        context.ExceptionHandled = true;
    }
}


