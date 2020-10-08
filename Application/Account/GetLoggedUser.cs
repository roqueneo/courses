using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Error;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account
{
    public class GetLoggedUserRequest : IRequest<UserModel>
    { }

    public class GetLoggedUserHandler : IRequestHandler<GetLoggedUserRequest, UserModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserSession _userSession;

        public GetLoggedUserHandler(UserManager<User> userManager, IJwtGenerator jwtGenerator, IUserSession userSession)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _userSession = userSession;
        }
        
        public async Task<UserModel> Handle(GetLoggedUserRequest request, CancellationToken cancellationToken)
        {
            string userName = _userSession.GetUserName();
            var loggedUser = await _userManager.FindByNameAsync(userName);
            if (loggedUser == null)
                throw new ErrorHandler(HttpStatusCode.NotFound, $"User with userName [{userName}] not found");

            string token = _jwtGenerator.CreateToken(loggedUser);
            return new UserModel(token, loggedUser.FullName, loggedUser.UserName, loggedUser.Email);
        }
    }
}