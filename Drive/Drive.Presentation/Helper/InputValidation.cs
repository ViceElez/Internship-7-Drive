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
        public static string FileNameValidation(User loggedUser)
        {
            var fileName = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("Ime nemoze biti prazno");
                    fileName = ReturnNameOrMenuForNameValidation(loggedUser, fileName);
                }
                else if (!Drive.Domain.Repositories.FileRepository.CheckIfFileExistByName(loggedUser, fileName))
                {
                    Console.WriteLine("Ne postoji file s time imenom.");
                    fileName = ReturnNameOrMenuForNameValidation(loggedUser, fileName);
                }
                else
                    break;
            }

            return fileName;
        }
        public static int FileIdValidation(User loggedUser, string fileName)
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
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
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
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else if (!Drive.Domain.Repositories.FileRepository.CheckIfFileExistById(loggedUser, fileId))
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
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else
                    break;
            }

            return fileId;
        }
        public static string FolderNameValidation(User loggedUser)
        {
            var folderName = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(folderName))
                {
                    Console.WriteLine("Ime mape ne moze biti prazno.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite ime mape:");
                        folderName = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else if (!Domain.Repositories.FolderRepositroy.CheckIfFolderExistsByName(folderName, loggedUser))
                {
                    Console.WriteLine("Nepostoji mapa s tim imenom.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite ime mape:");
                        folderName = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else
                {
                    break;
                }
            }
            return folderName;
        }
        public static int FolderIdValidation(User loggedUser, string folderName)
        {
            Console.WriteLine("Postoji vise foldera s istim imenom");
            Domain.Repositories.FolderRepositroy.ListAllFoldersWithSameName(loggedUser, folderName);

            Console.Write("Upisite id foldera kojeg zelite izbrisati:");
            var isIdCorrect = int.TryParse(Console.ReadLine(), out var folderId);

            while (true)
            {
                if (!Domain.Repositories.FolderRepositroy.CheckIfFolderExistsById(folderId, loggedUser))
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
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else if (!isIdCorrect)
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
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
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
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else
                    break;
            }

            return folderId;
        }
        public static string ReturnNameOrMenuForNameValidation(User loggedUser,string fileName)
        {
            var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
            if (confirmForFolderName)
            {
                Console.Write("Unesite ime file-a:");
                fileName = Console.ReadLine().Trim();
                return fileName;
            }
            else
            {
                Console.WriteLine("Proces je prekinut.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser);
                return null;
            }
        }
        public static void ListAllFunctions()
        {
            Console.WriteLine("1. Stvori mapu\n2. Stvori datoteku\n3. Udi u mapu\n4. Uredi datoteku" +
                "\n5. Izbrisi mapu\n6. Izbrisi datoteku\n7. Promijeni naziv mape\n8. Promijeni naziv datoteke\n9. Povratak");
            Console.ReadKey();
        }
    }
}
