using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolCore.Entities;
using System;
using EntityConfigurationBase;

namespace SchoolEntityConfiguration
{
    public partial class UserCourseConfiguration : EntityTypeConfigurationBase<UserCourse, int>
    {
        //public override Type DbContextType => base.DbContextType;
        public override void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.HasIndex(u => new { u.UserId, u.CourseId }).IsUnique();
            builder.HasOne(u => u.User).WithMany(u => u.UserCourse).HasForeignKey(u => u.UserId);
            builder.HasOne(u => u.Course).WithMany(u => u.CourseMember).HasForeignKey(u => u.CourseId);

            EntityConfigurationAppend(builder);
        }      
        partial void EntityConfigurationAppend(EntityTypeBuilder<UserCourse> builder);
    }
}
