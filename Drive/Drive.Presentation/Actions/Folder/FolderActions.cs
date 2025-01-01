using Drive.Data.Entities.Models.Users;
using Drive.Presentation.Actions.Menus;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace Drive.Presentation.Actions.Folder
{
    public class FolderActions 
    {
        public static void CreateFolder(User loggedUser, int? currentFolderId)
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
                        MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else
                {
                    break;
                }
            }

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                Drive.Domain.Repositories.FolderRepositroy.CreateFolder(folderName, loggedUser, currentFolderId);
                Console.WriteLine("Folder uspjesno kreirana.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
            }
            else
            {
                Console.WriteLine("Proces kreiranja foldera je prekinut.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
            }
        }
        public static void DeleteFolder(User loggedUser, int? currentFolderId)
        {
            Console.Write("Upisite ime foldera kojeg zelite izbrisati:");
            var folderName = Helper.InputValidation.FolderNameValidation(loggedUser, currentFolderId);

            var folderId = 0;
            if (Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser, folderName) > 1)
                folderId = Helper.InputValidation.FolderIdValidation(loggedUser, folderName, currentFolderId);

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                if (Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser, folderName) == 1)
                    folderId = Domain.Repositories.FolderRepositroy.GetFolderId(loggedUser, folderName);
                Domain.Repositories.FolderRepositroy.DeleteFolder(loggedUser, folderId, folderName);
                Console.WriteLine("Folder uspjesno izbrisan.");
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
        public static void ChangeFolderName(User loggedUser, int? currentFolderId)
        {
            Console.Write("Upisite ime foldera kojem zelite promijeniti ime:");
            var folderName = Helper.InputValidation.FolderNameValidation(loggedUser, currentFolderId);

            var folderId = 0;
            if (Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser, folderName) > 1)
                folderId = Helper.InputValidation.FolderIdValidation(loggedUser, folderName, currentFolderId);

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
                        MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else if (newFolderName == folderName)
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
                        MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                    }
                }
                else
                    break;
            }

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                if (Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser, folderName) == 1)
                    folderId = Domain.Repositories.FolderRepositroy.GetFolderId(loggedUser, folderName);
                Domain.Repositories.FolderRepositroy.ChangeFolderName(loggedUser, folderName, newFolderName, folderId);
                Console.WriteLine("Folderu uspjesno promjenjeno ime.");
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
        public static void EnterFolder(User loggedUser, int? currentFolderId)
        {
            Console.Write("Upisite ime foldera u koji zelite uci:");
            var folderName = Helper.InputValidation.FolderNameValidation(loggedUser, currentFolderId);

            var folderId = 0;
            if (Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser, folderName) > 1)
                folderId = Helper.InputValidation.FolderIdValidation(loggedUser, folderName, currentFolderId);

            if (Helper.InputValidation.ConfirmAndDelete())
            {
                if (Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser, folderName) == 1)
                    folderId = Domain.Repositories.FolderRepositroy.GetFolderId(loggedUser, folderName);
                Console.WriteLine("Uspjesan ulazak u folder.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser, folderId);

            }
            else
            {
                Console.WriteLine("Proces je prekinut.");
                Console.ReadKey();
                MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
            }
        }
        public static void ShareFolder(User loggedUser,int? currentFolderId)
        {
            Console.Write("Upisite ime foldera u koji zelite podijeliti:");
            var folderName = Helper.InputValidation.FolderNameValidation(loggedUser, currentFolderId);

            var folderId = 0;
            if (Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser, folderName) > 1)
                folderId = Helper.InputValidation.FolderIdValidation(loggedUser, folderName, currentFolderId);

            if (Domain.Repositories.FolderRepositroy.ReturnTheNumberOfFoldersWithSamename(loggedUser, folderName) == 1)
                    folderId = Domain.Repositories.FolderRepositroy.GetFolderId(loggedUser, folderName);

            Domain.Repositories.UserRepository.ListAllUsers();
            while (true)
            {
                Console.Write("Upisite email korisnika kojem zelite podijeliti folder(kada dodate sve zeljene korisnike upisite 'stop'):");
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
                            Console.WriteLine("Ne mozete podijeliti folder sami sa sobom.");
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
                            if (Domain.Repositories.FolderRepositroy.ShareFolder(loggedUser, folderId, userEmail))
                            {
                                Console.WriteLine("Folder uspjesno podijeljen.");
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

    }
}
