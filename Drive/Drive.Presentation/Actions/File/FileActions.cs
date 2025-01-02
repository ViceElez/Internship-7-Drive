using Drive.Data.Entities.Models.Users;
using Drive.Presentation.Actions.Menus;
using Drive.Presentation.Helper;

namespace Drive.Presentation.Actions.File
{
    public class FileActions
    {
        public static void CreateFile(User loggedUser, int? currentFolderId)
        {
            Console.Write("Upisite ime file-a:");
            var fileName = Console.ReadLine().Trim();
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
                        MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else
                    break;

            }
            if (Helper.InputValidation.ConfirmAndDelete())
            {
                Drive.Domain.Repositories.FileRepository.AddFile(loggedUser, fileName, currentFolderId);
                Console.WriteLine("File uspjesno kreirana.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
            }
            else
            {
                Console.WriteLine("Proces kreiranja file-a je prekinut.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
            }
        }
        public static void DeleteFile(User loggedUser, int? currentFolderId)
        {
            Console.WriteLine("Upisite ime file-a kojeg zelite izbrisati:");
            var fileNameToDelete = Helper.InputValidation.FileNameValidation(loggedUser, currentFolderId);
            var fileIdToDelete = 0;
            if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileNameToDelete) > 1)
                fileIdToDelete = Helper.InputValidation.FileIdValidation(loggedUser, fileNameToDelete, currentFolderId);

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileNameToDelete) == 1)
                    fileIdToDelete = Domain.Repositories.FileRepository.GetFileId(loggedUser, fileNameToDelete);
                Domain.Repositories.FileRepository.DeleteFile(loggedUser, fileIdToDelete, fileNameToDelete);
                Console.WriteLine("File uspjesno izbrisan.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
            }
            else
            {
                Console.WriteLine("Proces je prekinut.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
            }
        }
        public static void ChangeFileName(User loggedUser, int? currentFolderId)
        {
            Console.Write("Upisite ime file-a kojem zelite promijeniti ime:");
            var fileName = Helper.InputValidation.FileNameValidation(loggedUser, currentFolderId);

            var IdOfFile = 0;
            if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileName) > 1)
                IdOfFile = Helper.InputValidation.FileIdValidation(loggedUser, fileName, currentFolderId);

            Console.Write("Upisite novo ime file-a:");
            var newFileName = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(newFileName))
                {
                    Console.WriteLine("Ime nemoze biti prazno");
                    var confirmForFileName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFileName)
                    {
                        Console.Write("Unesite ime:");
                        newFileName = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else if (newFileName == fileName)
                {
                    Console.WriteLine("Nemozete promijeniti ime u isto.");
                    var confirmForFileName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFileName)
                    {
                        Console.Write("Unesite ime:");
                        newFileName = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else
                    break;
            }

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileName) == 1)
                    IdOfFile = Domain.Repositories.FileRepository.GetFileId(loggedUser, fileName);
                Domain.Repositories.FileRepository.ChangeFileName(loggedUser, fileName, newFileName, IdOfFile);
                Console.WriteLine("File-u uspjesno promjenjeno ime.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
            }
            else
            {
                Console.WriteLine("Proces je prekinut.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
            }


        }
        public static void EditFileContent(User loggedUser, int? currentFolderId)
        {
            Console.WriteLine("Upisite ime file-a koji zelite urediti:");
            var fileName = Helper.InputValidation.FileNameValidation(loggedUser, currentFolderId);

            var fileId = 0;
            if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileName) > 1)
                fileId = Helper.InputValidation.FileIdValidation(loggedUser, fileName, currentFolderId);

            if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileName) == 1)
                fileId = Domain.Repositories.FileRepository.GetFileId(loggedUser, fileName);

            Console.Clear();
            var fileToEdit = Domain.Repositories.FileRepository.GetFile(loggedUser, fileName, fileId);
            Console.WriteLine($"Uredivanje datoteke: {fileToEdit.Name}");
            Console.WriteLine($"\nTrenutni tekst: {fileToEdit.Text}");

            List<string> lines = new List<string>(fileToEdit.Text.Split('\n'));
            string currentLine = string.Empty;

            Console.WriteLine("Unesite tekst za spremanje. Koristite ':help' za popis komandi.");

            while (true)
            {
                var keyInfo = Console.ReadKey(intercept: true);
                char keyChar = keyInfo.KeyChar;

                if (keyChar == ':')
                {
                    Console.WriteLine();
                    string command = Console.ReadLine();

                    switch (command.ToLower().Trim())
                    {
                        case "help":
                            InputValidation.ListAllEditFileFunctions();
                            break;

                        case "spremanje i izlaz":
                            Domain.Repositories.FileRepository.SavingAEditedFile(loggedUser, fileName, fileId, lines);
                            Console.WriteLine("Datoteka je spremljena.");
                            Console.ReadKey();
                            return;

                        case "izlaz bez spremanja":
                            Console.WriteLine("Izlaz bez spremanja.");
                            Console.ReadKey();
                            return;
                        case "otvori komentare":
                            Drive.Presentation.Actions.Comment.CommentAction.CommentMenu(loggedUser, fileId, currentFolderId);
                            break;

                        default:
                            Console.WriteLine($"Nepoznata komanda: {command}");
                            Console.ReadKey();
                            break;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    lines = Domain.Repositories.FileRepository.HandlingBackspaceFileEditings(loggedUser, fileName, fileId, lines, ref currentLine);
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    lines = Domain.Repositories.FileRepository.HandlingEnterFileEditings(loggedUser, fileName, fileId, lines, ref currentLine);
                }
                else
                {
                    currentLine += keyChar;
                    Console.Write(keyChar);
                }
            }
        }
        public static void ShareFile(User loggedUser, int? currentFolderId)
        {
            Console.WriteLine("Upisite ime file-a koji zelite podijeliti:");
            var fileName = Helper.InputValidation.FileNameValidation(loggedUser, currentFolderId);

            var fileId = 0;
            if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileName) > 1)
                fileId = Helper.InputValidation.FileIdValidation(loggedUser, fileName, currentFolderId);

            if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileName) == 1)
                fileId = Domain.Repositories.FileRepository.GetFileId(loggedUser, fileName);

            Domain.Repositories.UserRepository.ListAllUsers();
            while (true)
            {
                Console.Write("Upisite email korisnika kojem zelite podijeliti file(kada dodate sve zeljene korisnike upisite 'stop'):");
                var userEmail = Console.ReadLine().Trim();
                if (userEmail.ToLower().Trim() == "stop")
                    break;
                else
                {
                    while (true)
                    {
                        if (string.IsNullOrEmpty(userEmail))
                        {
                            Console.WriteLine("Email nemoze biti prazan.");
                            var confirmForEmail = Helper.InputValidation.ConfirmAndDelete();
                            if (confirmForEmail)
                            {
                                Console.Write("Unesite email:");
                                userEmail = Console.ReadLine().Trim();
                            }
                            else
                            {
                                Console.WriteLine("Proces je prekinut.");
                                Console.ReadKey();
                                MainMenuActions.MainMenu();
                            }
                        }
                        else if (!Helper.InputValidation.IsValid(userEmail))
                        {
                            Console.WriteLine("Neipsravan format email-a.");
                            var confirmForEmail = Helper.InputValidation.ConfirmAndDelete();
                            if (confirmForEmail)
                            {
                                Console.Write("Unesite email:");
                                userEmail = Console.ReadLine().Trim();
                            }
                            else
                            {
                                Console.WriteLine("Proces je prekinut.");
                                Console.ReadKey();
                                MainMenuActions.MainMenu();
                            }
                        }
                        else if (!Drive.Domain.Repositories.UserRepository.EmailExists(userEmail))
                        {
                            Console.WriteLine("Ne postoji racun s tim emailom");
                            var confirmForEmail = Helper.InputValidation.ConfirmAndDelete();
                            if (confirmForEmail)
                            {
                                Console.Write("Unesite email:");
                                userEmail = Console.ReadLine().Trim();
                            }
                            else
                            {
                                Console.WriteLine("Proces je prekinut.");
                                Console.ReadKey();
                                MainMenuActions.MainMenu();
                            }

                        }
                        else if (userEmail == loggedUser.Email)
                        {
                            Console.WriteLine("Ne mozete podijeliti file sami sa sobom.");
                            var confirmForEmail = Helper.InputValidation.ConfirmAndDelete();
                            if (confirmForEmail)
                            {
                                Console.Write("Unesite email:");
                                userEmail = Console.ReadLine().Trim();
                            }
                            else
                            {
                                Console.WriteLine("Proces je prekinut.");
                                Console.ReadKey();
                                MainMenuActions.MainMenu();
                            }
                        }
                        else
                        {
                            if (Domain.Repositories.FileRepository.ShareFile(loggedUser, fileId, userEmail))
                            {
                                Console.WriteLine("File uspjesno podijeljen.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("File je vec podijeljen s tim korisnikom.");
                                Console.ReadKey();
                                break;
                            }
                        }
                    }
                }

            }
            MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);

        }
        public static void StopSharingFile(User loggedUser, int? currentFolderId)
        {
            Console.Write("Upisite ime file-a koji zelite prestati dijeliti:");
            var fileName = Helper.InputValidation.FileNameValidation(loggedUser, currentFolderId);

            var fileId = 0;
            if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileName) > 1)
                fileId = Helper.InputValidation.FileIdValidation(loggedUser, fileName, currentFolderId);

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileName) == 1)
                    fileId = Domain.Repositories.FileRepository.GetFileId(loggedUser, fileName);
                Domain.Repositories.FileRepository.StopSharingFile(loggedUser, fileId);
                Console.WriteLine("File se uspjesno prestao dijeliti.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
            }
            else
            {
                Console.WriteLine("Proces je prekinut.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
            }
        }
        public static void DeleteSharedFile(User loggedUser, int? currentFolderId)
        {
            Console.Write("Upisite ime file-a koji zelite izbrisati:");
            var fileName = Helper.InputValidation.SharedFileNameValidation(loggedUser, currentFolderId);

            var fileId = 0;
            if (Domain.Repositories.FileRepository.ReturnTheNumberOfSharedFilesWithSamename(loggedUser, fileName) > 1)
                fileId = Helper.InputValidation.SharedFileIdValidation(loggedUser, fileName, currentFolderId);

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                if (Domain.Repositories.FileRepository.ReturnTheNumberOfSharedFilesWithSamename(loggedUser, fileName) == 1)
                    fileId = Domain.Repositories.FileRepository.GetSharedFileId(loggedUser, fileName);
                Domain.Repositories.FileRepository.DeleteSharedFile(loggedUser, fileId);
                Console.WriteLine("File uspjesno izbrisan.");
                Console.ReadKey();
                SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
            }
            else
            {
                Console.WriteLine("Proces je prekinut.");
                Console.ReadKey();
                SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
            }


        }
        public static void EditSharedFileContent(User loggedUser, int? currentFolderId)
        {
            Console.Write("Upisite ime file-a koji zelite urediti:");
            var fileName = Helper.InputValidation.SharedFileNameValidation(loggedUser, currentFolderId);

            var fileId = 0;
            if (Domain.Repositories.FileRepository.ReturnTheNumberOfSharedFilesWithSamename(loggedUser, fileName) > 1)
                fileId = Helper.InputValidation.SharedFileIdValidation(loggedUser, fileName, currentFolderId);

            if (Domain.Repositories.FileRepository.ReturnTheNumberOfSharedFilesWithSamename(loggedUser, fileName) == 1)
                fileId = Domain.Repositories.FileRepository.GetSharedFileId(loggedUser, fileName);

            Console.Clear();
            var fileToEdit = Domain.Repositories.FileRepository.GetSharedFile(loggedUser, fileName,fileId);
            Console.WriteLine($"Uredivanje datoteke: {fileToEdit.Name}");
            Console.WriteLine($"\nTrenutni tekst: {fileToEdit.Text}");

            List<string> lines = new List<string>(fileToEdit.Text.Split('\n'));
            string currentLine = string.Empty;

            Console.WriteLine("Unesite tekst za spremanje. Koristite ':help' za popis komandi.");

            while (true)
            {
                var keyInfo = Console.ReadKey(intercept: true);
                char keyChar = keyInfo.KeyChar;

                if (keyChar == ':')
                {
                    Console.WriteLine();
                    string command = Console.ReadLine();

                    switch (command.ToLower().Trim())
                    {
                        case "help":
                            InputValidation.ListAllEditSharedFileFunctions();
                            break;

                        case "spremanje i izlaz":
                            Domain.Repositories.FileRepository.SavingASharedEditedFile(loggedUser, fileName, fileId, lines);
                            Console.WriteLine("Datoteka je spremljena.");
                            Console.ReadKey();
                            return;

                        case "izlaz bez spremanja":
                            Console.WriteLine("Izlaz bez spremanja.");
                            Console.ReadKey();
                            return;
                        case "otvori komentare":
                            Drive.Presentation.Actions.Comment.CommentAction.CommentSharedMenu(loggedUser, fileId,currentFolderId);
                            break;

                        default:
                            Console.WriteLine($"Nepoznata komanda: {command}");
                            Console.ReadKey();
                            break;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    lines = Domain.Repositories.FileRepository.HandlingBackspaceFileEditings(loggedUser, fileName, fileId, lines, ref currentLine);
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    lines = Domain.Repositories.FileRepository.HandlingEnterFileEditings(loggedUser, fileName, fileId, lines, ref currentLine);
                }
                else
                {
                    currentLine += keyChar;
                    Console.Write(keyChar);
                }
            }

        }
    }
}
