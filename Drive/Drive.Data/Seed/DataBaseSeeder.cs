using Drive.Data.Entities.Models.Comments;
using Drive.Data.Entities.Models.Files;
using Drive.Data.Entities.Models.Folders;
using Drive.Data.Entities.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Drive.Data.Seed
{
    public static class DataBaseSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new List<User>
            {
                new User { Id = 1, Email = "alice@example.com", Password = "password123" },
                new User { Id = 2, Email = "bob@example.com", Password = "password456" },
                new User { Id = 3, Email = "charlie@example.com", Password = "password789" },
                new User { Id = 4, Email = "david@example.com", Password = "password101" },
                new User { Id = 5, Email = "emma@example.com", Password = "password112" }
            });

            modelBuilder.Entity<DriveFolder>().HasData(new List<DriveFolder>
            {
                new DriveFolder { Id = 1, Name = "Dokumenti", CreatedAt = DateTime.Now, FolderUserId = 1 },
                new DriveFolder { Id = 2, Name = "Rad", CreatedAt = DateTime.Now, FolderUserId = 2, ParentFolderId = 1 },
                new DriveFolder { Id = 3, Name = "Osobno", CreatedAt = DateTime.Now, FolderUserId = 3, ParentFolderId = 1 },
                new DriveFolder { Id = 4, Name = "PraznaMapa", CreatedAt = DateTime.Now, FolderUserId = 4 }
            });

            modelBuilder.Entity<DriveFile>().HasData(new List<DriveFile>
            {
                new DriveFile { Id = 1, Name = "ProjektniPrijedlog.pdf", Text = "Projektni prijedlog koji detaljno opisuje ciljeve i plan.", FileUserId = 1, FolderId = 2 },
                new DriveFile { Id = 2, Name = "MjesečniIzvještaj.docx", Text = "Mjesečni financijski izvještaj koji sažima ključne metrike.", FileUserId = 2, FolderId = 1 },
                new DriveFile { Id = 3, Name = "OsobniDnevnik.txt", Text = "Dnevnički zapisi koji bilježe osobne refleksije.", FileUserId = 3, FolderId = 3 },
                new DriveFile { Id = 4, Name = "ZbirkaFotografija.zip", Text = "Kolekcija fotografija s odmora.", FileUserId = 4, FolderId = 4 },
                new DriveFile { Id = 5, Name = "Upute.txt", Text = "Korak-po-korak upute za postavljanje sustava.", FileUserId = 5, FolderId = 4 }
            });

            modelBuilder.Entity<DriveComments>().HasData(new List<DriveComments>
            {
                new DriveComments { Id = 1, Content = "Ovo izgleda super! Pregledajmo i završimo.", FileId = 1, UserId = 2, CreatedAt = DateTime.UtcNow },
                new DriveComments { Id = 2, Content = "Izvrstan napredak na izvještaju.", FileId = 2, UserId = 3, CreatedAt = DateTime.UtcNow },
                new DriveComments { Id = 3, Content = "Hvala na dijeljenju dnevničkih zapisa.", FileId = 3, UserId = 4, CreatedAt = DateTime.UtcNow },
                new DriveComments { Id = 4, Content = "Prekrasne fotografije! Moram ih provjeriti.", FileId = 4, UserId = 1, CreatedAt = DateTime.UtcNow },
                new DriveComments { Id = 5, Content = "Ove upute će mi biti vrlo korisne.", FileId = 5, UserId = 2, CreatedAt = DateTime.UtcNow }
            });

            modelBuilder.Entity<DriveFileUser>().HasData(new List<DriveFileUser>
            {
                new DriveFileUser { DriveFileId = 1, UserId = 2 },
                new DriveFileUser { DriveFileId = 2, UserId = 3 },
                new DriveFileUser { DriveFileId = 3, UserId = 4 },
                new DriveFileUser { DriveFileId = 4, UserId = 1 },
                new DriveFileUser { DriveFileId = 5, UserId = 2 }
            });

            modelBuilder.Entity<DriveFolderUser>().HasData(new List<DriveFolderUser>
            {
                new DriveFolderUser { DriveFolderId = 1, UserId = 2 },
                new DriveFolderUser { DriveFolderId = 2, UserId = 3 },
                new DriveFolderUser { DriveFolderId = 2, UserId = 4 },
                new DriveFolderUser { DriveFolderId = 3, UserId = 4 }
            });
        }
    }
}
