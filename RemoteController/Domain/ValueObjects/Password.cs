using Domain.Primitives;
using Domain.Shared;

namespace RemoteController.Domain.ValueObjects;

public sealed class Password : ValueObject
{
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

        return new Password(password);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
