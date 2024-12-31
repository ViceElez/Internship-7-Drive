using Drive.Data.Entities;
using Drive.Data.Entities.Models.Users;
using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities.Models.Files;
using System.Security;

namespace Drive.Domain.Repositories
{
    public class FileRepository : BaseRepository
    {
        public FileRepository(DriveDbContext dbContext) : base(dbContext) { }
        public static void ListAllFiles(User loggedUser, int? folderId)
        {
            Console.WriteLine("Vasi file-ovi su:");
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var files = context.driveFiles.Where(f => f.FileUserId == loggedUser.Id && f.FolderId == folderId)
                 .OrderByDescending(f => f.LastChanges).ToList();
                foreach (var file in files)
                {
                    Console.WriteLine($"{file.Id}-{file.Name}");
                }
            }
        }
        public static bool CheckIfFileExistById(User loggedUser, int IdOfFile, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = context.driveFiles.FirstOrDefault(f => f.Id == IdOfFile && f.FolderId == currentFolderId && f.FileUserId == loggedUser.Id);
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
                var file = context.driveFiles.FirstOrDefault(f => f.Name == NameOfFile && f.FolderId == currentFileId && f.FileUserId == loggedUser.Id);
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
                    FolderId = currentFolderId,
                    LastChanges = DateTime.UtcNow,
                    Text = "Default text"
                };
                context.driveFiles.Add(file);
                context.SaveChanges();
            }
        }
        public static void DeleteFile(User loggedUser, int IdOfFileToDelete, string NameOfFileToDelete)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = context.driveFiles.FirstOrDefault(f => f.Id == IdOfFileToDelete && f.Name == NameOfFileToDelete && f.FileUserId == loggedUser.Id);
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
                var file = context.driveFiles.FirstOrDefault(f => f.Name == oldFileName && f.Id == fileId && f.FileUserId == loggedUser.Id);
                if (file != null)
                {
                    file.Name = newFileName;
                    context.SaveChanges();
                }
            }
        }
        public static int GetFileId(User loggedUser, string fileName)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = context.driveFiles.FirstOrDefault(f => f.Name == fileName && f.FileUserId == loggedUser.Id);
                return file.Id;
            }
        }
        public static DriveFile GetFile(User loggedUser, string fileName, int FileId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                DriveFile file = context.driveFiles.FirstOrDefault(f => f.Name == fileName && f.FileUserId == loggedUser.Id && f.Id == FileId);
                return file;
            }
        }
        public static void SavingAEditedFile(User loggedUser, string fileName, int fileId, List<string> newFileContent)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = context.driveFiles.FirstOrDefault(f => f.Name == fileName && f.FileUserId == loggedUser.Id && f.Id == fileId);
                if (file != null)
                {
                    file.Text = string.Join("\n", newFileContent);
                    file.LastChanges = DateTime.UtcNow;
                    context.SaveChanges();
                }
            }
        }
        public static List<string> HandlingBackspaceFileEditings(User loggedUser, string fileName, int fileId, List<string> newFileContent, ref string currentLine)
        {
            if (!string.IsNullOrEmpty(currentLine))
            {
                currentLine = currentLine.Substring(0, currentLine.Length - 1);
                Console.Write("\b \b");
            }
            else if (newFileContent.Count > 0)
            {
                currentLine = newFileContent[^1];
                newFileContent.RemoveAt(newFileContent.Count - 1);
                Console.Write("\r" + new string(' ', currentLine.Length) + "\r");
                Console.WriteLine("Zadnji red obrisan.");
            }
            else
            {
                Console.WriteLine("Nema reda za brisanje.");
            }

            return newFileContent;
        }
        public static List<string> HandlingEnterFileEditings(User loggedUser, string fileName, int fileId, List<string> newFileContent, ref string currentLine)
        {
            if (!string.IsNullOrEmpty(currentLine))
            {
                newFileContent.Add(currentLine);
                currentLine = string.Empty;
                Console.WriteLine("\nRed dodan.");
            }
            return newFileContent;
        }

    }
}
