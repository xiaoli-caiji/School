using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolCore.Entities;
using System;
using EntityConfigurationBase;

namespace SchoolEntityConfiguration
{
    public partial class AcademicCourseConfiguration : EntityTypeConfigurationBase<AcademicCourse, int>
    {
        public override void Configure(EntityTypeBuilder<AcademicCourse> builder)
        {
            builder.HasIndex(u => new { u.AcademicId, u.CourseId }).IsUnique();
            builder.HasOne(u => u.Academic).WithMany(r => r.AcademicCourses).HasForeignKey(u => u.AcademicId);
            builder.HasOne(u => u.Course).WithMany(r => r.CourseAcademic).HasForeignKey(u => u.CourseId);

            EntityConfigurationAppend(builder);
        }      
        partial void EntityConfigurationAppend(EntityTypeBuilder<AcademicCourse> builder);
    }
}
