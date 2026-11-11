using System;
using Inventory_Management_System_Cayanes.Models;
namespace Inventory_Management_System_Cayanes.Services
{
    public class PermissionManager
    {
        public static bool CanAddItem(User user)
        {
            return user != null && user.IsActive;
        }
        public static bool CanEditItem(User user)
        {
            return user != null && user.IsActive;
        }
        public static bool CanDeleteItem(User user)
        {
            return user != null && user.IsActive && user.IsAdmin();
        }
        public static bool CanViewReports(User user)
        {
            return user != null && user.IsActive;
        }
        public static bool CanManageCategories(User user)
        {
            return user != null && user.IsActive && user.IsAdmin();
        }
        public static bool CanManageUsers(User user)
        {
            return user != null && user.IsActive && user.IsAdmin();
        }
        public static bool CanViewTransactionLogs(User user)
        {
            return user != null && user.IsActive && user.IsAdmin();
        }
        public static bool CanExportData(User user)
        {
            return user != null && user.IsActive && user.IsAdmin();
        }
        public static int GetPermissionLevel(User user)
        {
            if (user == null || !user.IsActive)
                return 0;
            return user.IsAdmin() ? 2 : 1;
        }
        public static string GetPermissionDescription(User user)
        {
            if (user == null || !user.IsActive)
                return "No access";
            if (user.IsAdmin())
                return "Administrator - Full access to all features";
            else
                return "User - Can view and add inventory items";
        }
    }
}
