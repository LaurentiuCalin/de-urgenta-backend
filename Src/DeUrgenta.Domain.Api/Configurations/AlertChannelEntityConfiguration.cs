using System.Collections.Generic;
using DeUrgenta.Domain.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeUrgenta.Domain.Api.Configurations
{
    internal class AlertChannelEntityConfiguration : IEntityTypeConfiguration<AlertChannel>
    {
        public void Configure(EntityTypeBuilder<AlertChannel> builder)
        {
            builder
                .HasKey(x => x.Id)
                .HasName("PK_AlertChannel");

            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(64);
            
            builder
                .HasMany(x => x.Users)
                .WithMany(x => x.SelectedAlertChannels);

            var knownAlertChannels = new List<AlertChannel>
            {
                new()
                {
                    Id = 1,
                    Type = KnownAlertChannelTypes.Email
                },
                new()
                {
                    Id = 2,
                    Type = KnownAlertChannelTypes.Sms
                },
                new()
                {
                    Id = 3,
                    Type = KnownAlertChannelTypes.PushNotification
                },
            };

            builder.HasData(knownAlertChannels);
        }
    }
}