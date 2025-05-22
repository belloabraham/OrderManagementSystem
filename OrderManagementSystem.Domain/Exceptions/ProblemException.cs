using System.Net;

namespace OrderManagementSystem.Domain.Exceptions;

[Serializable]
public class ProblemException(string errorMessage, int statusCode, string title) : Exception(errorMessage)
{
    public string ErrorMessage { get; set; } = errorMessage;
    public string Title { get; set; } = title;
    public int StatusCode { get; set; } = statusCode;
}