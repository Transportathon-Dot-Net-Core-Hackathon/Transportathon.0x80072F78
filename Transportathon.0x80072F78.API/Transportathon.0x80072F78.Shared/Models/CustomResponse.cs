using System.Text.Json.Serialization;
using System.Text.Json;
using Transportathon._0x80072F78.Shared.DTOs;

namespace Transportathon._0x80072F78.Shared.Models;

public record CustomResponse<T>
{
    [JsonPropertyName("data")] public T Data { get; set; }

    [JsonIgnore] public int StatusCode { get; set; }

    [JsonIgnore] public bool Succeeded { get; private set; }

    [JsonIgnore] public object[] Args { get; private set; }

    [JsonPropertyName("error")] public ErrorDTO Error { get; set; }

    public override string ToString()
    {
        return Succeeded ? "Succeeded" : $"Failed : {JsonSerializer.Serialize(Error)}";
    }

    #region Static Factory Methods

    public static CustomResponse<T> Success(int statusCode, T data)
    {
        return new CustomResponse<T> { Data = data, StatusCode = statusCode, Succeeded = true };
    }

    public static CustomResponse<T> Success(int statusCode)
    {
        return new CustomResponse<T> { StatusCode = statusCode, Succeeded = true };
    }

    public static CustomResponse<T> Fail(int statusCode, List<string> errors)
    {
        return new CustomResponse<T>
        {
            StatusCode = statusCode,
            Error = new ErrorDTO { Details = errors },
            Succeeded = false
        };
    }

    public static CustomResponse<T> Fail(int statusCode, List<string> errors, params object[] args)
    {
        return new CustomResponse<T>
        {
            StatusCode = statusCode,
            Error = new ErrorDTO { Details = errors },
            Succeeded = false,
            Args = args
        };
    }

    public static CustomResponse<T> Fail(int statusCode, string error)
    {
        return new CustomResponse<T>
        {
            StatusCode = statusCode,
            Error = new ErrorDTO { Details = new List<string> { error } },
            Succeeded = false
        };
    }

    public static CustomResponse<T> Fail(int statusCode, int errorCode, string error, params object[] args)
    {
        return new CustomResponse<T>
        {
            StatusCode = statusCode,
            Error = new ErrorDTO { Details = new List<string> { error }, ErrorCode = errorCode },
            Succeeded = false,
            Args = args
        };
    }

    public static CustomResponse<T> Fail(int statusCode, ErrorDTO error)
    {
        return new CustomResponse<T>
        {
            StatusCode = statusCode,
            Error = error,
            Succeeded = false
        };
    }

    public static CustomResponse<T> Fail(int statusCode, string error, int errorCode)
    {
        return new CustomResponse<T>
        {
            StatusCode = statusCode,
            Error = new ErrorDTO { Details = new List<string> { error }, ErrorCode = errorCode },
            Succeeded = false
        };
    }

    public static CustomResponse<T> Fail(int statusCode, int errorCode, List<string> errors)
    {
        return new CustomResponse<T>
        {
            StatusCode = statusCode,
            Error = new ErrorDTO { Details = errors, ErrorCode = errorCode },
            Succeeded = false
        };
    }

    public static CustomResponse<T> Fail(int statusCode, string error, int errorCode, string errorTitle)
    {
        return new CustomResponse<T>
        {
            StatusCode = statusCode,
            Error = new ErrorDTO { Details = new List<string> { error }, ErrorCode = errorCode, Title = errorTitle },
            Succeeded = false
        };
    }

    public static CustomResponse<T> Fail(int statusCode, string error, int errorCode, string errorTitle, string? stackTrace)
    {
        return new CustomResponse<T>
        {
            StatusCode = statusCode,
            Error = new ErrorDTO
            {
                Details = new List<string> { error },
                ErrorCode = errorCode,
                Title = errorTitle,
                StackTrace = stackTrace
            },
            Succeeded = false
        };
    }

    #endregion
}