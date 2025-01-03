using Drive.Data.Entities;
using Drive.Data.Entities.Models.Users;
using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities.Models.Files;

namespace Drive.Domain.Repositories
{
    public class FileRepository : BaseRepository
    {
        public FileRepository(DriveDbContext dbContext) : base(dbContext) { }
        public static List<DriveFile> ListAllFiles(User loggedUser, int? folderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var files = context.driveFiles.Where(f => f.FileUserId == loggedUser.Id && f.FolderId == folderId)
                 .OrderByDescending(f => f.LastChanges).ToList();
                if (files.Count == 0)
                {
                    return null;
                }
                return files;
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
        public static List<DriveFile> ListAllFilesWithSameName(User loggedUser, string fileName, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var files = context.driveFiles.Where(f => f.FileUserId == loggedUser.Id && f.Name == fileName && f.FolderId == currentFolderId).ToList();
                if (files.Count == 0)
                {
                    return null;
                }
                return files;
            }
        }
        public static void ChangeFileName(User loggedUser, string oldFileName, string newFileName, int fileId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = context.driveFiles.AsTracking().FirstOrDefault(f => f.Name == oldFileName && f.FileUserId == loggedUser.Id && f.Id == fileId);
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
                var file = context.driveFiles.AsTracking().FirstOrDefault(f => f.Name == fileName && f.FileUserId == loggedUser.Id && f.Id == fileId);

                file.Text = string.Join("\n", newFileContent);
                file.LastChanges = DateTime.UtcNow;

                context.SaveChanges();
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
        public static bool ShareFile(User loggedUser, int fileId, string addedUser)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var file = context.driveFiles.FirstOrDefault(f => f.Id == fileId && f.FileUserId == loggedUser.Id);

                if (file != null)
                {
                    var user = context.Users.FirstOrDefault(u => u.Email == addedUser);
                    if (user != null)
                    {
                        var isAlreadyShared = context.driveFileUsers.Any(df => df.DriveFileId == fileId && df.UserId == user.Id);
                        if (isAlreadyShared)
                        {
                            return false; 
                        }
                        var sharedFile = new DriveFileUser
                        {
                            DriveFileId = fileId,
                            UserId = user.Id
                        };
                        context.driveFileUsers.Add(sharedFile);
                        context.SaveChanges(); 
                    }
                }
            }
            return true; 
        }
        public static List<DriveFile> ListAllSharedFiles(User loggedUser, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFiles = context.driveFileUsers
                    .Where(df => df.UserId == loggedUser.Id)
                    .Select(df => df.DriveFileId)
                    .ToList();

                var orderedFiles = context.driveFiles
                    .Where(f => sharedFiles.Contains(f.Id) && f.FolderId == currentFolderId)
                    .OrderByDescending(f => f.LastChanges)
                    .ToList();

                if (orderedFiles.Any())
                    return orderedFiles;

                return null;
            }
        }
        public static List<DriveFile> ListAllSharedFilesWithTheSameName(User loggedUser, string fileName, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFiles = context.driveFileUsers.Where(df => df.UserId == loggedUser.Id).Select(df => df.DriveFileId).ToList();

                var orderedFiles = context.driveFiles.Where(f => sharedFiles.Contains(f.Id) && f.Name == fileName && f.FolderId == currentFolderId)
                    .OrderByDescending(f => f.LastChanges)
                    .ToList();

                return orderedFiles;
            }
        }
        public static bool CheckIfFileExistInSharedFileById(User loggedUser, int IdOfFile, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFile = context.driveFileUsers
                    .Any(df => df.UserId == loggedUser.Id && df.DriveFileId == IdOfFile);

                if (sharedFile)
                {
                    var file = context.driveFiles
                        .FirstOrDefault(f => f.Id == IdOfFile && f.FolderId == currentFolderId);

                    return file != null;
                }

                return false;
            }
        }
        public static bool CheckIfFileExistInSharedFileByName(User loggedUser, string NameOfFile, int? currentFileId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFile = context.driveFileUsers
                    .Any(df => df.UserId == loggedUser.Id && df.DriveFile.Name == NameOfFile);

                if (sharedFile)
                {
                    var file = context.driveFiles
                        .FirstOrDefault(f => f.Name == NameOfFile && f.FolderId == currentFileId);

                    return file != null;
                }

                return false;
            }
        }
        public static int ReturnTheNumberOfSharedFilesWithSamename(User loggedUser, string fileName)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFiles = context.driveFileUsers
                    .Where(df => df.UserId == loggedUser.Id)
                    .Select(df => df.DriveFileId)
                    .ToList();

                var filesWithSameName = context.driveFiles
                    .Where(f => sharedFiles.Contains(f.Id) && f.Name == fileName)
                    .ToList();

                return filesWithSameName.Count;
            }
        }
        public static int GetSharedFileId(User loggedUser, string fileName)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFile = context.driveFileUsers
                    .Where(df => df.UserId == loggedUser.Id)
                    .Select(df => df.DriveFileId)
                    .ToList();

                var file = context.driveFiles
                    .FirstOrDefault(f => sharedFile.Contains(f.Id) && f.Name == fileName);

                return file.Id;
            }
        }
        public static DriveFile GetSharedFile(User loggedUser, string fileName, int fileId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFile = context.driveFileUsers
                    .Where(df => df.UserId == loggedUser.Id)
                    .Select(df => df.DriveFileId)
                    .ToList();

                var file = context.driveFiles
                    .FirstOrDefault(f => sharedFile.Contains(f.Id) && f.Name == fileName && f.Id == fileId);

                return file;
            }
        }
        public static void DeleteSharedFile(User loggedUser, int fileId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFile = context.driveFileUsers.FirstOrDefault(df => df.DriveFileId == fileId && df.UserId == loggedUser.Id);

                if (sharedFile != null)
                {
                    context.driveFileUsers.Remove(sharedFile);
                    context.SaveChanges();
                }
            }
        }
        public static void SavingASharedEditedFile(User loggedUser, string fileName, int fileId, List<string> newFileContent)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFile = context.driveFileUsers
                    .AsTracking()
                    .FirstOrDefault(df => df.UserId == loggedUser.Id && df.DriveFileId == fileId);

                if (sharedFile == null)
                    return;

                var file = context.driveFiles
                    .AsTracking()
                    .FirstOrDefault(f => f.Id == fileId);

                if (file == null)
                    return;


                file.Text = string.Join("\n", newFileContent);
                file.LastChanges = DateTime.UtcNow;
                context.SaveChanges();
            }
        }
        public static void StopSharingFile(User loggedUser, int fileId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFileEntries = context.driveFileUsers
                    .Where(df => df.DriveFileId == fileId && df.UserId != loggedUser.Id)
                    .ToList();

                if (sharedFileEntries.Any())
                {
                    context.driveFileUsers.RemoveRange(sharedFileEntries);
                    context.SaveChanges();
                }
            }
        }



    }
}
