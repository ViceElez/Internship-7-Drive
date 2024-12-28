using Drive.Data.Entities.Models.Users;
using Drive.Domain.Repositories;

namespace Drive.Presentation.Actions.Menus
{
    public class MyDiskMenuActions
    {
        public static void MyDiskMenu(User loggedUser)
        {
            while (true)
            { //triba kad se folder dodaje da mu dan i parent folder al to implementiraj kad bude tribalo ulazit u mape, i nek se ispise u kojem folderu se nalazis
                Console.Clear();
                //Console.WriteLine($"Nalazite se u {} folderu\n");
                FolderRepositroy.ListAllFolders(loggedUser);
                FileRepository.ListAllFiles(loggedUser);
                Console.WriteLine("Upisite komandu za rad s datotekama (ili upisite 'help' za popis komandi):");
                var commandOption = Console.ReadLine().Trim().ToLower();

                switch (commandOption)
                {
                    case "stvori mapu":
                        Drive.Presentation.Actions.Folder.FolderActions.CreateFolder(loggedUser);
                        break;

                    case "stvori datoteku":
                        Drive.Presentation.Actions.File.FileActions.CreateFile(loggedUser);
                        break;

                    case "udi u mapu":
                        Console.Write("Upisite ime mape u koju želite uci: ");
                        var enterFolder = Console.ReadLine().Trim();
                        // Implementacija ulaska u mapu
                        break;

                    case "uredi datoteku":
                        Console.Write("Upisite ime datoteke koju zelite urediti: ");
                        var changeFile = Console.ReadLine().Trim();
                        // Implementacija uređivanja datoteke
                        break;

                    case "izbrisi mapu":
                         Drive.Presentation.Actions.Folder.FolderActions.DeleteFolder(loggedUser);
                        break;

                    case "izbrisi datoteku":
                        Drive.Presentation.Actions.File.FileActions.DeleteFile(loggedUser);
                        break;

                    case "promijeni naziv mape":
                        Drive.Presentation.Actions.Folder.FolderActions.ChangeFolderName(loggedUser);
                        break;

                    case "promijeni naziv datoteke":
                        Drive.Presentation.Actions.File.FileActions.ChangeFileName(loggedUser);
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
