using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolCore.Entities;
using System;
using EntityConfigurationBase;

namespace SchoolEntityConfiguration
{
    public partial class UserRoleConfiguration : EntityTypeConfigurationBase<UserRole, int>
    {
        public override Type DbContextType => base.DbContextType;
        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasIndex(u => new { u.UserId, u.RoleId }).IsUnique();
            builder.HasOne(u => u.User).WithMany(u => u.UserRoles).HasForeignKey(u => u.UserId);
            builder.HasOne(u => u.Role).WithMany(u => u.UserRoles).HasForeignKey(u => u.RoleId);

            EntityConfigurationAppend(builder);
        }      
        partial void EntityConfigurationAppend(EntityTypeBuilder<UserRole> builder);
    }
}
