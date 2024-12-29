using Drive.Data.Entities;
using Drive.Data.Entities.Models.Users;
using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities.Models.Files;

namespace Drive.Domain.Repositories
{
    public class FileRepository: BaseRepository
    {
        public FileRepository(DriveDbContext dbContext) : base(dbContext) { }
        public static void ListAllFiles(User loggedUser, int? folderId)
        {
            Console.WriteLine("Vasi file-ovi su:");
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var files = context.driveFiles.Where(f => f.FileUserId == loggedUser.Id && f.FolderId==folderId).ToList();
               files.OrderBy(f => f.LastChanges);
                foreach (var file in files)
                {
                    Console.WriteLine($"{file.Id}-{file.Name}");
                }
            }
        }
        public static bool CheckIfFileExistById(User loggedUser, int IdOfFile,int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = context.driveFiles.FirstOrDefault(f => f.Id ==IdOfFile && f.FolderId==currentFolderId  && f.FileUserId == loggedUser.Id);
                if (file != null)
                {
                    return true;
                }

                return false;
            }
        }
        public static bool CheckIfFileExistByName(User loggedUser, string NameOfFile, int? currentFileId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = context.driveFiles.FirstOrDefault(f => f.Name == NameOfFile && f.FolderId==currentFileId && f.FileUserId == loggedUser.Id);
                if (file != null)
                {
                    return true;
                }

                return false;
            }
        }
        public static void AddFile(User loggedUser, string fileName, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = new DriveFile
                {
                    Name = fileName,
                    FileUserId = loggedUser.Id,
                    FolderId=currentFolderId
                };
                context.driveFiles.Add(file);
                context.SaveChanges();
            }
        }
        public static void DeleteFile(User loggedUser, int IdOfFileToDelete,string NameOfFileToDelete )
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = context.driveFiles.FirstOrDefault(f => f.Id == IdOfFileToDelete && f.Name==NameOfFileToDelete && f.FileUserId == loggedUser.Id);
                if (file != null)
                {
                    context.driveFiles.Remove(file);
                    context.SaveChanges();
                }
            }
        }
        public static int ReturnTheNumberOfFilesWithSamename(User loggedUser, string fileName)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var filesWithSameName = context.driveFiles.Where(f => f.FileUserId == loggedUser.Id && f.Name == fileName).ToList();
                return filesWithSameName.Count;
            }
        }
        public static void ListAllFilesWithSameName(User loggedUser, string fileName)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var files = context.driveFiles.Where(f => f.FileUserId == loggedUser.Id && f.Name == fileName).ToList();
                foreach (var file in files)
                {
                    Console.WriteLine($"{file.Id}-{file.Name}");
                }
            }
        }
        public static void ChangeFileName(User loggedUser, string oldFileName, string newFileName, int fileId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = context.driveFiles.FirstOrDefault(f => f.Name == oldFileName && f.Id==fileId &&f.FileUserId == loggedUser.Id);
                if (file != null)
                {
                    file.Name = newFileName;
                    context.SaveChanges();
                }
            }
        }
        public static int GetFileId(User loggedUser,string fileName)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = context.driveFiles.FirstOrDefault(f => f.Name == fileName && f.FileUserId == loggedUser.Id);
                return file.Id;
            }
        }
    }
}
