
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User : IdentityUser
    {
        public User()
        {}

        public User(string fullName, string email, string userName)
        {
            FullName = fullName;
            Email = email;
            UserName = userName;
        }

        public string FullName { get; set; }
    }
}