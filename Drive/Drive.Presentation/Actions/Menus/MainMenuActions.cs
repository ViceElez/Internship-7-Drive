using Drive.Presentation.Actions.LogIn;
using Drive.Presentation.Actions.SignIn;

namespace Drive.Presentation.Actions.Menus
{
    public class MainMenuActions
    {

        public static void MainMenu()
        {
            string[] options = new string[] { "Prijavi se", "Registriraj se", "Izlaz" };
            var menu = new DriveMenu(options);
            int selectedOption = menu.Run();



            switch (selectedOption)
            {
                case 0:
                    Console.Clear();
                    LogInAction.UserLogInAction();
                    break;
                case 1:
                    Console.Clear();
                    SignInAction.UserSignInAction();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }


        }
    }
}
