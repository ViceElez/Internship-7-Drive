using Drive.Data.Entities.Models.Users;
using Drive.Domain.Repositories;

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
        
    }
}
