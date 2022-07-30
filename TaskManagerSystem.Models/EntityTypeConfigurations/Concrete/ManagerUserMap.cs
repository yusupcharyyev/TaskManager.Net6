using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Models.Entities.Concrete;

namespace TaskManagerSystem.Models.EntityTypeConfigurations.Concrete
{
    public class ManagerUserMap : IEntityTypeConfiguration<ManagerUser>
    {
        public void Configure(EntityTypeBuilder<ManagerUser> builder)
        {
            builder.HasKey(a => new { a.UserId, a.ManagerId });
        }
    }
}
