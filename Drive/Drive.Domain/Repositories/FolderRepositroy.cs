﻿using Drive.Data.Entities.Models.Folders;
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
        public static bool CheckIfFolderExistsById(int IdOfFolder, User loggedUser)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder=context.driveFolders.FirstOrDefault(f => f.Id == IdOfFolder && f.FolderUserId == loggedUser.Id);
                if (folder != null)
                {
                   return true;
                }

                return false;
            }
        }
        public static bool CheckIfFolderExistsByName(string nameOfFolder, User loggedUser)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = context.driveFolders.FirstOrDefault(f => f.Name == nameOfFolder && f.FolderUserId == loggedUser.Id);
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
        public static void DeleteFolderByName(User loggedUser,string nameOfFolderToDelete)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder=context.driveFolders.FirstOrDefault(f=>f.Name==nameOfFolderToDelete && f.FolderUserId==loggedUser.Id);
                if(folder != null)
                {
                    context.driveFolders.Remove(folder);
                    context.SaveChanges();
                }
            }
        }
        public static void DeleteFolderById(User loggedUser, int IdOfFolderToDelete)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var folder = context.driveFolders.FirstOrDefault(f => f.Id == IdOfFolderToDelete && f.FolderUserId == loggedUser.Id);
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
        public static void ListAllFoldersWithSameName(User loggedUser, string folderName)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var foldersWithSameName = context.driveFolders.Where(f => f.FolderUserId == loggedUser.Id && f.Name == folderName).ToList();
                foreach (var folder in foldersWithSameName)
                {
                    Console.WriteLine($"{folder.Id}-{folder.Name}");
                }
            }
        }
    }
}
