using Domain.Shared;

namespace Domain.Errors;

public static class DomainErrors
{
    public static class Server
    {
        public static readonly Error ServerError = new Error(
            "Server.NoResponse",
            "Server is down at the moment.");
    }
}
