using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Domain.Exceptions;

namespace OrderManagementSystem.API;

//TODO
public class GlobalExceptionHandler:IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<GlobalExceptionHandler> _logger;


    public GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var isServerError = exception is not ProblemException;
        var problemException = exception as ProblemException;
        
        var problemDetails = new ProblemDetails
        {
            Status = isServerError ? StatusCodes.Status500InternalServerError : problemException?.StatusCode,
            Detail = isServerError ? exception.Message : problemException?.Message,
            Title = isServerError ? "Internal Server Error" : problemException?.Title,
        };

        if (isServerError)
        {
            //TODO
            _logger.LogError(exception, exception.Message);
            
        }
        
        return await _problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }
}