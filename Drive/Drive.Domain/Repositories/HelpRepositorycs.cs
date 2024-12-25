
namespace Drive.Domain.Repositories
{
    public class HelpRepositorycs
    {
        public static void ListAllFunctions()
        {
            Console.WriteLine("1. Stvori mapu\n2. Stvori datoteku\n3. Udi u mapu\n4. Uredi datoteku" +
                "\n5. Izbrisi mapu\n6. Izbrisi datoteku\n7. Promijeni naziv mape\n8. Promijeni naziv datoteke\n9. Povratak");
            Console.ReadKey();
        }
    }
}
