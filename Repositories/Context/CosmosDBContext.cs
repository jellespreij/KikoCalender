﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Context
{
    public class CosmosDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Folder> Folders { get; set; }

        //DONT FORGET TO NUGET INSTALL: Install-Package Microsoft.EntityFrameworkCore.Cosmos -Version 5.0.10 for DbContextbuilder Use Cosmos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                Environment.GetEnvironmentVariable("CosmosEndUrl"), Environment.GetEnvironmentVariable("CosmosDBString"),
                Environment.GetEnvironmentVariable("CosmosDbName"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Containers");

            modelBuilder.Entity<User>()
                .ToContainer("users");

            modelBuilder.Entity<User>()
                .HasNoDiscriminator();


            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Appointment>()
                .ToContainer("appointments");

            modelBuilder.Entity<Appointment>()
                .HasNoDiscriminator();

            modelBuilder.Entity<Appointment>()
                .HasPartitionKey(ap => ap.PartitionKey);

            modelBuilder.Entity<Asset>()
                .ToContainer("assets");

            modelBuilder.Entity<Asset>()
                .HasNoDiscriminator();

            modelBuilder.Entity<Asset>()
                .HasPartitionKey(a => a.PartitionKey);

            modelBuilder.Entity<Family>()
                .ToContainer("families");

            modelBuilder.Entity<Family>()
                .HasNoDiscriminator();

            modelBuilder.Entity<Family>()
                .HasPartitionKey(f => f.PartitionKey);

            modelBuilder.Entity<Family>()
               .HasMany(f => f.Users)
               .WithOne(u => u.Family)
               .HasForeignKey(u => u.FamilyId);

            modelBuilder.Entity<Family>()
                .HasMany(f => f.Folders)
                .WithOne(fo => fo.Family)
                .HasForeignKey(fo => fo.FamilyId); 

            modelBuilder.Entity<Contact>()
                .ToContainer("contacts");

            modelBuilder.Entity<Contact>()
                .HasNoDiscriminator();

            modelBuilder.Entity<Contact>()
                .HasPartitionKey(c => c.PartitionKey);

            modelBuilder.Entity<Transaction>()
                .ToContainer("transactions");

            modelBuilder.Entity<Transaction>()
                .HasNoDiscriminator();

            modelBuilder.Entity<Transaction>()
                .HasPartitionKey(t => t.PartitionKey);

            modelBuilder.Entity<Folder>()
               .ToContainer("folders");

            modelBuilder.Entity<Folder>()
                .HasNoDiscriminator();

            modelBuilder.Entity<Folder>()
                .HasPartitionKey(f => f.PartitionKey);

            modelBuilder.Entity<Folder>()
              .HasMany(fo => fo.Assets)
              .WithOne(a => a.Folder)
              .HasForeignKey(a => a.FolderId);
        }
    }
}
