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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "0e995991-e5ce-4b73-9001-a49c3f4fc36d", ConcurrencyStamp = "a71ab93a-6dfc-45ec-9066-125fb51de007", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "eff47076-d0eb-4922-80a8-6db7ff821954", ConcurrencyStamp = "14s3eec47-c1c1-4729-af69-23656139ffdd", Name = "User", NormalizedName = "USER" }
            );

        }
    }
}
