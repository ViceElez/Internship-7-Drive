using Drive.Data.Entities.Models.Users;
using Drive.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Drive.Presentation.Actions.Menus
{
    public class MyDiskMenuActions
    {
        public static void MyDiskMenu(User loggedUser, int? currentFolderId)
        {
            while (true)
            {
                Console.Clear();
                var currentFolder = FolderRepositroy.GetFolderById(loggedUser, currentFolderId);
                if (currentFolder == null)
                    Console.WriteLine("Nalazite se u pocetnom folderu.\n");
                else
                    Console.WriteLine($"Nalazite se u {currentFolder.Name} folderu.\n");

                var folders = FolderRepositroy.ListAllFolders(loggedUser, currentFolderId);
                if (folders != null)
                {
                    Console.WriteLine("Vasi folderi su:");
                    foreach (var folder in folders)
                    {
                        Console.WriteLine($"{folder.Id} - {folder.Name}");
                    }
                }
                else
                    Console.WriteLine("\nNemate foldera u ovom folderu");
                Console.WriteLine();
                var files = FileRepository.ListAllFiles(loggedUser, currentFolderId);
                if (files != null)
                {
                    Console.WriteLine("\nVasi file-ovi su:");
                    foreach (var file in files)
                    {
                        Console.WriteLine($"{file.Id} - {file.Name}");
                    }
                }
                else
                    Console.WriteLine("Nemate file-ova u ovom folderu");

                Console.WriteLine("\nUpisite komandu za rad s datotekama (ili upisite 'help' za popis komandi):");
                var commandOption = Console.ReadLine().Trim().ToLower();

                switch (commandOption)
                {
                    case "stvori mapu":
                        Drive.Presentation.Actions.Folder.FolderActions.CreateFolder(loggedUser, currentFolderId);
                        break;

                    case "stvori datoteku":
                        Drive.Presentation.Actions.File.FileActions.CreateFile(loggedUser, currentFolderId);
                        break;

                    case "udi u mapu":
                        Drive.Presentation.Actions.Folder.FolderActions.EnterFolder(loggedUser, currentFolderId);
                        break;

                    case "uredi datoteku":
                        Drive.Presentation.Actions.File.FileActions.EditFileContent(loggedUser, currentFolderId);
                        break;

                    case "izbrisi mapu":
                        Drive.Presentation.Actions.Folder.FolderActions.DeleteFolder(loggedUser, currentFolderId);
                        break;

                    case "izbrisi datoteku":
                        Drive.Presentation.Actions.File.FileActions.DeleteFile(loggedUser, currentFolderId);
                        break;

                    case "promijeni naziv mape":
                        Drive.Presentation.Actions.Folder.FolderActions.ChangeFolderName(loggedUser, currentFolderId);
                        break;

                    case "promijeni naziv datoteke":
                        Drive.Presentation.Actions.File.FileActions.ChangeFileName(loggedUser, currentFolderId);
                        break;

                    case "podijeli datoteku":
                        Drive.Presentation.Actions.File.FileActions.ShareFile(loggedUser, currentFolderId);
                        break;

                    case "podijeli mapu":
                        Drive.Presentation.Actions.Folder.FolderActions.ShareFolder(loggedUser, currentFolderId);
                        break;

                    case "prestani dijelit mapu":
                        Drive.Presentation.Actions.Folder.FolderActions.StopSharingFolder(loggedUser, currentFolderId);
                        break;

                    case "prestani dijelit datoteku":
                        Drive.Presentation.Actions.File.FileActions.StopSharingFile(loggedUser, currentFolderId);
                        break;

                    case "help":
                        Helper.InputValidation.ListAllFunctions();
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
                                Helper.InputValidation.ListAllFunctions();
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
