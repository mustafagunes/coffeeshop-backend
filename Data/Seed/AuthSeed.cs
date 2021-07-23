using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model.Auth;
using Microsoft.AspNetCore.Identity;

namespace Data.Seed
{
    public class AuthSeed
    {
        /**
        Seed Data
        **/
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                // Create Roles
                var roles = new List<string>{
                    Role.Admin, Role.User
                };
                
                foreach (var currentRole in roles.Select(role => new IdentityRole
                {
                    Name = role
                }))
                {
                    await roleManager.CreateAsync(currentRole);
                }

                // Add Users
                var users = new List<string>{
                    "Ali", "Ahmet"
                };
                foreach (var currentUser in users.Select(user => new AppUser
                {
                    DisplayName = user,
                    Email = user + "@email.com",
                    UserName = user + "123",
                }))
                {
                    await userManager.CreateAsync(currentUser, "Pa$$w0rd123@");
                    await userManager.AddToRoleAsync(currentUser, Role.User);
                }

                // Admin
                var admin = new AppUser
                {
                    DisplayName = "mustafagunes",
                    Email = "gunes149" + "@gmail.com",
                    UserName = "mustafa" + "123",
                };
                await userManager.CreateAsync(admin, "Deneme1234*");
                await userManager.AddToRoleAsync(admin, Role.Admin);
            }
        }
    }
}