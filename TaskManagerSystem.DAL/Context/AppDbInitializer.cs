using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Models.Entities.Concrete;

namespace TaskManagerSystem.DAL.Context
{
    public class AppDbInitializer
    {
        public static string guid = Guid.NewGuid().ToString();
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync("Yonetici")) ;
                await roleManager.CreateAsync(new IdentityRole("Yonetici"));
                if (!await roleManager.RoleExistsAsync("Admin"))
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                if (!await roleManager.RoleExistsAsync("Personel"))
                    await roleManager.CreateAsync(new IdentityRole("Personel"));

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string adminUserEmail = "yusupcharyyevv@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    User newAdminUser = new User()
                    {
                        Id = guid,
                        Email = adminUserEmail,
                        UserName = "yusupcharyyev",
                        FirstName = "Yusup",
                        LastName = "Charyyev",
                        Statu = Models.Enums.Statu.Active,
                        PasswordHash = "Yusup1996.",
                        CompanyID= new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7")
                    };

                    await userManager.CreateAsync(newAdminUser, "Yusup1996.");
                    await userManager.AddToRoleAsync(newAdminUser, "Admin");
                }

            }
        }
    }
}
