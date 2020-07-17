using Microsoft.AspNetCore.Identity;
using MU.Common.Services;
using MU.Identity.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using static MU.Common.Constants;

namespace MU.Identity.Data
{
    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public IdentityDataSeeder(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void SeedData()
        {
            if (this.roleManager.Roles.Any() ||
                this.userManager.Users.Any())
            {
                return;
            }

            Task
                .Run(async () =>
                {
                    // po tup kod skoro ne sum pisal.
                    // No me murzi da extendvam User/Role managerite, zashtoto samo v tqh imam access do respective Store-a;

                    var adminRole = new IdentityRole(AdministratorRoleName);
                    var normalUserRole = new IdentityRole(RegularUserRoleName);
                    var translatorRole = new IdentityRole(TranslatorRoleName);

                    await this.roleManager.CreateAsync(adminRole);
                    await this.roleManager.CreateAsync(normalUserRole);
                    await this.roleManager.CreateAsync(translatorRole);
                    

                    var adminUser = new User
                    {
                        UserName = "admin@admin.com",
                        Email = "admin@admin.com",
                        SecurityStamp = "RandomSecurityStamp"
                    };

                    await userManager.CreateAsync(adminUser, "adminpass123");

                    await userManager.AddToRoleAsync(adminUser, AdministratorRoleName);

                    var regularUser = new User
                    {
                        UserName = "user@user.com",
                        Email = "user@user.com",
                        SecurityStamp = "RandomSecurityStamp2"
                    };

                    await userManager.CreateAsync(regularUser, "userpass123");

                    await userManager.AddToRoleAsync(regularUser, RegularUserRoleName);

                    var translatorUser = new User
                    {
                        UserName = "translator@translator.com",
                        Email = "translator@translator.com",
                        SecurityStamp = "RandomSecurityStamp3"
                    };

                    await userManager.CreateAsync(translatorUser, "translatorpass123");

                    await userManager.AddToRoleAsync(translatorUser, TranslatorRoleName);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
