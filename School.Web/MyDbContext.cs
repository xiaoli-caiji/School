using Microsoft.EntityFrameworkCore;
using SchoolCore.Entities;
using SchoolEntityConfiguration;

namespace SchoolCore
{
    public class MyDbContext : BaseDbContext
    {
        public MyDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        { }

        //public override void OnConfiguring
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
