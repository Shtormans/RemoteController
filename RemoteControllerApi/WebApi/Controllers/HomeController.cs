using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ApiController
    {
        public HomeController(ISender sender) 
            : base(sender)
        {
        }

        [HttpPost]
        [Route("TestConnection")]
        public async Task<IActionResult> TestConnection(CancellationToken cancellationToken)
        {
            var result = Result.Success();

            return Ok(result);
        }
    }
}
