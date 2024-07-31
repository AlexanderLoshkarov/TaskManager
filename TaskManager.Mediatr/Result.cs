namespace TaskManager.Mediatr
{
  public class Result<T>
  {
    protected internal Result(bool isSuccess, Error error, T value)
    {
      if (isSuccess && error != Error.None ||
          !isSuccess && error == Error.None)
      {
        throw new ArgumentException("Invalid error", nameof(error));
      }

      IsSuccess = isSuccess;
      Error = error;
      Value = value;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public T Value { get; }
    public static Result<T> Success(T value) => new ResultSuccess<T>(true, Error.None, value);
    public static Result<T> Failure(Error error) => new ResultFailure<T>(false, error);
  }

  public class ResultSuccess<T> : Result<T>
  {
    protected internal ResultSuccess(bool isSuccess, Error error, T value)
      : base(isSuccess, error, value)
    {
    }
  }

  public class ResultFailure<T> : Result<T>
  {
    protected internal ResultFailure(bool isSuccess, Error error)
      : base(isSuccess, error, default)
    {
    }
  }
}
