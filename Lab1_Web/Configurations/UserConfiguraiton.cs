﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1_Web.Entities;

namespace Lab1_Data.Configurations;

internal class UserConfiguraiton : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Articles)
            .WithOne(x => x.Author);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Author);

        builder.HasOne(x => x.Gender)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.GenderId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
