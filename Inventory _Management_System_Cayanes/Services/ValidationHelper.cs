using System;
using System.Text.RegularExpressions;
using Inventory_Management_System_Cayanes.Models;
namespace Inventory_Management_System_Cayanes.Services
{
    public class ValidationHelper
    {
        public static bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;
            return Regex.IsMatch(username, @"^[a-zA-Z0-9_\-]{3,20}$");
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;
            return password.Length >= 6;
        }
        public static bool IsValidItemCode(string itemCode)
        {
            if (string.IsNullOrWhiteSpace(itemCode))
                return false;
            return Regex.IsMatch(itemCode, @"^[a-zA-Z0-9]{3,20}$");
        }
        public static bool IsValidProductName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                return false;
            return productName.Trim().Length >= 3 && productName.Trim().Length <= 150;
        }
        public static bool IsValidNumeric(string value, decimal minValue = 0, decimal maxValue = decimal.MaxValue)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            if (!decimal.TryParse(value, out decimal numValue))
                return false;
            return numValue >= minValue && numValue <= maxValue;
        }
        public static bool IsValidInteger(string value, int minValue = 0, int maxValue = int.MaxValue)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            if (!int.TryParse(value, out int intValue))
                return false;
            return intValue >= minValue && intValue <= maxValue;
        }
        public static bool IsValidCategoryName(string categoryName)
        {
            return !string.IsNullOrWhiteSpace(categoryName) && categoryName.Trim().Length >= 2;
        }
        public static string GetValidationErrorMessage(string fieldName, string validationType)
        {
            if (validationType == "required")
                return $"{fieldName} is required.";
            else if (validationType == "format")
                return $"{fieldName} has invalid format.";
            else if (validationType == "length")
                return $"{fieldName} length is invalid.";
            else if (validationType == "numeric")
                return $"{fieldName} must be a valid number.";
            else if (validationType == "unique")
                return $"{fieldName} already exists.";
            else if (validationType == "range")
                return $"{fieldName} is out of valid range.";
            else
                return $"{fieldName} validation failed.";
        }
    }
}
