using Drive.Data.Entities.Models.Users;
using Drive.Presentation.Actions.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Presentation.Actions.File
{
    public class FileActions
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
        public static void DeleteFIle(User loggedUser)
        {
            Console.WriteLine("Upisite ime file-a kojeg zelite izbrisati:");
            var fileNameToDelete = Console.ReadLine().Trim();
            while (true)
            {
                if (string.IsNullOrEmpty(fileNameToDelete))
                {
                    Console.WriteLine("Ime file-a ne moze biti prazno.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite ime file-a:");
                        fileNameToDelete = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces brisanja file-a je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else if (!Drive.Domain.Repositories.FileRepository.CheckIfFileExistByName(loggedUser, fileNameToDelete))
                {
                    Console.WriteLine("Ne postoji file s time imenom.");
                    var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                    if (confirmForFolderName)
                    {
                        Console.Write("Unesite ime file-a:");
                        fileNameToDelete = Console.ReadLine().Trim();
                    }
                    else
                    {
                        Console.WriteLine("Proces brisanja file-a je prekinut.");
                        Console.ReadKey();
                        MyDiskMenuActions.MyDiskMenu(loggedUser);
                    }
                }
                else
                    break;

                if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileNameToDelete)>1)
                {
                    Console.WriteLine("Postoji vise file-ova s tim imenom.");
                    Domain.Repositories.FileRepository.ListAllFilesWithSameName(loggedUser,fileNameToDelete);
                    Console.Write("Unesite id file-a koji zelite izbrisati:");
                    var isIdCorrect = int.TryParse(Console.ReadLine(), out var IdOfFileToDelete);
                    while (true)
                    {
                        if (!Drive.Domain.Repositories.FileRepository.CheckIfFileExistById(loggedUser, IdOfFileToDelete))
                        {
                            Console.WriteLine("Ne postoji file s tim id-om.");
                            var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                            if (confirmForFolderName)
                            {
                                Console.Write("Unesite id file-a:");
                                IdOfFileToDelete = int.Parse(Console.ReadLine());
                            }
                            else
                            {
                                Console.WriteLine("Proces brisanja file-a je prekinut.");
                                Console.ReadKey();
                                MyDiskMenuActions.MyDiskMenu(loggedUser);
                            }
                        }
                        else if (!isIdCorrect)
                        {
                            Console.WriteLine("Uneseni id mora biti broj.");
                            var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                            if (confirmForFolderName)
                            {
                                Console.Write("Unesite id file-a:");
                                isIdCorrect = int.TryParse(Console.ReadLine(), out IdOfFileToDelete);
                            }
                            else
                            {
                                Console.WriteLine("Proces brisanja file-a je prekinut.");
                                Console.ReadKey();
                                MyDiskMenuActions.MyDiskMenu(loggedUser);
                            }
                        }
                        else if (IdOfFileToDelete <= 0)
                        {
                            Console.WriteLine("Id file-a mora biti veci od 0.");
                            var confirmForFolderName = Helper.InputValidation.ConfirmAndDelete();
                            if (confirmForFolderName)
                            {
                                Console.Write("Unesite id file-a:");
                                isIdCorrect = int.TryParse(Console.ReadLine(), out IdOfFileToDelete);
                            }
                            else
                            {
                                Console.WriteLine("Proces brisanja file-a je prekinut.");
                                Console.ReadKey();
                                MyDiskMenuActions.MyDiskMenu(loggedUser);
                            }
                        }
                        else
                            break;

                        if (Helper.InputValidation.ConfirmAndDelete())
                        {
                            if (Domain.Repositories.FileRepository.ReturnTheNumberOfFilesWithSamename(loggedUser, fileNameToDelete) > 1)
                            {
                                Domain.Repositories.FileRepository.DeleteFileById(loggedUser, IdOfFileToDelete);
                                Console.WriteLine("File uspjesno izbrisan.");
                                Console.ReadKey();
                                MyDiskMenuActions.MyDiskMenu(loggedUser);
                            }
                            else
                            {
                                Domain.Repositories.FileRepository.DeleteFileByName(loggedUser, fileNameToDelete);
                                Console.WriteLine("File uspjesno izbrisan.");
                                Console.ReadKey();
                                MyDiskMenuActions.MyDiskMenu(loggedUser);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Proces brisanja foldera je prekinut.");
                            Console.ReadKey();
                            MyDiskMenuActions.MyDiskMenu(loggedUser);
                        }
                    }
                }
            }
        }
    }
}
