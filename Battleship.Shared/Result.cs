namespace Battleship.Shared;

public struct Result<TSuccess, TError>
{
    public TSuccess? Data { get; private set; }

    public TError? Error { get; private set; }

    public bool IsSuccess => Data is not null;

    public bool IsFailure => Error is not null && Data is null;

    private Result(TSuccess successPayload)
    {
        Data = successPayload;
        Error = default;
    }

    private Result(TError failurePayload)
    {
        Error = failurePayload;
        Data = default;
    }

    public static Result<TSuccess, TError> Success(TSuccess successPayload) =>
        new(successPayload);

    public static Result<TSuccess, TError> Failure(TError failurePayload) =>
        new(failurePayload);

    public static implicit operator TSuccess(Result<TSuccess, TError> param) =>
        (param.IsSuccess
            ? param.Data
            : throw new InvalidOperationException("Invalid state of the result. Cast isn't possible."))!;
}