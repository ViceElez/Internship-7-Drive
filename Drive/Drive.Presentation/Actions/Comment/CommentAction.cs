using Drive.Data.Entities.Models.Users;
using Drive.Domain.Repositories;
using Drive.Presentation.Actions.Menus;
using Drive.Presentation.Helper;

namespace Drive.Presentation.Actions.Comment
{
    public class CommentAction
    {
        public static void CommentSharedMenu(User loggedUser, int fileId, int? currentFolderId)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Komentari:");
                var listOfComments = CommentRepository.GetAllComments(fileId);
                if(listOfComments == null || listOfComments.Count == 0)
                {
                    Console.WriteLine("Nema komentara.");
                }
                else
                {
                    foreach (var comment in CommentRepository.GetAllComments(fileId))
                    {
                        Console.WriteLine($"{comment.Id} - {comment.User.Email} - {comment.CreatedAt}\n{comment.Content}\n");
                    }
                }
                Console.WriteLine("\nUpisite komandu za rad s komentarima('help' za ispisat sve komande):");
                var commadOption = Console.ReadLine().Trim().ToLower();
                switch (commadOption)
                {
                    case "dodaj komentar":
                        AddComment(loggedUser, fileId, currentFolderId);
                        break;
                    case "uredi komentar":
                        EditComment(loggedUser, fileId, currentFolderId);
                        break;
                    case "izbrisi komentar":
                        DeleteComment(loggedUser, fileId, currentFolderId);
                        break;
                    case "izlaz":
                        SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
                        break;
                    case "help":
                        InputValidation.ListAllCommentFunctions();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda. Zelite li vidjeti popis svih komandi? (da/ne)");
                        while (true)
                        {
                            var showHelp = Console.ReadLine().Trim().ToLower();
                            if (showHelp == "da")
                            {
                                Helper.InputValidation.ListAllCommentFunctions();
                                break;
                            }
                            else if (showHelp == "ne")
                            {
                                Console.WriteLine("Ponovo unesite komandu.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Nepostojeca komanda.");
                                break;
                            }
                        }
                        break;
                }
            }
            
        }
        public static void CommentMenu(User loggedUser, int fileId, int? currentFolderId)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Komentari:");
                var listOfComments = CommentRepository.GetAllComments(fileId);
                if (listOfComments == null || listOfComments.Count == 0)
                {
                    Console.WriteLine("Nema komentara.");
                }
                else
                {
                    foreach (var comment in CommentRepository.GetAllComments(fileId))
                    {
                        Console.WriteLine($"{comment.Id} - {comment.User.Email} - {comment.CreatedAt}\n{comment.Content}\n");
                    }
                }
                Console.WriteLine("\nUpisite komandu za rad s komentarima('help' za ispisat sve komande):");
                var commadOption = Console.ReadLine().Trim().ToLower();
                switch (commadOption)
                {
                    case "dodaj komentar":
                        AddComment(loggedUser, fileId, currentFolderId);
                        break;
                    case "uredi komentar":
                        EditComment(loggedUser, fileId, currentFolderId);
                        break;
                    case "izbrisi komentar":
                        DeleteComment(loggedUser, fileId, currentFolderId);
                        break;
                    case "izlaz":
                        MyDiskMenuActions.MyDiskMenu(loggedUser, currentFolderId);
                        break;
                    case "help":
                        InputValidation.ListAllCommentFunctions();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda. Zelite li vidjeti popis svih komandi? (da/ne)");
                        while (true)
                        {
                            var showHelp = Console.ReadLine().Trim().ToLower();
                            if (showHelp == "da")
                            {
                                Helper.InputValidation.ListAllCommentFunctions();
                                break;
                            }
                            else if (showHelp == "ne")
                            {
                                Console.WriteLine("Ponovo unesite komandu.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Nepostojeca komanda.");
                                break;
                            }
                        }
                        break;
                }
            }

        }
        public static void AddComment(User loggedUser, int fileId,int? currentFolderId)
        {
            Console.WriteLine("Unesite komentar:");
            var content = Console.ReadLine().Trim();
            CommentRepository.AddComment(loggedUser, fileId, content);
            CommentMenu(loggedUser, fileId, currentFolderId);
        }
        public static void EditComment(User loggedUser, int fileId, int? currentFolderId)
        {
            Console.WriteLine("Unesite id komentara koji zelite urediti:");
            var commentId = InputValidation.CommentIdValidation(loggedUser,currentFolderId,fileId);
            Console.WriteLine("Unesite novi sadrzaj komentara:");
            var content = Console.ReadLine().Trim();
            CommentRepository.EditComment(fileId, commentId, content);
            CommentMenu(loggedUser,fileId,currentFolderId);
        }
        public static void DeleteComment(User loggedUser, int fileId, int? currentFolderId)
        {
            Console.WriteLine("Unesite id komentara koji zelite izbrisati:");
            var commentId = InputValidation.CommentIdValidation(loggedUser, currentFolderId, fileId);
            CommentRepository.DeleteComments(fileId, commentId);
            CommentMenu(loggedUser, fileId, currentFolderId);
        }
    }
}
