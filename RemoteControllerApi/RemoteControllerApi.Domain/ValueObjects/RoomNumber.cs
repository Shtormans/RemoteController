using Domain.Primitives;
using Domain.Shared;

namespace RemoteControllerApi.Domain.ValueObjects
{
    public sealed class RoomNumber : ValueObject
    {
        public const int NumberLength = 8;

        private RoomNumber(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<RoomNumber> Create(string number)
        {
            if (number.Length != NumberLength)
            {
                return Result.Failure<RoomNumber>(new Error(
                    "RoomId.WrongValue",
                    "Room id has the wrong value."));
            }

            return new RoomNumber(number);
        }

        public static RoomNumber CreateRandom()
        {
            return RoomNumber.Create(Guid.NewGuid()
                .ToString("N")
                .Substring(0, NumberLength))
                .Value;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static implicit operator string(RoomNumber roomNumber) =>
            roomNumber.Value;
    }
}
