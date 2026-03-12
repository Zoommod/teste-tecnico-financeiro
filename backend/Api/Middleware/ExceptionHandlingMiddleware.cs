using System;
using System.Net;
using System.Text.Json;
using Domain.Exceptions;

namespace Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Ocorreu uma exceção: {Message}", exception.Message);

        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new ErrorResponse();

        switch (exception)
        {
            case EntityNotFoundException notFoundEx:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse = new ErrorResponse
                {
                    StatusCode = response.StatusCode,
                    Message = notFoundEx.Message,
                    Details = new Dictionary<string, object>
                    {
                        { "entityName", notFoundEx.EntityName },
                        { "entityId", notFoundEx.EntityId }
                    }
                };
                break;

            case BusinessRuleException businessEx:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse = new ErrorResponse
                {
                    StatusCode = response.StatusCode,
                    Message = businessEx.Message,
                    Details = new Dictionary<string, object>
                    {
                        { "ruleName", businessEx.RuleName }
                    }
                };
                break;

            case ArgumentException argEx:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse = new ErrorResponse
                {
                    StatusCode = response.StatusCode,
                    Message = argEx.Message,
                    Details = new Dictionary<string, object>
                    {
                        { "parameterName", argEx.ParamName ?? "unknown" }
                    }
                };
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse = new ErrorResponse
                {
                    StatusCode = response.StatusCode,
                    Message = "Ocorreu um erro interno no servidor.",
                    Details = new Dictionary<string, object>
                    {
                        { "error", exception.Message }
                    }
                };
                break;
        }

        var result = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await response.WriteAsync(result);
    }
}

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, object>? Details { get; set; }
}
