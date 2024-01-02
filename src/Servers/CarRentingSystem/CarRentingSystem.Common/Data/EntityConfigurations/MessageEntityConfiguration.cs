using CarRentingSystem.Common.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CarRentingSystem.Common.Data.EntityConfigurations;

public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property<string>("serializedData")
            .HasField("serializedData");

        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion(x => x.AssemblyQualifiedName, x => Type.GetType(x));

        builder.Ignore(x => x.Data);
    }
}
