using Drive.Data.Entities.Models.Users;
using Drive.Presentation.Actions.Menus;

namespace Drive.Presentation.Actions.Folder
{
    public class FolderActions //malo pogledaj ovi file i folder ka jesan li sta krivo napisa ka za file folder ili obrnuto
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
            var folderName=Helper.InputValidation.FolderNameValidation(loggedUser);

            var folderId = 0;
            if (Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser, folderName) > 1)
                folderId = Helper.InputValidation.FolderIdValidation(loggedUser, folderName);

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                Domain.Repositories.FolderRepositroy.DeleteFolder(loggedUser, folderId,folderName);
                Console.WriteLine("Folder uspjesno izbrisan.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser);
            }
            else
            {
                Console.WriteLine("Proces je prekinut.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser);
            }
        }
        public static void ChangeFolderName(User loggedUser)
        {
            Console.Write("Upisite ime foldera kojem zelite promijeniti ime:");
            var folderName = Helper.InputValidation.FolderNameValidation(loggedUser);

            var folderId = 0;
            if (Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser, folderName) > 1)
                folderId = Helper.InputValidation.FolderIdValidation(loggedUser, folderName);

            Console.WriteLine("Upisite novo ime za folder:");
            var newFolderName = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(newFolderName))
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
                else
                    break;
            }

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                    Domain.Repositories.FolderRepositroy.ChangeFolderName(loggedUser,folderName,newFolderName, folderId);
                    Console.WriteLine("Folderu uspjesno promjenjeno ime.");
                    Console.ReadKey();
                    MyDiskMenuActions.MyDiskMenu(loggedUser);
            }
            else
            {
                Console.WriteLine("Proces je prekinut.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser);
            }
        }
        
    }
}
