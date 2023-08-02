using Domain.Primitives;
using Domain.Shared;
using System.Text.RegularExpressions;

namespace RemoteControllerApi.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Failure<Email>(new Error(
                "Email.Empty",
                "Email is empty."));
        }

        if (!Regex.IsMatch(email, @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$"))
        {
            return Result.Failure<Email>(new Error(
                "Email.WrongFormat",
                "Email has a wrong format."));
        }

        return new Email(email);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string(Email email) =>
        email.Value;
}
