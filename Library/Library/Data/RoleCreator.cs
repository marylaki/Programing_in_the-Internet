using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Data
{
    public class RoleCreator
    {
        public static async Task Initialize(ApplicationDbContext context,
                              UserManager<IdentityUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            string role1 = "User";
            string role2 = "Librarian";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role1));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role2));
            }
            if (await userManager.FindByNameAsync("m1@mail.ru") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "m1@mail.ru",
                    Email = "m1@mail.ru",
                    PhoneNumber = "6041234567"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, "M1@mail.ru");
                    await userManager.AddToRoleAsync(user, role1);
                }
            }
            if (await userManager.FindByNameAsync("m2@mail.ru") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "m2@mail.ru",
                    Email = "m2@mail.ru",
                    PhoneNumber = "6041234567"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, "M2@mail.ru");
                    await userManager.AddToRoleAsync(user, role2);
                }
            }
        }
    }
}