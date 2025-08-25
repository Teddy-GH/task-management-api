using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using task_management_system.Models;

namespace task_management_system.Data
{
    public class TaskManagementDbContext : IdentityDbContext<User>
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options)
        {
            
        }


        public DbSet<TaskItem> TaskItems { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            foreach (var e in ChangeTracker.Entries<TaskItem>())
                if (e.State == EntityState.Modified) e.Entity.UpdatedAt = DateTime.UtcNow;
            return base.SaveChangesAsync(ct);
        }



    }
}
