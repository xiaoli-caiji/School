using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolCore.Entities;
using System;
using EntityConfigurationBase;

namespace SchoolEntityConfiguration
{
    public partial class RoleClaimConfiguration : EntityTypeConfigurationBase<RoleClaim, int>
    {
        //public override Type DbContextType => base.DbContextType;
        public override void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.HasOne(r => r.Role);

            EntityConfigurationAppend(builder);
        }      
        partial void EntityConfigurationAppend(EntityTypeBuilder<RoleClaim> builder);
    }
}
