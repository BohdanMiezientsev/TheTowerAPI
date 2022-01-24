using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheTowerAPI.Models;

namespace TheTowerAPI.Configurations
{
    public class RecordConfiguration : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            builder
                .Property(r => r.Nickname)
                .HasMaxLength(20);

            builder
                .Property(r => r.LevelName)
                .HasMaxLength(30);

            builder
                .Property(r => r.Time)
                .IsRequired();
            
            builder
                .HasKey(r => new {r.Nickname, r.LevelName});

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.Records)
                .HasForeignKey(r => r.Nickname);

            builder
                .HasOne(r => r.Level)
                .WithMany(l => l.Records)
                .HasForeignKey(r => r.LevelName);
        }
    }
}