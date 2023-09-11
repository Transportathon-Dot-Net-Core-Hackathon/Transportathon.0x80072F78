using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities.Company;

namespace Transportathon._0x80072F78.Infrastructure.Configurations.Company;

public class CommentConfiguration : IEntityTypeConfiguration<Core.Entities.Company.Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        //builder.Property(x => x.gereklibelge).IsRequired();
        builder.Property(x => x.Score).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Text).IsRequired().HasMaxLength(40);

        //builder.HasOne(c => c.Sonbelge).WithMany().HasForeignKey(c => c.Sonbelgeid);
    }
}