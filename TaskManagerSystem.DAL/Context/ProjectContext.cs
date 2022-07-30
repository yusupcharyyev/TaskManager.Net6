using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Models.Entities.Concrete;
using TaskManagerSystem.Models.EntityTypeConfigurations.Concrete;

namespace TaskManagerSystem.DAL.Context
{
    public class ProjectContext : IdentityDbContext
    {
        public ProjectContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ManagerUser> ManagerUsers { get; set; }
        public DbSet<TaskDescription> TaskDescriptions { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<User> Usera { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CompanyMap());
            builder.ApplyConfiguration(new ManagerUserMap());
            builder.ApplyConfiguration(new TaskDescriptionMap());
            builder.ApplyConfiguration(new TasksMap());
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new UserTaskMap());
            builder.ApplyConfiguration(new IdentityRoleMap());
            base.OnModelCreating(builder);
        }
    }
}
