using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Error;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account
{
    public class LoginRequest : IRequest<User>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }

    public class LoginHandler : IRequestHandler<LoginRequest, User>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginHandler(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new ErrorHandler(HttpStatusCode.NotFound, new { user = $"User with email [{request.Email}] not found"});

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new ErrorHandler(HttpStatusCode.Unauthorized);
            
            return user;
        }
    }
}