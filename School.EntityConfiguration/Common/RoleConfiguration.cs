using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolCore.Entities;
using System;
using EntityConfigurationBase;

namespace SchoolEntityConfiguration
{
    public partial class RoleConfiguration : EntityTypeConfigurationBase<Role, int>
    {
        public override Type DbContextType => base.DbContextType;
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(u => u.UserRoles);
            builder.HasMany(u => u.RoleClaims);

            EntityConfigurationAppend(builder);
        }      
        partial void EntityConfigurationAppend(EntityTypeBuilder<Role> builder);
    }
}
