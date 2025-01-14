﻿using Drive.Data.Entities.Models.Users;
using Drive.Presentation.Actions.Menus;
using System.Text.RegularExpressions;

namespace Drive.Presentation.Helper
{
    public static class InputValidation
    {
        public static bool ConfirmAndDelete()
        {
            var answer = string.Empty;
            while (true)
            {
                Console.Write("Dali zelite nastaviti (da) ili odustati (ne): ");
                answer = Console.ReadLine()?.ToLower().Trim();

                if (answer == "da")
                {
                    return true;
                }
                else if (answer == "ne")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Krivi unos.");
                }
            }

        }
        public static bool IsValid(string email)
        {
            string regex = @"^[^\s@]+@[^\s@]+\.[^\s@]{3,}$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
        public static string FileNameValidation(User loggedUser, int? currentFolderId)
        {
            var fileName = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("Ime nemoze biti prazno");
                    fileName = ReturnNameOrMenuForNameValidation(loggedUser, fileName, currentFolderId);
                }
                else if (!Drive.Domain.Repositories.FileRepository.CheckIfFileExistByName(loggedUser, fileName, currentFolderId))
                {
                    Console.WriteLine("Ne postoji file s time imenom.");
                    fileName = ReturnNameOrMenuForNameValidation(loggedUser, fileName, currentFolderId);
                }
                else
                    break;
            }

