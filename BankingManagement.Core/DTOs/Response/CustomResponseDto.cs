namespace BankingManagement.Core.DTOs.Response;

public class CustomResponseDto<T>
{
    public string? Message { get; set; }
    public T? Data { get; set; }
    public ResponseStatus Status { get; set; }
    public List<string>? Errors { get; set; }

    public static CustomResponseDto<T> Info(string message)
    {
        return new CustomResponseDto<T>
        {
            Message = message,
            Status = ResponseStatus.Info
        };
    }

    public static CustomResponseDto<T> Info(T data, string message)
    {
        return new CustomResponseDto<T>
        {
            Data = data,
            Message = message,
            Status = ResponseStatus.Info
        };
    }

    public static CustomResponseDto<T> Success(T data)
    {
        return new CustomResponseDto<T>
        {
            Data = data,
            Status = ResponseStatus.Success
        };
    }

    public static CustomResponseDto<T> Success(string message)
    {
        return new CustomResponseDto<T>
        {
            Message = message,
            Status = ResponseStatus.Success
        };
    }

    public static CustomResponseDto<T> Success(T data, string message)
    {
        return new CustomResponseDto<T>
        {
            Data = data,
            Message = message,
            Status = ResponseStatus.Success
        };
    }

    public static CustomResponseDto<T> Error(string error)
    {
        return new CustomResponseDto<T>
        {
            Errors = new List<string>() { error },
            Status = ResponseStatus.Error
        };
    }


    public static CustomResponseDto<T> Error(List<string> errors)
    {
        return new CustomResponseDto<T>
        {
            Errors = errors,
            Status = ResponseStatus.Error
        };
    }

    public static CustomResponseDto<T> Error(T data, string error)
    {
        return new CustomResponseDto<T>
        {
            Data = data,
            Errors = new List<string>() { error },
            Status = ResponseStatus.Error
        };
    }

    public static CustomResponseDto<T> Error(T data, List<string> errors)
    {
        return new CustomResponseDto<T>
        {
            Data = data,
            Errors = errors,
            Status = ResponseStatus.Error
        };
    }
}

public enum ResponseStatus
{
    Info,
    Success,
    Error
}