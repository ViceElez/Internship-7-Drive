﻿using Drive.Data.Entities.Models.Folders;
using Drive.Data.Entities.Models.Users;
using Microsoft.EntityFrameworkCore;
using Drive.Data.Entities;


namespace Drive.Domain.Repositories
{
    public class FolderRepositroy : BaseRepository
    {
        public FolderRepositroy(DriveDbContext dbContext) : base(dbContext) { }
        public static List<DriveFolder> ListAllFolders(User loggedUser, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folders = context.driveFolders.Where(f => f.FolderUserId == loggedUser.Id && f.ParentFolderId == currentFolderId)
                    .OrderBy(f => f.Name).ToList();

                if (folders == null || folders.Count == 0)
                {
                    return null;
                }
                return folders;

            }
        }
        public static bool CheckIfFolderExistsById(int IdOfFolder, User loggedUser, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = context.driveFolders.FirstOrDefault(f => f.Id == IdOfFolder && f.ParentFolderId == currentFolderId && f.FolderUserId == loggedUser.Id);
                if (folder != null)
                {
                    return true;
                }

                return false;
            }
        }
        public static bool CheckIfFolderExistsByName(string nameOfFolder, User loggedUser, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = context.driveFolders.FirstOrDefault(f => f.Name == nameOfFolder && f.ParentFolderId == currentFolderId && f.FolderUserId == loggedUser.Id);
                if (folder != null)
                {
                    return true;
                }

                return false;
            }
        }
        public static void CreateFolder(string folderName, User loggedUser, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = new DriveFolder
                {
                    Name = folderName,
                    FolderUserId = loggedUser.Id,
                    ParentFolderId = currentFolderId,
                    CreatedAt = DateTime.UtcNow
                };
                context.driveFolders.Add(folder);
                context.SaveChanges();
            }
        }
        public static void DeleteFolder(User loggedUser, int IdOfFolderToDelete, string nameOfFolderToDelete)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = context.driveFolders.FirstOrDefault(f => f.Id == IdOfFolderToDelete && f.Name == nameOfFolderToDelete && f.FolderUserId == loggedUser.Id);
                if (folder != null)
                {
                    context.driveFolders.Remove(folder);
                    context.SaveChanges();
                }
            }
        }
        public static int ReturnTheNumberOfFoldersWithSamename(User loggedUser, string folderName)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folders = context.driveFolders.Where(f => f.FolderUserId == loggedUser.Id && f.Name == folderName).ToList();
                return folders.Count;
            }
        }
        public static List<DriveFolder> ListAllFoldersWithSameName(User loggedUser, string folderName, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folders = context.driveFolders.Where(f => f.FolderUserId == loggedUser.Id && f.Name == folderName && f.ParentFolderId == currentFolderId).ToList();
                if (folders == null || folders.Count == 0)
                {
                    return null;
                }
                return folders;
            }
        }
        public static void ChangeFolderName(User loggedUser, string oldFolderName, string newFolderName, int folderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = context.driveFolders.AsTracking().FirstOrDefault(f => f.Id == folderId && f.Name == oldFolderName && f.FolderUserId == loggedUser.Id);
                if (folder != null)
                {
                    folder.Name = newFolderName;
                    context.SaveChanges();
                }
            }
        }
        public static int GetFolderId(User loggedUser, string folderName)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = context.driveFolders.FirstOrDefault(f => f.Name == folderName && f.FolderUserId == loggedUser.Id);
                return folder.Id;
            }
        }
        public static DriveFolder GetFolderById(User loggedUser, int? folderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = context.driveFolders.FirstOrDefault(f => f.Id == folderId && f.FolderUserId == loggedUser.Id);
                return folder;
            }
        }
        public static DriveFolder GetSharedFolderById(User loggedUser, int? folderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = context.driveFolderUsers
                    .Where(fu => fu.UserId == loggedUser.Id)
                    .Select(fu => fu.DriveFolder)
                    .FirstOrDefault(f => f.Id == folderId);
                return folder;
            }
        }
        public static bool ShareFolder(User loggedUser, int folderId, string email)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);
                if (user == null)
                {
                    return false; 
                }

                var folder = context.driveFolders.FirstOrDefault(f => f.Id == folderId && f.FolderUserId == loggedUser.Id);
                if (folder == null)
                {
                    return false; 
                }

                var sharedFolder = context.driveFolderUsers.FirstOrDefault(sf => sf.DriveFolderId == folderId && sf.UserId == user.Id);
                if (sharedFolder != null)
                {
                    return false; 
                }

                sharedFolder = new DriveFolderUser
                {
                    DriveFolderId = folderId,
                    UserId = user.Id
                };
                context.driveFolderUsers.Add(sharedFolder);

                ShareFilesInFolder(context, folderId, user.Id);

                ShareSubFoldersInFolder(context, folderId, user.Id);

                context.SaveChanges();
                return true;
            }
        }
        private static void ShareFilesInFolder(DriveDbContext context, int folderId, int userId)
        {
            var filesInFolder = context.driveFiles.Where(f => f.FolderId == folderId).ToList();

            foreach (var file in filesInFolder)
            {
                var sharedFile = context.driveFileUsers.FirstOrDefault(sf => sf.DriveFileId == file.Id && sf.UserId == userId);
                if (sharedFile == null)
                {
                    sharedFile = new DriveFileUser
                    {
                        DriveFileId = file.Id,
                        UserId = userId
                    };
                    context.driveFileUsers.Add(sharedFile);
                }
            }
        }
        private static void ShareSubFoldersInFolder(DriveDbContext context, int folderId, int userId)
        {
            var subFolders = context.driveFolders.Where(f => f.ParentFolderId == folderId).ToList();

            foreach (var subFolder in subFolders)
            {
                var sharedSubFolder = context.driveFolderUsers.FirstOrDefault(sf => sf.DriveFolderId == subFolder.Id && sf.UserId == userId);
                if (sharedSubFolder == null)
                {
                    sharedSubFolder = new DriveFolderUser
                    {
                        DriveFolderId = subFolder.Id,
                        UserId = userId
                    };
                    context.driveFolderUsers.Add(sharedSubFolder);
                }

                ShareFilesInFolder(context, subFolder.Id, userId);
                ShareSubFoldersInFolder(context, subFolder.Id, userId);
            }
        }
        public static int ReturnTheNumberOfSharedFoldersWithSamename(User loggedUser, string folderName)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFolders = context.driveFolderUsers
                    .Where(fu => fu.UserId == loggedUser.Id)
                    .Select(fu => fu.DriveFolder)
                    .Where(f => f.Name == folderName)
                    .ToList();
                return sharedFolders.Count;
            }
        }
        public static List<DriveFolder> ListAllSharedFolders(User loggedUser, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFolders = context.driveFolderUsers
                    .Where(fu => fu.UserId == loggedUser.Id)
                    .Select(fu => fu.DriveFolder)
                    .Where(f => f.ParentFolderId == currentFolderId)
                    .OrderBy(f => f.Name)
                    .ToList();

               if(sharedFolders == null || sharedFolders.Count == 0)
                    return null;


                return sharedFolders;
            }
        }
        public static List<DriveFolder> ListAllSharedFoldersWithSameName(User loggedUser, string folderName, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFolders = context.driveFolderUsers
                     .Where(fu => fu.UserId == loggedUser.Id)
                     .Select(fu => fu.DriveFolder)
                     .Where(f => f.Name == folderName && f.ParentFolderId == currentFolderId)
                     .OrderBy(f => f.Name)
                     .ToList();

                if (sharedFolders == null || sharedFolders.Count == 0)
                    return null;
                return sharedFolders;
            }
        }
        public static bool CheckIfFolderExistsInSharedFoldersById(User loggedUser, int folderId, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = context.driveFolderUsers
                    .Where(fu => fu.UserId == loggedUser.Id)
                    .Select(fu => fu.DriveFolder)
                    .FirstOrDefault(f => f.Id == folderId && f.ParentFolderId == currentFolderId);
                if (folder != null)
                    return true;
                return false;
            }
        }
        public static bool CheckIfFolderExistsInSharedFoldersByName(User loggedUser, string fileName, int? currentFolderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = context.driveFolderUsers
                    .Where(fu => fu.UserId == loggedUser.Id)
                    .Select(fu => fu.DriveFolder)
                    .FirstOrDefault(f => f.Name == fileName && f.ParentFolderId == currentFolderId);
                if (folder != null)
                    return true;
                return false;
            }
        }
        public static int GetSharedFolderId(User loggedUser, string folderName)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = context.driveFolderUsers
                    .Where(fu => fu.UserId == loggedUser.Id)
                    .Select(fu => fu.DriveFolder)
                    .FirstOrDefault(f => f.Name == folderName);
                return folder.Id;
            }
        }
        public static void DeleteSharedFolder(User loggedUser, int folderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFolder = context.driveFolderUsers.FirstOrDefault(fu => fu.UserId == loggedUser.Id && fu.DriveFolderId == folderId);
                if (sharedFolder != null)
                {
                    context.driveFolderUsers.Remove(sharedFolder);
                    context.SaveChanges();
                }
            }
        }
        public static void StopSharingFolder(User loggedUser, int folderId)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var sharedFolderEntries = context.driveFolderUsers
                    .Where(df => df.DriveFolderId == folderId && df.UserId != loggedUser.Id)
                    .ToList();

                if (sharedFolderEntries.Any())
                {
                    context.driveFolderUsers.RemoveRange(sharedFolderEntries);
                    context.SaveChanges();
                }
            }
        }



    }
}
