using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheTowerAPI.Models;

namespace TheTowerAPI.Configurations
{
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder
                .Property(l => l.LevelName)
                .HasMaxLength(30);
            
            builder
                .HasKey(l => l.LevelName);
        }
    }
}