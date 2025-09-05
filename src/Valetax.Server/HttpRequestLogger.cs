namespace Valetax.Server;

public class HttpRequestLogger
{
    private readonly ILogger<HttpRequestLogger> _logger;
    private readonly HttpContext _httpContext;

    public HttpRequestLogger(ILogger<HttpRequestLogger> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContext = httpContextAccessor.HttpContext
            ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public void LogTrace()
    {
        var request = _httpContext.Request;

        var requestBody = _httpContext.Items["RequestBody"] as string
            ?? "No body content";

        var queryParams = FormatQueryParameters(request.Query);
        
        _logger.LogTrace($"Method: {request.Method}, " +
                         $"Path: {request.Path}, " +
                         $"Query Parameters: {queryParams}, " +
                         $"Body: {requestBody}");
    }

    private static string FormatQueryParameters(IQueryCollection query)
    {
        return string.Join(", ", query.Select(q => $"{q.Key}: {string.Join(";", q.Value)}"));
    }
}
