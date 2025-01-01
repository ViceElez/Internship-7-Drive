using Drive.Data.Entities.Models.Users;
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
            Console.WriteLine("Postoji vise file-ova s tim imenom.");
            Domain.Repositories.FileRepository.ListAllFilesWithSameName(loggedUser, fileName);
            Console.Write("Unesite id file-a:");
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
            Domain.Repositories.FolderRepositroy.ListAllFoldersWithSameName(loggedUser, folderName);

            Console.Write("Upisite id foldera kojeg zelite izbrisati:");
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
                "10. Podijeli datoteku\n11. Prestani dijelit mapu\n12. Prestani dijelit datoteku\n13.Povratak");
            Console.ReadKey();
        }
        public static void ListAllEditFileFunctions()
        {
            Console.WriteLine("1.:Spremanje i izlaz\n2.:Izlaz bez spremanja");
        }
    }
}
