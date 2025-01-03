using Drive.Data.Entities.Models.Users;
using Drive.Domain.Repositories;

namespace Drive.Presentation.Actions.Menus
{
    public class SharedWithMeMenuActions
    {
        public static void SharedDiskMenu(User loggedUser,int? currentFolderId)
        {
            while (true)
            {
                Console.Clear();
                var currentFolder = FolderRepositroy.GetSharedFolderById(loggedUser,currentFolderId);
                if (currentFolder == null)
                    Console.WriteLine("Nalazite se u pocetnom folderu.\n");
                else
                    Console.WriteLine($"Nalazite se u {currentFolder.Name} folderu.\n");

                var folders = FolderRepositroy.ListAllSharedFolders(loggedUser, currentFolderId);
                if (folders != null)
                {
                    Console.WriteLine("Vasi folderi su:");
                    foreach (var folder in folders)
                    {
                        Console.WriteLine($"{folder.Id} - {folder.Name}");
                    }
                }
                else
                    Console.WriteLine("Nemate foldera u ovom folderu");

                Console.WriteLine();
                var files = FileRepository.ListAllSharedFiles(loggedUser, currentFolderId);
                if (files != null)
                {
                    Console.WriteLine("\nVasi file-ovi su:");
                    foreach (var file in files)
                    {
                        Console.WriteLine($"{file.Id} - {file.Name}");
                    }
                }
                else
                    Console.WriteLine("\nNemate file-ova u ovom folderu");

                Console.WriteLine("\nUpisite komandu za rad s datotekama (ili upisite 'help' za popis komandi):");
                var commandOption = Console.ReadLine().Trim().ToLower();
                switch (commandOption)
                {
                    case "udi u mapu":
                        Drive.Presentation.Actions.Folder.FolderActions.EnterSharedFolder(loggedUser, currentFolderId);
                        break;
                    case "izbrisi mapu":
                        Drive.Presentation.Actions.Folder.FolderActions.DeleteSharedFolder(loggedUser, currentFolderId);
                        break;
                    case "izbrisi datoteku":
                        Drive.Presentation.Actions.File.FileActions.DeleteSharedFile(loggedUser, currentFolderId);
                        break;
                    case "uredi datoteku":
                        Drive.Presentation.Actions.File.FileActions.EditSharedFileContent(loggedUser, currentFolderId);
                        break;
                    case "help":
                        Helper.InputValidation.ListAllSharedFunctions();
                        break;
                    case "povratak":
                        DriveMenuActions.DriveMenu(loggedUser);
                        return;

                    default:
                        Console.WriteLine("Nepostojeca komanda. Zelite li vidjeti popis svih komandi? (da/ne)");
                     while (true)
                        {
                            var showHelp = Console.ReadLine().Trim().ToLower();

                            if (showHelp == "da")
                            {
                                Helper.InputValidation.ListAllSharedFunctions();
                                break;
                            }
                            else if (showHelp == "ne")
                            {
                                Console.WriteLine("Ponovo unesite komandu.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Molimo unesite 'da' ili 'ne'.");
                            }
                        }
                        break;
                }
            }
            
        }
    }
}