            return fileName;
        }
        public static int FileIdValidation(User loggedUser, string fileName, int? currentFolderId)
        {
            var fileId = 0;
            foreach (var file in Drive.Domain.Repositories.FileRepository.ListAllFilesWithSameName(loggedUser, fileName, currentFolderId))
                Console.WriteLine($"{file.Id} - {file.Name}");
            Console.Write("\nUnesite id file-a:");
            var isIdCorrect = int.TryParse(Console.ReadLine(), out fileId);

            while (true)
            {
                if (!isIdCorrect)
                {
                    Console.WriteLine("Uneseni id mora biti broj.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite id file-a:");
                        isIdCorrect = int.TryParse(Console.ReadLine(), out fileId);
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else if (fileId <= 0)
                {
                    Console.WriteLine("Id file-a mora biti veci od 0.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite id file-a:");
                        isIdCorrect = int.TryParse(Console.ReadLine(), out fileId);
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else if (!Drive.Domain.Repositories.FileRepository.CheckIfFileExistById(loggedUser, fileId, currentFolderId))
                {
                    Console.WriteLine("Ne postoji file s tim id-om.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite id file-a:");
                        fileId = int.Parse(Console.ReadLine());
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else
                    break;
            }

            return fileId;
        }
        public static string FolderNameValidation(User loggedUser, int? currentFolderId)
        {
            var folderName = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(folderName))
                {
                    Console.WriteLine("Ime mape ne moze biti prazno.");
                    folderName = ReturnNameOrMenuForNameValidation(loggedUser, folderName, currentFolderId);
                }
                else if (!Domain.Repositories.FolderRepositroy.CheckIfFolderExistsByName(folderName, loggedUser, currentFolderId))
                {
                    Console.WriteLine("Nepostoji mapa s tim imenom.");
                    folderName = ReturnNameOrMenuForNameValidation(loggedUser, folderName, currentFolderId);
                }
                else
                {
                    break;
                }
            }
            return folderName;
        }
        public static int FolderIdValidation(User loggedUser, string folderName, int? currentFolderId)
        {
            Console.WriteLine("Postoji vise foldera s istim imenom");
            foreach (var folder in Domain.Repositories.FolderRepositroy.ListAllFoldersWithSameName(loggedUser, folderName, currentFolderId))
                Console.WriteLine($"{folder.Id} - {folder.Name}");

            Console.Write("\nUpisite id foldera kojeg zelite izbrisati:");
            var isIdCorrect = int.TryParse(Console.ReadLine(), out var folderId);

            while (true)
            {
                if (!isIdCorrect)
                {
                    Console.WriteLine("Upisani id mora biti broj.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Upisite id foldera:");
                        isIdCorrect = int.TryParse(Console.ReadLine(), out folderId);
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else if (folderId <= 0)
                {
                    Console.WriteLine("Id nemoze biti 0 ili negativan.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Upisite id foldera:");
                        isIdCorrect = int.TryParse(Console.ReadLine(), out folderId);
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else if (!Domain.Repositories.FolderRepositroy.CheckIfFolderExistsById(folderId, loggedUser, currentFolderId))
                {
                    Console.WriteLine("Ne postoji folder s tim id-om.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Upisite id foldera:");
                        isIdCorrect = int.TryParse(Console.ReadLine(), out folderId);
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else
                    break;
            }

            return folderId;
        }
        public static string ReturnNameOrMenuForNameValidation(User loggedUser, string fileName, int? currentFolderId)
        {
            var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
            if (confirmForFolderName)
            {
                Console.Write("Unesite ime:");
                fileName = Console.ReadLine().Trim();
                return fileName;
            }
            else
            {
                Console.WriteLine("Proces je prekinut.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                return null;
            }
        }
        public static void ListAllFunctions()
        {
            Console.WriteLine("1. Stvori mapu\n2. Stvori datoteku\n3. Udi u mapu\n4. Uredi datoteku" +
                "\n5. Izbrisi mapu\n6. Izbrisi datoteku\n7. Promijeni naziv mape\n8. Promijeni naziv datoteke\n9. Podijeli mapu\n" +
                "10. Podijeli datoteku\n11. Prestani dijelit mapu\n12. Prestani dijelit datoteku\n13. Povratak");
            Console.ReadKey();
        }
        public static void ListAllEditFileFunctions()
        {
            Console.WriteLine("1.:Spremanje i izlaz\n2.:Izlaz bez spremanja\n3. Otvori komentare");
        }
        public static string SharedFolderNameValidation(User loggedUser, int? currentFolderId)
        {
            var folderName = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(folderName))
                {
                    Console.WriteLine("Ime mape ne moze biti prazno.");
                    folderName = ReturnNameOrSharedMenuForNameValidation(loggedUser, folderName, currentFolderId);
                }
                else if (!Domain.Repositories.FolderRepositroy.CheckIfFolderExistsInSharedFoldersByName(loggedUser,folderName,currentFolderId))
                {
                    Console.WriteLine("Nepostoji mapa s tim imenom.");
                    folderName = ReturnNameOrSharedMenuForNameValidation(loggedUser, folderName, currentFolderId);
                }
                else
                    break;
            }
            return folderName;
        }
        public static int SharedFolderIdValidation(User loggedUser, string folderName, int? currentFolderId)
        {
            Console.WriteLine("Postoji vise foldera s istim imenom");
            foreach(var folder in Domain.Repositories.FolderRepositroy.ListAllSharedFoldersWithSameName(loggedUser, folderName, currentFolderId))
                Console.WriteLine($"{folder.Id} - {folder.Name}");

            Console.Write("\nUpisite id foldera kojeg zelite izbrisati:");
            var isIdCorrect = int.TryParse(Console.ReadLine(), out var folderId);

            while (true)
            {
                if (!isIdCorrect)
                {
                    Console.WriteLine("Upisani id mora biti broj.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Upisite id foldera:");
                        isIdCorrect = int.TryParse(Console.ReadLine(), out folderId);
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else if (folderId <= 0)
                {
                    Console.WriteLine("Id nemoze biti 0 ili negativan.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Upisite id foldera:");
                        isIdCorrect = int.TryParse(Console.ReadLine(), out folderId);
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else if (!Domain.Repositories.FolderRepositroy.CheckIfFolderExistsInSharedFoldersById(loggedUser,folderId,currentFolderId))
                {
                    Console.WriteLine("Ne postoji folder s tim id-om.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Upisite id foldera:");
                        isIdCorrect = int.TryParse(Console.ReadLine(), out folderId);
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else
                    break;
            }

            return folderId;
        }
        public static string SharedFileNameValidation(User loggedUser, int? currentFolderId)
        {
            var fileName = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("Ime nemoze biti prazno");
                    fileName = ReturnNameOrSharedMenuForNameValidation(loggedUser, fileName, currentFolderId);
                }
                else if (!Domain.Repositories.FileRepository.CheckIfFileExistInSharedFileByName(loggedUser,fileName,currentFolderId))
                {
                    Console.WriteLine("Ne postoji file s time imenom.");
                    fileName = ReturnNameOrSharedMenuForNameValidation(loggedUser, fileName, currentFolderId);
                }
                else
                    break;
            }

            return fileName;
        }
        public static int SharedFileIdValidation(User loggedUser, string fileName, int? currentFolderId)
        {
            var fileId = 0;
            foreach (var file in Domain.Repositories.FileRepository.ListAllSharedFilesWithTheSameName(loggedUser, fileName, currentFolderId))
                Console.WriteLine($"{file.Id} - {file.Name}");
            Console.Write("\nUnesite id file-a:");
            var isIdCorrect = int.TryParse(Console.ReadLine(), out fileId);

            while (true)
            {
                if (!isIdCorrect)
                {
                    Console.WriteLine("Uneseni id mora biti broj.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite id file-a:");
                        isIdCorrect = int.TryParse(Console.ReadLine(), out fileId);
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else if (fileId <= 0)
                {
                    Console.WriteLine("Id file-a mora biti veci od 0.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite id file-a:");
                        isIdCorrect = int.TryParse(Console.ReadLine(), out fileId);
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else if (!Domain.Repositories.FileRepository.CheckIfFileExistInSharedFileById(loggedUser,fileId,currentFolderId))
                {
                    Console.WriteLine("Ne postoji file s tim id-om.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite id file-a:");
                        fileId = int.Parse(Console.ReadLine());
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else
                    break;
            }

            return fileId;
        }
        public static string ReturnNameOrSharedMenuForNameValidation(User loggedUser, string fileName, int? currentFolderId)
        {
            var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
            if (confirmForFolderName)
            {
                Console.Write("Unesite ime:");
                fileName = Console.ReadLine().Trim();
                return fileName;
            }
            else
            {
                Console.WriteLine("Proces je prekinut.");
                Console.ReadKey();
                SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
                return null;
            }
        }
        public static void ListAllSharedFunctions()
        {
            Console.WriteLine("1. Udi u mapu\n2. Uredi datoteku\n3. Izbrisi mapu\n4. Izbrisi datoteku\n5. Povratak");
            Console.ReadKey();
        }
        public static void ListAllCommentFunctions()
        {
            Console.WriteLine("1. Dodaj komentar\n2. Uredi komentar\n3. Izbrisi komentar\n4. Izlaz");
            Console.ReadKey();
        }
        public static int CommentIdValidation(User loggedUser, int? currentFolderId,int fileId)
        {
            var validCommentId = int.TryParse(Console.ReadLine().Trim(), out int commentId);
            while (true)
            {
                if (!validCommentId)
                {
                    Console.WriteLine("Upisani id mora biti broj.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Upisite id komentara:");
                        validCommentId = int.TryParse(Console.ReadLine(), out commentId);
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else if (commentId <= 0)
                {
                    Console.WriteLine("Id nemoze biti 0 ili negativan.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Upisite id komentara:");
                        validCommentId = int.TryParse(Console.ReadLine(), out commentId);
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else if (!Domain.Repositories.CommentRepository.CheckIfCommentExists(fileId, commentId))
                {
                    Console.WriteLine("Ne postoji komentar s tim id-om.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Upisite id komentara:");
                        validCommentId = int.TryParse(Console.ReadLine(), out commentId);
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else
                    break;
            }
            return commentId;
        }
    } 
}
