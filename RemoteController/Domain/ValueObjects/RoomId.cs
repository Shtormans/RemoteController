using Domain.Primitives;
using Domain.Shared;

namespace RemoteController.Domain.ValueObjects;

public class RoomId : ValueObject
{
    public const int IdLength = 8;

    private RoomId(string value) 
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<RoomId> Create(string value)
    {
        if (value.Length != IdLength)
        {
            return Result.Failure<RoomId>(new Error(
                "RoomId.WrongValue",
                "Room id has the wrong value."));
        }

        return new RoomId(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
