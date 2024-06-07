﻿using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevFreela.Infrastucture.Configuration
{
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
            .HasKey(p => p.Id);

            builder
              .HasOne(p => p.Freelancer)
              .WithMany(f => f.FreelanceProjects)
              .HasForeignKey(p => p.IdFreelancer)
              .OnDelete(DeleteBehavior.Restrict);

            builder
             .HasOne(p => p.Client)
             .WithMany(f => f.OwnedProjects)
             .HasForeignKey(p => p.IdClient)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
