using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Drive.Presentation.Helper
{
    public static class InputValidation
    {
        public static bool ConfirmAndDelete()
        {
            var answer = string.Empty;
            while (true)
            {
                Console.Write("Dali zelite nastaviti (da) ili odustati (ne): ");
                answer = Console.ReadLine()?.ToLower().Trim();

                if (answer == "da")
                {
                    return true;
                }
                else if (answer == "ne")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Krivi unos.");
                }
            }

        }

        public static bool IsValid(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.[a-zA-Z]+$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
    }
}
