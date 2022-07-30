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
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public virtual void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(a => a.FirstName).HasMaxLength(20).IsRequired(true);
            builder.Property(a => a.LastName).HasMaxLength(20).IsRequired(true);
            builder.HasOne(a => a.Companys).WithMany(b => b.Users).HasForeignKey(a => a.CompanyID);
        }
    }
}
