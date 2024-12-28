using Drive.Data.Entities.Models.Users;
using Drive.Presentation.Actions.Menus;

namespace Drive.Presentation.Actions.File
{
    public class FileActions // za ove file i foldere brisat triba stavit jednu funkciju samo al da gleda i ime i id filea ili foldera i user id
    {
        public static void CreateFile(User loggedUser)
        {
            Console.Write("Upisite ime file-a:");
            var fileName=Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("Ime file-a nemoze biti prazno.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite ime file-a:");
                        fileName = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces kreiranja file-a je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else
                    break;

            }
            if (Helper.InputValidation.ConfirmAndDelete())
            {
                Drive.Domain.Repositories.FileRepository.AddFile(loggedUser, fileName);
                Console.WriteLine("File uspjesno kreirana.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser);
            }
            else
            {
                Console.WriteLine("Proces kreiranja file-a je prekinut.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser);
            }
        }
        public static void DeleteFile(User loggedUser)
        {
            Console.WriteLine("Upisite ime file-a kojeg zelite izbrisati:");
            var fileNameToDelete = Helper.InputValidation.FileNameValidation(loggedUser);
            var fileIdToDelete = 0;
            if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileNameToDelete) > 1)
                fileIdToDelete = Helper.InputValidation.FileIdValidation(loggedUser, fileNameToDelete);

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                Domain.Repositories.FileRepository.DeleteFile(loggedUser, fileIdToDelete,fileNameToDelete);
                Console.WriteLine("File uspjesno izbrisan.");
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
        public static void ChangeFileName(User loggedUser)
        {
            Console.Write("Upisite ime file-a kojem zelite promijeniti ime:");
            var fileName=Helper.InputValidation.FileNameValidation( loggedUser);

            var IdOfFile = 0;
            if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileName) > 1)
                IdOfFile = Helper.InputValidation.FileIdValidation(loggedUser, fileName);

            Console.WriteLine("Upisite novo ime file-a:");
            var newFileName = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(newFileName))
                {
                    Console.WriteLine("Ime nemoze biti prazno");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite ime:");
                        fileName = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else if (newFileName==fileName)
                {
                    Console.WriteLine("Nemozete promijeniti ime u isto.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite ime:");
                        fileName = Console.ReadLine().Trim();
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
                Domain.Repositories.FileRepository.ChangeFileName(loggedUser, fileName,newFileName, IdOfFile);
                Console.WriteLine("File-u uspjesno promjenjeno ime.");
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
