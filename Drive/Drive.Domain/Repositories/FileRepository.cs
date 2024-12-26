using Drive.Data.Entities;
using Drive.Data.Entities.Models.Users;
using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities.Models.Files;

namespace Drive.Domain.Repositories
{
    public class FileRepository: BaseRepository
    {
        public FileRepository(DriveDbContext dbContext) : base(dbContext) { }
        public static void ListAllFiles(User loggedUser)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var files = context.driveFiles.Where(f => f.FileUserId == loggedUser.Id).ToList();
               files.OrderBy(f => f.LastChanges);
                foreach (var file in files)
                {
                    Console.WriteLine($"{file.Id}-{file.Name}");
                }
            }
        }
        public static bool CheckIfFileExist(User loggedUser, string fileName)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = context.driveFiles.FirstOrDefault(f => f.Name == fileName && f.FileUserId == loggedUser.Id);
                if (file != null)
                {
                    return true;
                }

                return false;
            }
        }
        public static void AddFile(User loggedUser, string fileName)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = new DriveFile
                {
                    Name = fileName,
                    FileUserId = loggedUser.Id,
                };
                context.driveFiles.Add(file);
                context.SaveChanges();
            }
        }
    }
}
