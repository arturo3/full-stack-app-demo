using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace full_stack_app_demo.Server.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var errorId = Guid.NewGuid().ToString();
            _logger.LogError(ex, "Unhandled exception. ErrorId: {ErrorId}", errorId);

            var problem = new ProblemDetails
            {
                Title = "An unexpected error occurred.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError,
                Instance = context.Request.Path
            };

            problem.Extensions["errorId"] = errorId;

            if (_environment.IsDevelopment())
            {
                problem.Extensions["stackTrace"] = ex.StackTrace ?? string.Empty;
            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
        }
    }
}
