using Calabonga.Microservices.Core.Exceptions;
using Logunov.Facts.Web.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logunov.Facts.Web.Data
{
    public static class DataInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            var isExist = context!.GetService<IDatabaseCreator>() is RelationalDatabaseCreator databaseCreator &&
                await databaseCreator.ExistsAsync();

            if (isExist)
            {
                return;
            }
            
            await context.Database.MigrateAsync();

            //create roles
            //1.vreate AppData.cs
            //2.

            var roles = AppData.Roles.ToArray();
            var roleStore = new RoleStore<IdentityRole>(context);
            foreach (var role in roles)
            {
                if (!context.Roles.Any(x => x.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole(role)
                        { NormalizedName = role.ToUpper() }
                    );
                }
            }

            const string userName = "777koc@gmail.com";

            if(context.Users.Any(x => x.Email == userName))
            {
                return;
            }

            var newUser = new IdentityUser
            {
                Email = userName,
                EmailConfirmed = true,
                NormalizedEmail = userName.ToUpper(),
                PhoneNumber = "+380637245297",
                UserName = userName,
                PhoneNumberConfirmed = true,
                NormalizedUserName = userName.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();

            newUser.PasswordHash = passwordHasher.HashPassword(newUser, "k123456");

            var userStore = new UserStore<IdentityUser>(context);

            var identityResult = await userStore.CreateAsync(newUser);

            if (!identityResult.Succeeded)
            {
                var message = string.Join(", ", identityResult.Errors.Select(x => $"{x.Code}: {x.Description}"));
                
                throw new MicroserviceDatabaseException(message);
            }

            var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();

            var result = await userManager!.AddToRolesAsync(newUser, roles);

            /*foreach (var role in roles)
            {
                var result = await userManager!.AddToRoleAsync(newUser, role);
                if (!result.Succeeded)
                {
                    var message = string.Join(", ", identityResult.Errors.Select(x => $"{x.Code}: {x.Description}"));

                    throw new MicroserviceDatabaseException(message);
                }
            }*/           

            await context.SaveChangesAsync();
        }
    }
}
