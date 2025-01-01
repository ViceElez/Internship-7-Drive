using Drive.Data.Entities.Models.Files;
using Drive.Data.Entities.Models.Folders;
using Drive.Data.Entities.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Drive.Data.Seed;
using Drive.Data.Entities.Models.Comments;

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
        public DbSet<DriveFileUser> driveFileUsers => Set<DriveFileUser>();
        public DbSet<DriveFolderUser> driveFolderUsers => Set<DriveFolderUser>();
        public DbSet<DriveComments> driveComments => Set<DriveComments>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<DriveFolderUser>()
                .HasKey(df => new { df.DriveFolderId, df.UserId });
            modelBuilder.Entity<DriveFileUser>()
                .HasKey(df => new { df.DriveFileId, df.UserId });
           
            modelBuilder.Entity<DriveFolder>()
                .HasOne(f => f.ParentFolder)
                .WithMany(pf => pf.SubFolders)
                .HasForeignKey(f => f.ParentFolderId)
                .OnDelete(DeleteBehavior.Restrict);
          
            modelBuilder.Entity<DriveFile>()
                .HasOne(f => f.Folder)
                .WithMany(f => f.Files)
                .HasForeignKey(f => f.FolderId)
                .OnDelete(DeleteBehavior.Cascade); 
        
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(); 
         
            modelBuilder.Entity<DriveFileUser>()
                .HasIndex(df => df.DriveFileId) 
                .IsUnique(); 
            modelBuilder.Entity<DriveFolderUser>()
                .HasIndex(df => df.DriveFolderId) 
                .IsUnique();


            DataBaseSeeder.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Drive;User Id=postgres;Password=gR4)0Lo2Q;")
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }
    }

    public class DriveDbContextFactory : IDesignTimeDbContextFactory<DriveDbContext>
    {
        public DriveDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("Drive");

            var optionsBuilder = new DbContextOptionsBuilder<DriveDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new DriveDbContext(optionsBuilder.Options);
        }
    }
}
