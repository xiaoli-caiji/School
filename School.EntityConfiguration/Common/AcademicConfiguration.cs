using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolCore.Entities;
using System;
using EntityConfigurationBase;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.EntityFrameworkCore;

namespace SchoolEntityConfiguration
{
    public partial class AcademicConfiguration : EntityTypeConfigurationBase<Academic, int>
    {
        public override Type DbContextType => base.DbContextType;
        public override void Configure(EntityTypeBuilder<Academic> builder)
        {
            builder.HasMany(u => u.AcademicClass);
            builder.HasMany(u => u.AcademicCourses);
            builder.HasMany(u => u.AcademicUsers);

            EntityConfigurationAppend(builder);
        }      
        partial void EntityConfigurationAppend(EntityTypeBuilder<Academic> builder);
    }
}
