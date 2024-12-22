﻿using Drive.Data.Entities.Models.Files;
using Drive.Data.Entities.Models.Folders;
using Drive.Data.Entities.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Drive.Data.Entities
{
    public class DriveDbContext : DbContext
    {

        public DriveDbContext(DbContextOptions<DriveDbContext> options) : base(options)
        {
        }

        public DbSet<DriveFile> driveFiles => Set<DriveFile>();
        public DbSet<DriveFolder> driveFolders => Set<DriveFolder>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DriveFolder>()
                .HasOne(f => f.FolderUser)
                .WithMany(u => u.Folders)
                .HasForeignKey(f => f.FolderUserId);

            modelBuilder.Entity<DriveFolder>()
                .HasOne(f => f.ParentFolder)
                .WithMany(p => p.SubFolders)
                .HasForeignKey(f => f.ParentFolderId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DriveFile>()
                .HasOne(f => f.FileUser)
                .WithMany(u => u.Files)
                .HasForeignKey(f => f.FileUserId);

            modelBuilder.Entity<DriveFile>()
                .HasOne(f => f.Folder)
                .WithMany(d => d.Files)
                .HasForeignKey(f => f.FolderId);

            Seed.DatabaseSeeder.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

    }

    public class DriveDbContextFactory : IDesignTimeDbContextFactory<DriveDbContext>
    {
        public DriveDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("App.config")
                .Build();

            var connectionString = configuration.GetConnectionString("Drive");

            var optionsBuilder = new DbContextOptionsBuilder<DriveDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new DriveDbContext(optionsBuilder.Options);
        }
    }
}
