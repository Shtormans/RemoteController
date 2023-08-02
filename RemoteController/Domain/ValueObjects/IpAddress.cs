using Domain.Primitives;
using Domain.Shared;
using System.Text.RegularExpressions;

namespace RemoteController.Domain.ValueObjects;

public class IpAddress : ValueObject
{
    private IpAddress(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<IpAddress> Create(string ipAddress)
    {
        string emailValidation = @"^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?):[1-9]\d*$";
        var regex = new Regex(emailValidation);

        if (!regex.IsMatch(ipAddress))
        {
            return Result.Failure<IpAddress>(new Error(
                "IpAddress.WrongValue",
                "Ip address has the wrong value."));
        }

        return new IpAddress(ipAddress);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
