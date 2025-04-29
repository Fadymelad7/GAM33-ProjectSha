using Gma33.Core.Entites.StoreEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gam33.Repositries.Data.Configrations
{
    public class OrderConfigure : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O => O.TotalPrice).IsRequired()
                                               .HasColumnType("decimal(18,2)");
            builder.Property(O => O.OrderDate).IsRequired();

            builder.HasOne(O => O.customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Admin)
                    .WithMany()
                    .HasForeignKey(o => o.AdminId)
                    .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
