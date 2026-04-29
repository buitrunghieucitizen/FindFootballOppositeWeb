using System.Linq;
using FindFootballOppsite.Models;

namespace FindFootballOppsite.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Add roles if not exist
            string[] roles = { "Admin", "StadiumOwner", "Captain", "Player" };
            foreach (var role in roles)
            {
                if (!context.Roles.Any(r => r.RoleName == role))
                {
                    context.Roles.Add(new Role { RoleName = role });
                }
            }
            context.SaveChanges();

            // Add test users
            AddUser(context, "admin", "Admin System", "Admin");
            AddUser(context, "stadiumowner", "Tran Chu San", "StadiumOwner");
            AddUser(context, "captain", "Doi Truong", "Captain");
            AddUser(context, "player", "Cau Thu", "Player");
        }

        private static void AddUser(ApplicationDbContext context, string username, string fullName, string roleName)
        {
            if (!context.Users.Any(u => u.Username == username))
            {
                var user = new User
                {
                    Username = username,
                    FullName = fullName,
                    Phone = "0987654321",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Pass12345", 12),
                    IsFreeAgent = false,
                    CreatedAt = System.DateTime.UtcNow
                };
                context.Users.Add(user);
                context.SaveChanges();

                var role = context.Roles.FirstOrDefault(r => r.RoleName == roleName);
                if (role != null)
                {
                    context.UserRoles.Add(new UserRole
                    {
                        UserID = user.UserID,
                        RoleID = role.RoleID
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
