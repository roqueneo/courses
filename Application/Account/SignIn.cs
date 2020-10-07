using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Error;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Account
{
    public class SignInCommand : IRequest<UserModel>
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(r => r.FullName).NotEmpty();
            RuleFor(r => r.Email).NotEmpty();
            RuleFor(r => r.UserName).NotEmpty();
            RuleFor(r => r.Password).NotEmpty();
        }
    }

    public class SignInHandler : IRequestHandler<SignInCommand, UserModel>
    {
        private readonly CoursesDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IJwtGenerator _jwtGenerator;

        public SignInHandler(CoursesDbContext context, UserManager<User> userManager, IJwtGenerator jwtGenerator)
        {
            _context = context;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UserModel> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            bool userExists = await _context.Users.Where(u => u.Email == request.Email).AnyAsync();
            if (userExists)
                throw new ErrorHandler(HttpStatusCode.BadRequest, new { user = $"User with email [{request.Email}] already exists." } );

            userExists = await _context.Users.Where(u => u.UserName == request.UserName).AnyAsync();
            if (userExists)
                throw new ErrorHandler(HttpStatusCode.BadRequest, new { user = $"User with username [{request.UserName}] already exists." } );

            User user = new User(request.FullName, request.Email, request.UserName);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new Exception("User can't be created");

            string token = _jwtGenerator.CreateToken(user);
            return new UserModel(token, user.FullName, user.UserName, user.Email);
        }
    }
}