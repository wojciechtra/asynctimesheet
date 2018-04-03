using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.BLL.Models;

namespace Timesheet.DAL.EntityFramework
{
    public static class DbInitializer
    {
        private const string adminRoleName = "Admin";
        private const string userRoleName = "User";
                
        private const string adminEmail = "admin@timesheet.pl";
        private const string userEmail = "user1@timesheet.pl";
        private const string password = "qwerty";

        

        public static async Task<bool> Seed(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                if (!context.EntryType.Any())
                {
                    EntryType[] entryTypes = new EntryType[]
                    {
                    new EntryType
                    {
                        Name = "Office"
                    },
                    new EntryType
                    {
                        Name = "Remote"
                    },
                    new EntryType
                    {
                        Name = "Delegation"
                    },
                    new EntryType
                    {
                        Name = "Training"
                    },
                    new EntryType
                    {
                        Name = "Vacation"
                    }
                    };

                    context.EntryType.AddRange(entryTypes);
                    context.SaveChanges();
                }

                await SeedUsers(userManager);
                await SeedRoles(roleManager);
                await AssignUsersToRoles(userManager, roleManager);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

        private static async Task<bool> SeedUsers(UserManager<IdentityUser> userManager)
        {
            
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new IdentityUser
                {
                    Email = adminEmail,
                    UserName = "Admin"
                };
                var result = await userManager.CreateAsync(admin, password);
            }

            if (await userManager.FindByEmailAsync(userEmail) == null)
            {
                var user = new IdentityUser
                {
                    Email = userEmail,
                    UserName = "User1"
                };

                await userManager.CreateAsync(user, password);
            }

            return true;
        }

        private static async Task<bool> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(adminRoleName) == null)
            {
                var adminRole = new IdentityRole
                {
                    Name = adminRoleName
                };

                await roleManager.CreateAsync(adminRole);
            }

            if(await roleManager.FindByNameAsync(userRoleName)== null)
            {
                var userRole = new IdentityRole
                {
                    Name = userRoleName
                };

                await roleManager.CreateAsync(userRole);
            }

            return true;
        }

        private static async Task<bool> AssignUsersToRoles(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminAccount = await userManager.FindByEmailAsync(adminEmail);
            var adminRole = await roleManager.FindByNameAsync(adminRoleName);

            if (adminAccount != null && adminRole != null && !(await userManager.IsInRoleAsync(adminAccount, adminRoleName)))
            {
                await userManager.AddToRoleAsync(adminAccount, adminRoleName);
            }

            var userAccount = await userManager.FindByEmailAsync(userEmail);
            var userRole = await roleManager.FindByNameAsync(userRoleName);

            if (userAccount != null && userRole != null && !(await userManager.IsInRoleAsync(userAccount, userRoleName)))
            {
                await userManager.AddToRoleAsync(userAccount, userRoleName);
            }

            return true;
        }
    }
}
