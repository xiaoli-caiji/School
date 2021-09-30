using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolCore.Entities;
using System;
using EntityConfigurationBase;

namespace SchoolEntityConfiguration
{
    public partial class CourseConfiguration : EntityTypeConfigurationBase<Course, int>
    {
        public override Type DbContextType => base.DbContextType;
        public override void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasMany(u => u.CourseMember);
            builder.HasMany(u => u.CourseAcademic);

            EntityConfigurationAppend(builder);
        }      
        partial void EntityConfigurationAppend(EntityTypeBuilder<Course> builder);
    }
}
