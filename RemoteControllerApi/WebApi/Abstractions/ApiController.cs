using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Abstractions;

public abstract class ApiController : Controller
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }
}
