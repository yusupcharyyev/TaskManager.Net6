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
    public class TaskDescriptionMap : BaseMap<TaskDescription>
    {
        public override void Configure(EntityTypeBuilder<TaskDescription> builder)
        {
            builder.Property(a => a.Description).HasMaxLength(200).IsRequired(true);



            base.Configure(builder);
        }
    }
}
