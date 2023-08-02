using Domain.Primitives;
using Domain.Shared;

namespace RemoteControllerApi.Domain.ValueObjects;

public sealed class DeviceName : ValueObject
{
    private const int MaxLength = 30;

    private DeviceName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<DeviceName> Create(string deviceName)
    {
        if (string.IsNullOrWhiteSpace(deviceName))
        {
            return Result.Failure<DeviceName>(new Error(
                "DeviceName.Empty",
                "Device name is empty."));
        }

        if (deviceName.Length > MaxLength)
        {
            return Result.Failure<DeviceName>(new Error(
                "DeviceName.TooLong",
                $"Device name length must be greater than {MaxLength}."));
        }

        return new DeviceName(deviceName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string(DeviceName deviceName) => 
        deviceName.Value;
}
