using Drive.Data.Entities.Models.Users;
using Drive.Presentation.Actions.Menus;

namespace Drive.Presentation.Actions.Folder
{
    public class FolderActions
    {
        public static void CreateFolder(User loggedUser)
        {
            Console.Write("Upisite ime nove mape:");
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
                        Console.WriteLine("Proces kreiranja mape je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else
                {
                    break;
                }
            }

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                Drive.Domain.Repositories.FolderRepositroy.CreateFolder(folderName, loggedUser);
                Console.WriteLine("Folder uspjesno kreirana.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser);
            }
            else
            {
                Console.WriteLine("Proces kreiranja foldera je prekinut.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser);
            }
        }
        public static void DeleteFolder(User loggedUser)
        {
            Console.Write("Upisite ime foldera kojeg zelite izbrisati:");
            var folderNameToDelete = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(folderNameToDelete))
                {
                    Console.WriteLine("Ime mape ne moze biti prazno.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite ime mape:");
                        folderNameToDelete = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces brisanja mape je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else if (!Domain.Repositories.FolderRepositroy.CheckIfFolderExistsByName(folderNameToDelete, loggedUser))
                {
                    Console.WriteLine("Nepostoji mapa s tim imenom.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite ime mape:");
                        folderNameToDelete = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces brisanja mape je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else
                {
                    break;
                }
            }

            if (Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser, folderNameToDelete) > 1)
            {
                Console.WriteLine("Postoji vise foldera s istim imenom");
                Domain.Repositories.FolderRepositroy.ListAllFoldersWithSameName(loggedUser, folderNameToDelete);
                Console.Write("Upisite id foldera kojeg zelite izbrisati:");
                var isIdCorrect = int.TryParse(Console.ReadLine(), out var IdOfFolderToDelete);
                while (true)
                {
                    if (!Domain.Repositories.FolderRepositroy.CheckIfFolderExistsById(IdOfFolderToDelete, loggedUser))
                    {
                        Console.WriteLine("Ne postoji folder s tim id-om.");
                        var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                        if (confirmForFolderName)
                        {
                            Console.Write("Upisite id folder kojeg zelite izbrisati:");
                            isIdCorrect = int.TryParse(Console.ReadLine(), out IdOfFolderToDelete);
                        }
                        else
                        {
                            Console.WriteLine("Proces brisanja foldera je prekinut.");
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
                            Console.Write("Upisite id foldera kojeg zelite izbrisati:");
                            isIdCorrect = int.TryParse(Console.ReadLine(), out IdOfFolderToDelete);
                        }
                        else
                        {
                            Console.WriteLine("Proces brisanja foldera je prekinut.");
                            Console.ReadKey();
                            MyDiskMenuActions.MyDiskMenu(loggedUser);
                        }
                    }
                    else if (IdOfFolderToDelete <= 0)
                    {
                        Console.WriteLine("Id nemoze biti 0 ili negativan.");
                        var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                        if (confirmForFolderName)
                        {
                            Console.Write("Upisite id foldera kojeg zelite izbrisati:");
                            isIdCorrect = int.TryParse(Console.ReadLine(), out IdOfFolderToDelete);
                        }
                        else
                        {
                            Console.WriteLine("Proces brisanja foldera je prekinut.");
                            Console.ReadKey();
                            MyDiskMenuActions.MyDiskMenu(loggedUser);
                        }
                    }
                    else
                        break;

                    if (Helper.InputValidation.ConfirmAndDelete())
                    {
                        if (Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser, folderNameToDelete) > 1)
                        {
                            Domain.Repositories.FolderRepositroy.DeleteFolderById(loggedUser, IdOfFileToDelete);
                            Console.WriteLine("Folder uspjesno izbrisan.");
                            Console.ReadKey();
                            MyDiskMenuActions.MyDiskMenu(loggedUser);
                        }
                        else
                        {
                            Domain.Repositories.FolderRepositroy.DeleteFolderByName(loggedUser, folderNameToDelete);
                            Console.WriteLine("Folder uspjesno izbrisan.");
                            Console.ReadKey();
                            MyDiskMenuActions.MyDiskMenu(loggedUser);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Proces brisanja foldera je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }

            }
        }
    }
}
