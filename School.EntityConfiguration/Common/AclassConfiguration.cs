using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolCore.Entities;
using System;
using EntityConfigurationBase;

namespace SchoolEntityConfiguration
{
    public partial class AclassConfiguration : EntityTypeConfigurationBase<AClass, int>
    {
        public override Type DbContextType => base.DbContextType;
        public override void Configure(EntityTypeBuilder<AClass> builder)
        {
            builder.HasMany(u => u.AclassUsers);
            builder.HasOne(u => u.Academic);

            EntityConfigurationAppend(builder);
        }      
        partial void EntityConfigurationAppend(EntityTypeBuilder<AClass> builder);
    }
}
