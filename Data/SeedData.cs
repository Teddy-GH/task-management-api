using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using task_management_system.enums;
using task_management_system.Models;

namespace task_management_system.Data
{
    public static  class SeedData
    {
        public static async Task SeedAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<TaskManagementDbContext>();
            var users = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            await db.Database.MigrateAsync();

          
            var admin = await users.FindByEmailAsync("admin@gmail.com");

              if (admin == null)
            {
                admin = new User
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    Role = Role.ADMIN
                };

                var result = await users.CreateAsync(admin, "P@ssw0rd123");
                if (result.Succeeded)
                {
                    

                    await users.AddToRoleAsync(admin, "ADMIN");
                }
            }

              // create a user
            
            var user = await users.FindByEmailAsync("user@yahoo.com");

            if (user == null)
            {
                user = new User
                {
                    UserName = "user",
                    Email = "user@yahoo.com",
                    Role = Role.USER
                };

                var result = await users.CreateAsync(user, "P@ssw0rd123");
                if (result.Succeeded)
                {
                    await users.AddToRoleAsync(user, "USER");
                }
            }


            // seed tasks

            if (!await db.TaskItems.AnyAsync())
            {
                db.TaskItems.AddRange(
                    new TaskItem { Title = "UI/UX", CreatorId = admin.Id, AssigneeId = user.Id, Status = Status.TODO },
                    new TaskItem { Title = "API skeleton", CreatorId = admin.Id, AssigneeId = admin.Id, Status = Status.IN_PROGRESS },
                    new TaskItem { Title = "Login page", CreatorId = admin.Id, AssigneeId = user.Id, Status = Status.DONE },
                    new TaskItem { Title = "Board UI", CreatorId = admin.Id, AssigneeId = user.Id, Priority = TaskPriority.HIGH }
                );

                await db.SaveChangesAsync();
            }
        }

    }
}
