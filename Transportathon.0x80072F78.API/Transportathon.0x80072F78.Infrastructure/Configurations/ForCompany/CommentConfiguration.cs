using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportathon._0x80072F78.Core.Entities.ForCompany;

namespace Transportathon._0x80072F78.Infrastructure.Configurations.Company;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.Property(x => x.OfferId).IsRequired();
        builder.Property(x => x.CompanyId).IsRequired();
        builder.Property(x => x.Score).IsRequired();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Text).HasMaxLength(150);
        builder.Property(x => x.UserId).IsRequired();
    }
}