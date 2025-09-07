using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Valetax.App.Exceptions;
using Valetax.App.Services;
using Valetax.Server.Api;

namespace Valetax.Server;

public class ExceptionFilter : IAsyncExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;
    private readonly HttpContext _httpContext;
    private readonly JournalService _journalService;

    public ExceptionFilter(ILogger<ExceptionFilter> logger,
        IHttpContextAccessor accessor,
        JournalService  journalService)
    {
        _logger = logger;
        _httpContext = accessor.HttpContext 
                       ?? throw new ArgumentNullException(nameof(IHttpContextAccessor));
        _journalService = journalService;
    }

    public async Task OnExceptionAsync(ExceptionContext context)
    {
        var request = _httpContext.Request;

        var requestBody = _httpContext.Items["RequestBody"] as string
                          ?? "No body content";

        var queryParams = FormatQueryParameters(request.Query);
        
        _logger.LogTrace($"Method: {request.Method}, " +
                         $"Path: {request.Path}, " +
                         $"Query Parameters: {queryParams}, " +
                         $"Body: {requestBody}");
        
        _logger.LogError($"An exception occurred: {context.Exception}");
        
        var eventId = await _journalService.Create(request.Method,
            request.Path, 
            queryParams, 
            context.Exception.StackTrace);
        
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An internal server error occurred.";

        if (context.Exception is ValidationException validationException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = validationException.Message;
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
        
        var response = new Response<Guid> 
        {
            Data = eventId,
            Message = message,
            Success = false
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = (int)statusCode
        };

        context.ExceptionHandled = true;
    }
    
    private static string FormatQueryParameters(IQueryCollection query)
    {
        return string.Join(", ", query.Select(q => $"{q.Key}: {string.Join(";", q.Value)}"));
    }
}


