using Domain.Primitives;
using Domain.Shared;

namespace RemoteControllerApi.Domain.ValueObjects;

public sealed class Password : ValueObject
{
    private const int MaxLength = 10;

    private Password(string value) 
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Password> Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return Result.Failure<Password>(new Error(
                "Password.Empty",
                "Password is empty."));
        }

        if (password.Length > MaxLength)
        {
            return Result.Failure<Password>(new Error(
                "Password.TooLong",
                $"Password length must be greater than {MaxLength}."));
        }

        return new Password(password);
    }

    public static Password CreateRandom()
    {
        return Password.Create(Guid.NewGuid()
            .ToString("N")
            .Substring(0, 10))
            .Value;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string(Password password) =>
        password.Value;
}
