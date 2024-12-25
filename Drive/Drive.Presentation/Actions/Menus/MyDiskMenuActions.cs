using Drive.Data.Entities.Models.Users;

namespace Drive.Presentation.Actions.Menus
{
    public class MyDiskMenuActions
    {
        public static void MyDiskMenu(User loggedUser)
        {
            while (true)
            {
                Console.Clear();
                //izlistat sve
                Console.WriteLine("Upisite komandu za rad s datotekama (ili upisite 'help' za popis komandi):");
                var commandOption = Console.ReadLine().Trim().ToLower();

                switch (commandOption)
                {
                    case "stvori mapu":
                        Console.Write("Upisite ime mape koju zelite stvoriti: ");
                        var createdFolder = Console.ReadLine().Trim();
                        // Implementacija stvaranja mape
                        break;

                    case "stvori datoteku":
                        Console.Write("Upisite ime datoteke koju zelite stvoriti: ");
                        var createdFile = Console.ReadLine().Trim();
                        // Implementacija stvaranja datoteke
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
                        Console.Write("Upisite ime mape koju zelite izbrisati: ");
                        var deleteFolder = Console.ReadLine().Trim();
                        // Implementacija brisanja mape
                        break;

                    case "izbrisi datoteku":
                        Console.Write("Upisite ime datoteke koju zelite izbrisati: ");
                        var deleteFile = Console.ReadLine().Trim();
                        // Implementacija brisanja datoteke
                        break;

                    case "promijeni naziv mape":
                        Console.Write("Upisite ime mape kojoj zelite promijeniti naziv: ");
                        var renameFolder = Console.ReadLine().Trim();
                        // Implementacija promjene naziva mape
                        break;

                    case "promijeni naziv datoteke":
                        Console.Write("Upisite ime datoteke kojoj zelite promijeniti naziv: ");
                        var renameFile = Console.ReadLine().Trim();
                        // Implementacija promjene naziva datoteke
                        break;

                    case "help":
                        Drive.Domain.Repositories.HelpRepositorycs.ListAllFunctions();
                        break;

                    case "povratak":
                        DriveMenuActions.DriveMenu(loggedUser);
                        return;

                    default:
                        Console.WriteLine("Nepostojeća komanda. Želite li vidjeti popis svih komandi? (da/ne)");

                        while (true)
                        {
                            var showHelp = Console.ReadLine().Trim().ToLower();

                            if (showHelp == "da")
                            {
                                Drive.Domain.Repositories.HelpRepositorycs.ListAllFunctions();
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
