using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using School.Core.Common.Entities;
using School.Core.UserIndex.Entities;
using SchoolCore.Entities;
using System.Diagnostics.CodeAnalysis;

namespace SchoolCore
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        { }

        //public BaseDbContext([NotNull] DbContextOptions options) : base(options)
        //{
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Academic> Academics { get; set; }
        public DbSet<AClass> AClasses { get; set; }
        public DbSet<AcademicCourse> AcademicAndCourses { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }    
        public DbSet<ErrorType> ErrorTypes { get; set; }
        public DbSet<ReportCards> ReportCards { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsType> NewsTypes { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new UserConfiguration());
        //    modelBuilder.ApplyConfiguration(new UserCourseConfiguration());
        //    modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
        //    modelBuilder.ApplyConfiguration(new UserCourseConfiguration());
        //    modelBuilder.ApplyConfiguration(new RoleConfiguration());
        //    modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
        //    modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        //    modelBuilder.ApplyConfiguration(new AclassConfiguration());
        //    modelBuilder.ApplyConfiguration(new AcademicConfiguration());
        //    modelBuilder.ApplyConfiguration(new AcademicCourseConfiguration());
        //    modelBuilder.ApplyConfiguration(new CourseConfiguration());
        //    modelBuilder.ApplyConfiguration(new ReportCardsConfiguration());
        //    base.OnModelCreating(modelBuilder);
        //}
    }

}
