using EnterpriseBackendTemplate.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBackendTemplate.Persistence.Configurations
{
    public sealed class ProductConfiguration : BaseConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.ToTable("Products");

           

            builder.Property(product => product.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(product => product.Price)
                .HasPrecision(18, 2)
                .IsRequired();

        }
    }
}
