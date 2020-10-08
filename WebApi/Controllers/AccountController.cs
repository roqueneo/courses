using System.Threading.Tasks;
using Application.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class AccountController : ApiControllerBase
    {
        public AccountController(IMediator mediator)
            : base(mediator)
        { }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> Login(LoginRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> SignIg(SignInCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("loggeduser")]
        public async Task<ActionResult<UserModel>> GetLoggedUser()
        {
            GetLoggedUserRequest request = new GetLoggedUserRequest();
            return await _mediator.Send(request);
        }
    }
}