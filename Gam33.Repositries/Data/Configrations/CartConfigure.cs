﻿using Gma33.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gam33.Repositries.Data.Configrations
{
    public class CartConfigure : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(c => c.TotalPrice)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

            builder.HasOne(c => c.Customer)
                    .WithOne(c => c.Cart)
                    .HasForeignKey<Cart>(c => c.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
