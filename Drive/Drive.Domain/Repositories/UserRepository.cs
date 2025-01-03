using Drive.Data.Entities;
using Drive.Data.Entities.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Drive.Domain.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(DriveDbContext dbContext) : base(dbContext) { }
        public static bool EmailExists(string email)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                return context.Users.Any(u => u.Email == email);
            }
        }
        public static bool ConfirmPassword(string email, string password)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                return context.Users.Any(u => u.Email == email && u.Password == password);
            }
        }
        public static User GetUserByEmail(string email)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                return context.Users.FirstOrDefault(u => u.Email == email);
            }
        }
        public static void AddUser(string email, string password)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var user = new User
                {
                    Email = email,
                    Password = password
                };
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        public static string GenerateCaptcha()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string captcha = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());

            if (!captcha.Any(char.IsDigit) || !captcha.Any(char.IsLetter))
                return GenerateCaptcha();

            return captcha;
        }
        public static bool CheckCaptcha(string generatedCaptcha, string enteredCaptcha)
        {
            if (generatedCaptcha == enteredCaptcha)
            {
                return true;
            }
            else
                return false;
        }
        public static User ChangeAccountEmail(string newEmail, User loggedUser)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var user = context.Users.AsTracking().FirstOrDefault(u => u.Email == loggedUser.Email);
                user.Email = newEmail;
                context.SaveChanges();
                return user;
            }
        }
        public static void ChangeAccountPassword(string newPassword, User loggedUser)
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var user = context.Users.AsTracking().FirstOrDefault(u => u.Email == loggedUser.Email);
                user.Password = newPassword;
                context.SaveChanges();
            }
        }
        public static void ListAllUsers()
        {
            using (var context = new DriveDbContext(new DbContextOptions<DriveDbContext>()))
            {
                var users = context.Users.ToList();
                foreach (var user in users)
                {
                    Console.WriteLine($"Id: {user.Id}, Email: {user.Email}");
                }
            }
        }

    }
}
