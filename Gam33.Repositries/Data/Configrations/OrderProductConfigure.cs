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
    public class OrderProductConfigure : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasOne(po => po.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(p => p.ProductId)
                    .OnDelete(DeleteBehavior.Restrict); // Don't allow deleting a Product if it's in an Order

            builder.HasOne(po => po.order)
                 .WithMany(o => o.orderProducts)
                 .HasForeignKey(o => o.OrderId)
                 .OnDelete(DeleteBehavior.Cascade);  // If an order is deleted, remove related OrderProducts


            builder.HasKey(op => new { op.ProductId, op.OrderId });// Composite Primary Key

        }
    }
}
