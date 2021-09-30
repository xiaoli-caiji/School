using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolCore.Entities;
using System;
using EntityConfigurationBase;
using School.Core.Common.Entities;

namespace SchoolEntityConfiguration
{
    public partial class ReportCardsConfiguration : EntityTypeConfigurationBase<ReportCards, int>
    {
        public override Type DbContextType => base.DbContextType;
        public override void Configure(EntityTypeBuilder<ReportCards> builder)
        {
            
            builder.HasOne(u => u.Student);
            builder.HasOne(u => u.Courses);
            EntityConfigurationAppend(builder);
        }      
        partial void EntityConfigurationAppend(EntityTypeBuilder<ReportCards> builder);
    }
}
