using System;
using System.Threading.Tasks;
using BankDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BankDemo.Core.Services
{
    public class CreateInitialUsersService
    {
        const string AdminEmail = "admintest@mail.ru";
        const string ModeratorEmail = "moderatortest@mail.ru";
        const string Password = "qwerty1234";
        static public async Task CreateUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var userAdmin = await userManager.Users.FirstOrDefaultAsync(u => u.Email == AdminEmail);
            if (userAdmin == null)
            {
                await userManager.CreateAsync(new ApplicationUser {UserName = AdminEmail, Email = AdminEmail}, Password);
                var admin = await userManager.Users.FirstOrDefaultAsync(u => u.Email == AdminEmail);
                await userManager.AddToRoleAsync(admin, "Administrator");
                await userManager.AddToRoleAsync(admin, "Moderator");
            }
            var userModerator = await userManager.Users.FirstOrDefaultAsync(u => u.Email == ModeratorEmail);
            if (userModerator == null)
            {
                await userManager.CreateAsync(new ApplicationUser {UserName = ModeratorEmail, Email = ModeratorEmail}, Password);
                var moderator = await userManager.Users.FirstOrDefaultAsync(u => u.Email == ModeratorEmail);
                await userManager.AddToRoleAsync(moderator, "Moderator");
            }
        }
    }
}