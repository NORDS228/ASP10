namespace API1.Services;

public enum ServiceError
{
    None,
    NotFound,
    Invalid,
    Conflict
}

public sealed record ServiceResult<T>(T? Value, ServiceError Error = ServiceError.None, string? Message = null)
{
    public bool Succeeded => Error == ServiceError.None;

    public static ServiceResult<T> Success(T value) => new(value);
    public static ServiceResult<T> NotFound(string message = "Resource was not found.") =>
        new(default, ServiceError.NotFound, message);
    public static ServiceResult<T> Invalid(string message) =>
        new(default, ServiceError.Invalid, message);
    public static ServiceResult<T> Conflict(string message) =>
        new(default, ServiceError.Conflict, message);
}

public sealed record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount);
public sealed record CreatedResource<T>(int Id, T Value);
