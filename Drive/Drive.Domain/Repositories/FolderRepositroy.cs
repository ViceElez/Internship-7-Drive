using Drive.Data.Entities.Models.Folders;
using Drive.Data.Entities.Models.Users;
using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities;


namespace Drive.Domain.Repositories
{
    public class FolderRepositroy:BaseRepository
    {
        public FolderRepositroy(DriveDbContext dbContext) : base(dbContext) { } 
        public static void ListAllFolders(User loggedUser )
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folders = context.driveFolders.Where(f => f.FolderUserId == loggedUser.Id).ToList();
                folders.OrderBy(f => f.Name);
                foreach (var folder in folders)
                {
                    Console.WriteLine($"{folder.Id}-{folder.Name}");
                }
            }
        }
        public static bool CheckIfFolderExists(string folderName, User loggedUser)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder=context.driveFolders.FirstOrDefault(f => f.Name == folderName && f.FolderUserId == loggedUser.Id);
                if (folder != null)
                {
                   return true;
                }

                return false;
            }
        }
        public static void CreateFolder(string folderName, User loggedUser)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = new DriveFolder
                {
                    Name = folderName,
                    FolderUserId = loggedUser.Id
                };
                context.driveFolders.Add(folder);
                context.SaveChanges();
            }
        }
    }
}
