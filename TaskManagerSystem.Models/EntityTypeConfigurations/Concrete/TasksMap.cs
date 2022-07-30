using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Models.Entities.Concrete;
using TaskManagerSystem.Models.EntityTypeConfigurations.Abstract;

namespace TaskManagerSystem.Models.EntityTypeConfigurations.Concrete
{
    public class TasksMap : BaseMap<Tasks>
    {
        public override void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(60).IsRequired(true);
            builder.Property(a => a.Content).HasMaxLength(120).IsRequired(true);
            builder.Property(a => a.EndTime).IsRequired(true);
            builder.Property(a => a.FilePath).IsRequired(false);
            builder.Property(a => a.UserID).IsRequired(true);

        
            base.Configure(builder);
        }
    }
}
