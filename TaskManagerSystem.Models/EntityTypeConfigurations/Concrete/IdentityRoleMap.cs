﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerSystem.Models.EntityTypeConfigurations.Concrete
{
    public class IdentityRoleMap : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" }, new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Yonetici", NormalizedName = "YONETICI" }, new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Personel", NormalizedName = "PERSONEL" });
        }
    }
}
