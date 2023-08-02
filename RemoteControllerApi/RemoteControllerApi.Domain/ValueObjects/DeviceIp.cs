using Domain.Primitives;
using Domain.Shared;

namespace RemoteControllerApi.Domain.ValueObjects;

public sealed class DeviceIp : ValueObject
{
    private DeviceIp(string value)
    {
        Value = value;
    }

    public string Value { get; set; }

    public static Result<DeviceIp> Create(string deviceIp)
    {
        if (string.IsNullOrWhiteSpace(deviceIp))
        {
            return Result.Failure<DeviceIp>(new Error(
                "DeviceIp.Empty",
                "Device ip is empty."));
        }

        return new DeviceIp(deviceIp);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static implicit operator string(DeviceIp deviceIp) =>
        deviceIp.Value;
}
