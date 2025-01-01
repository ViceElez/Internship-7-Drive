using Drive.Data.Entities.Models.Users;
using Drive.Domain.Repositories;
using Drive.Presentation.Actions.Menus;
using Drive.Presentation.Helper;

namespace Drive.Presentation.Actions.Comment
{
    public class CommentAction
    {
        public static void CommentMenu(User loggedUser, int fileId, int? currentFolderId)
        {
            while (true)
            {
                Console.Clear();
                foreach (var comment in CommentRepository.ListAllComments(fileId))
                {
                    Console.WriteLine($"{comment.User.Id} - {comment.User.Email} - {comment.CreatedAt}");
                    Console.WriteLine($"{comment.Content}\n");
                }
                Console.WriteLine("Upisite komandu za rad s komentarima:");
                var commadOption = Console.ReadLine().Trim().ToLower();
                switch (commadOption)
                {
                    case "dodaj komentar":
                        //implementacija
                        break;
                    case "uredi komentar":
                        //implementacija
                        break;
                    case "izbrisi komentar":
                        //implementacija
                        break;
                    case "izlaz":
                       // Drive.Presentation.Actions.Menus.SharedWithMeMenuActions.SharedDiskMenu(loggedUser,currentFolderId);
                        return;

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
            SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
        }
        public static void EditComment(User loggedUser, int fileId, int? currentFolderId)
        {
            Console.WriteLine("Unesite id komentara koji zelite urediti:");
            var commentId = InputValidation.CommentIdValidation(loggedUser,currentFolderId,fileId);
            Console.WriteLine("Unesite novi sadrzaj komentara:");
            var content = Console.ReadLine().Trim();
            CommentRepository.EditComment(fileId, commentId, content);
            SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
        }
        public static void DeleteComment(User loggedUser, int fileId, int? currentFolderId)
        {
            Console.WriteLine("Unesite id komentara koji zelite izbrisati:");
            var commentId = InputValidation.CommentIdValidation(loggedUser, currentFolderId, fileId);
            CommentRepository.DeleteComments(fileId, commentId);
            SharedWithMeMenuActions.SharedDiskMenu(loggedUser, currentFolderId);
        }
    }
}
