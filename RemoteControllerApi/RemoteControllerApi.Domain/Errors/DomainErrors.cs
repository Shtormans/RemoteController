using Domain.Shared;

namespace Domain.Errors;

public static class DomainErrors
{
    public static class Room
    {
        public static readonly Error IsNotOnline = new Error(
            "Room.IsNotOnline",
            $"User is not online.");

        public static readonly Error IsNotConnected = new Error(
            "Room.DeviceIsNotConnected",
            $"Device is not connected yet.");

        public static Error WrongRoomNumber(string roomNumber) => new Error(
            "Room.WrongNumber",
            $"Room with number '{roomNumber}' does not exist.");

        public static Error WrongPassword(string roomId) => new Error(
            "Room.WrongPassword",
            $"Wrong password for room with id '{roomId}'.");

        public static Error DeviceNameAlreadyExist(string email, string deviceName) => new Error(
            "Room.DeviceNameAlreadyExist",
            $"Room with name '{deviceName}' already exist in account 'email'.");
    }

    public static class User
    {
        public static Error AlreadyExist(string email) => new Error(
                "User.AlreadyExist",
                $"User with email '{email}' already exist.");

        public static Error WrongEmail(string email) => new Error(
            "User.WrongEmail",
            $"User with email '{email}' does not exist.");

        public static Error WrongPassword(string email) => new Error(
            "User.WrongPassword",
            $"Wrong password for user with email '{email}'.");
    }
}
