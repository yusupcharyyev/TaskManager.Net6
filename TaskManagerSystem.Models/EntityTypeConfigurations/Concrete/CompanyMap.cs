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
    public class CompanyMap: BaseMap<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(a => a.CompanyName).HasMaxLength(50).IsRequired(true);
            builder.HasData( new Company { ID = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"), CompanyName = "ProjeIT"});
            base.Configure(builder);
        }
    }
}
