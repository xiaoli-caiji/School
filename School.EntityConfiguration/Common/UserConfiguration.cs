using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolCore.Entities;
using System;
using EntityConfigurationBase;

namespace SchoolEntityConfiguration
{
    public partial class UserConfiguration : EntityTypeConfigurationBase<User, int>
    {
        public override Type DbContextType => base.DbContextType;
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.UserRoles);
            builder.HasMany(u => u.UserClaims);
            builder.HasMany(u => u.UserCourse);
            builder.HasOne(u => u.UserAcademic);

            EntityConfigurationAppend(builder);
        }      
        partial void EntityConfigurationAppend(EntityTypeBuilder<User> builder);
    }
}
