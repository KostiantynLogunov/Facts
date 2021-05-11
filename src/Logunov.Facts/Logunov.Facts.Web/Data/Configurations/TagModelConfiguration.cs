using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logunov.Facts.Web.Data.Configurations
{
    public class TagModelConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Name);

            builder.HasIndex(x => x.Name);

            //builder.HasMany(x => x.Facts).WithMany(x=>x.Tags);
        }
    }
}
