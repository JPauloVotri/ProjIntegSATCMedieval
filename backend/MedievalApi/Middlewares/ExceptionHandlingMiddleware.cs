using System.Net;
using System.Text.Json;
using MedievalApi.Exceptions;

namespace MedievalApi.Middlewares;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DomainException ex)
        {
            await WriteDomainExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await WriteUnexpectedExceptionAsync(context, ex);
        }
    }

    private async Task WriteDomainExceptionAsync(HttpContext context, DomainException ex)
    {
        var (status, title) = ex switch
        {
            NotFoundException => (HttpStatusCode.NotFound, "Recurso não encontrado"),
            ConflictException => (HttpStatusCode.Conflict, "Conflito"),
            BusinessException => (HttpStatusCode.BadRequest, "Regra de negócio violada"),
            _ => (HttpStatusCode.BadRequest, "Erro de domínio")
        };

        logger.LogWarning(
            ex,
            "Exceção de domínio tratada: {Type} - {Message}",
            ex.GetType().Name,
            ex.Message);

        await WriteResponseAsync(context, status, title, ex.Message);
    }

    private async Task WriteUnexpectedExceptionAsync(HttpContext context, Exception ex)
    {
        logger.LogError(ex, "Erro inesperado");

        await WriteResponseAsync(
            context,
            HttpStatusCode.InternalServerError,
            "Erro interno",
            "Ocorreu um erro inesperado. Contate o suporte.");
    }

    private static async Task WriteResponseAsync(
        HttpContext context,
        HttpStatusCode status,
        string title,
        string detail)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)status;

        var problem = new
        {
            type = $"https://httpstatuses.com/{(int)status}",
            title,
            status = (int)status,
            detail,
            traceId = context.TraceIdentifier
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(problem, JsonOptions));
    }
}
