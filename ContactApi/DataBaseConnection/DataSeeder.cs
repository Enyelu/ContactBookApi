using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseConnection
{
    public class DataSeeder
    {
        public static async Task SeedUserData(AppDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                await dbContext.Database.EnsureCreatedAsync();
                if (!dbContext.AppUsers.Any())
                {
                    List<string> roles = new List<string> { "Admin", "Regular" };
                    foreach (var role in roles)
                    {
                        await roleManager.CreateAsync(new IdentityRole { Name = role });
                    }
                    List<User> users = new List<User>
                {
                    new User
                    {
                        FirstName = "Chukwuebuka",
                        LastName = "Enyelu",
                        Email = "enyelu.joseph@gmail.com",
                        PhoneNumber = "07062926449",
                        UserName = "enyelu"
                    },
                    new User
                    {
                        FirstName = "Bimbo",
                        LastName = "Abimbola",
                        Email = "abim@gmail.com",
                        PhoneNumber = "07064543424",
                        UserName = "bimbo"
                    },
                    new User
                    {
                        FirstName = "Chibuike",
                        LastName = "Chibyk",
                        Email = "chibuike@gmail.com",
                        PhoneNumber = "08054348433",
                        UserName = "chibyke"
                    },
                    new User
                    {
                        FirstName = "Angelo",
                        LastName = "Angelo",
                        Email = "angelo@gmail.com",
                        PhoneNumber = "07064543424",
                        UserName = "SA"
                    },
                    new User
                    {
                        FirstName = "Raphael",
                        LastName = "Raph",
                        Email = "raph@gmail.com",
                        PhoneNumber = "08054348433",
                        UserName = "Raph"
                    },
                    new User
                    {
                        FirstName = "Michael",
                        LastName = "Mike",
                        Email = "mike@gmail.com",
                        PhoneNumber = "07064543424",
                        UserName = "Mikel"
                    },
                    new User
                    {
                        FirstName = "Ola",
                        LastName = "Kehinde",
                        Email = "kenny@gmail.com",
                        PhoneNumber = "08054348433",
                        UserName = "kenny"
                    },
                    new User
                    {
                        FirstName = "Patricia",
                        LastName = "Enyelu",
                        Email = "Parti@gmail.com",
                        PhoneNumber = "08036250680",
                        UserName = "MadamParis"
                    },
                    new User
                    {
                        FirstName = "Agnes",
                        LastName = "Ugochukwu",
                        Email = "agnes@gmail.com",
                        PhoneNumber = "08054348433",
                        UserName = "AgyBaby"
                    },
                    new User
                    {
                        FirstName = "Daniel",
                        LastName = "Mbata",
                        Email = "dan@gmail.com",
                        PhoneNumber = "07064543424",
                        UserName = "Danilo"
                    }
                };
                    foreach (var user in users)
                    {
                        await userManager.CreateAsync(user, "Password@123");
                        if (user == users[0])
                        {
                            await userManager.AddToRoleAsync(user, "Admin");
                        }
                        else
                            await userManager.AddToRoleAsync(user, "Regular");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AccessViolationException($"Error occur while accessing the database: {ex}");
            }
        }
    }
}
