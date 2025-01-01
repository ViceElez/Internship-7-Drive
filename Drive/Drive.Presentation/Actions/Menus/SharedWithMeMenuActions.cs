using Drive.Data.Entities.Models.Users;
using Drive.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Presentation.Actions.Menus
{
    public class SharedWithMeMenuActions
    {
        public static void SharedDiskMenu(User loggedUser,int? currentFolderId)
        {
            while (true)
            {
                Console.Clear();
                var currentFolder = FolderRepositroy.GetFolderById(currentFolderId);
                if (currentFolder == null)
                    Console.WriteLine("Nalazite se u pocetnom folderu.\n");
                else
                    Console.WriteLine($"Nalazite se u {currentFolder.Name} folderu.\n");
                Console.WriteLine("Vasi folderi su:");
                FolderRepositroy.ListAllSharedFolders(loggedUser, currentFolderId);
                Console.WriteLine();
                Console.WriteLine("Vasi file-ovi su:");
                FileRepository.ListAllSharedFiles(loggedUser, currentFolderId);
                Console.WriteLine("Upisite komandu za rad s datotekama (ili upisite 'help' za popis komandi):");
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

                        break;
                    case "navigacija":

                        break;
                    case "help":
                        Helper.InputValidation.ListAllSharedFunctions();
                        break;
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
