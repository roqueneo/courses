using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Error;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account
{
    public class LoginRequest : IRequest<UserModel>
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

    public class LoginHandler : IRequestHandler<LoginRequest, UserModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UserModel> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new ErrorHandler(HttpStatusCode.NotFound, new { user = $"User with email [{request.Email}] not found"});

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new ErrorHandler(HttpStatusCode.Unauthorized);
            
            string token = _jwtGenerator.CreateToken(user);
            return new UserModel(token, user.FullName, user.UserName, user.Email);
        }
    }
}