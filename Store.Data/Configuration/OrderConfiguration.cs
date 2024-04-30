using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.shippngAddress, x =>
            {
                x.WithOwner();
            });
            builder.HasOne(order=>order.deliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(x=>x.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
