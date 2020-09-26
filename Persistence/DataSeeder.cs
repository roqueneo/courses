using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class DataSeeder
    {
        public static async Task InitUsers(CoursesDbContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                User admin = new User{ FullName = "Administrador", UserName = "admin", Email = "roqueneo@gmail.com" };
                await userManager.CreateAsync(admin, "Sesamo.1");
            }
        }
    }
}