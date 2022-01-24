using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheTowerAPI.Models;

namespace TheTowerAPI.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(u => u.Nickname)
                .HasMaxLength(20);

            builder
                .Property(u => u.Email)
                .IsRequired();
            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .Property(u => u.Password)
                .IsRequired();
            
            //TODO set default value to 0 when email validation will be added
            builder.Property(u => u.Role)
                .HasDefaultValue(1);
            
            builder
                .HasDiscriminator(a => a.Role)
                .HasValue<User>(1)
                .HasValue<Moderator>(2)
                .HasValue<Admin>(3);

            builder
                .HasKey(u => u.Nickname);
        }
    }
}