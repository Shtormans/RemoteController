using Domain.Primitives;
using Domain.Shared;

namespace RemoteController.Domain.ValueObjects;

public sealed class MonitorNumber : ValueObject
{
    private MonitorNumber(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<MonitorNumber> Create(int monitorNumber, int monitorCount)
    {
        if (monitorNumber < 0 || monitorNumber > monitorCount)
        {
            return Result.Failure<MonitorNumber>(new Error(
                "MonitorNumber.WrongValue",
                "Monitor number has the wrong value."));
        }

        return new MonitorNumber(monitorNumber);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}
