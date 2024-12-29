using Drive.Data.Entities.Models.Users;
using Drive.Presentation.Actions.Menus;
using System.Diagnostics.Eventing.Reader;

namespace Drive.Presentation.Actions.Folder
{
    public class FolderActions //akd upises za novo ime prazno onda vise nemos upisat jer ti uvik vrati isto
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
                if(Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser,folderName)==1)
                    folderId=Domain.Repositories.FolderRepositroy.GetFolderId(loggedUser,folderName);
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

            Console.Write("Upisite novo ime za folder:");
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
                        newFolderName = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else if (newFolderName==folderName)
                {
                    Console.WriteLine("Nemozete promijeniti ime foldera u isto.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite ime:");  
                        newFolderName = Console.ReadLine().Trim();
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
                if (Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser, folderName) == 1)
                    folderId = Domain.Repositories.FolderRepositroy.GetFolderId(loggedUser, folderName);
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
