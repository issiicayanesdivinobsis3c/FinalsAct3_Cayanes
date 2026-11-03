using System;
namespace Inventory_Management_System_Cayanes.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // 'Admin' or 'User'
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public User()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
        public User(int userID, string username, string email, string role, bool isActive)
        {
            UserID = userID;
            Username = username;
            Email = email;
            Role = role;
            IsActive = isActive;
        }
        public bool IsAdmin()
        {
            return Role?.Equals("Admin", StringComparison.OrdinalIgnoreCase) ?? false;
        }
        public override string ToString()
        {
            return $"{Username} ({Role})";
        }
    }
}
