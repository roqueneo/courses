using System.Threading.Tasks;
using Application.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class AccountController : ApiControllerBase
    {
        public AccountController(IMediator mediator)
            : base(mediator)
        { }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login(LoginRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}