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
            Console.WriteLine($"Zelite li kreirati novu mapu pod nazivom:{folderName}");
            var answer = Console.ReadLine().Trim().ToLower();
            do
            {
                if (answer == "da")
                {
                    Drive.Domain.Repositories.FolderRepositroy.CreateFolder(folderName, loggedUser);
                    Console.WriteLine("Mapa uspjesno kreirana.");
                    Console.ReadKey();
                    MyDiskMenuActions.MyDiskMenu(loggedUser);
                }
                else if (answer == "ne")
                {
                    Console.WriteLine("Proces kreiranja mape je prekinut.");
                    Console.ReadKey();
                    MyDiskMenuActions.MyDiskMenu(loggedUser);
                }
                else
                {
                    Console.WriteLine("Unesite da ili ne.");
                    answer = Console.ReadLine().Trim().ToLower();
                }

            } while (answer != "da" && answer != "ne");

        }
    }
}
