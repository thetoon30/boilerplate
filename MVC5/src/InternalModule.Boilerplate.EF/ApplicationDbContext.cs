using InternalModule.Boilerplate.Core.DataContext;
using InternalModule.Boilerplate.Core.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalModeule.Boilerplate.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public ApplicationDbContext() : base("IMOContext")
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                 .ToTable("Users", "dbo").Property(p => p.Id).HasColumnName("UserId");

            modelBuilder.Entity<IdentityRole>()
                .ToTable("Roles", "dbo").Property(p => p.Id).HasColumnName("Id");

            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRoles", "dbo").Property(p => p.UserId).HasColumnName("UserId");

            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaims", "dbo").Property(p => p.UserId).HasColumnName("UserId");

            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("UserLogins", "dbo").Property(p => p.UserId).HasColumnName("UserId");
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
