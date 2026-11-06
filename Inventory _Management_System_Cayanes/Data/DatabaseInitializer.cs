using System;
using System.Collections.Generic;
using System.IO;
using System.Data.OleDb;

namespace Inventory_Management_System_Cayanes.Data
{
    public class DatabaseInitializer
    {
        public static string ResolveDatabasePath(string databasePath = "InventoryDB.accdb")
        {
            if (string.IsNullOrWhiteSpace(databasePath))
            {
                databasePath = "InventoryDB.accdb";
            }

            if (File.Exists(databasePath))
            {
                return Path.GetFullPath(databasePath);
            }

            string fileName = Path.GetFileName(databasePath);
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "InventoryDB.accdb";
            }

            HashSet<string> candidatePaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            candidatePaths.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName));
            candidatePaths.Add(Path.Combine(Environment.CurrentDirectory, fileName));

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 6; i++)
            {
                candidatePaths.Add(Path.Combine(currentDirectory, fileName));
                DirectoryInfo parent = Directory.GetParent(currentDirectory);
                if (parent == null)
                {
                    break;
                }
                currentDirectory = parent.FullName;
            }

            foreach (string candidatePath in candidatePaths)
            {
                if (File.Exists(candidatePath))
                {
                    return candidatePath;
                }
            }

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        public static bool InitializeDatabase(string databasePath = "InventoryDB.accdb")
        {
            try
            {
                databasePath = ResolveDatabasePath(databasePath);

                if (!File.Exists(databasePath))
                {
                    System.Diagnostics.Debug.WriteLine($"Database file not found at: {databasePath}");
                    return false;
                }

                System.Diagnostics.Debug.WriteLine($"Using database at: {databasePath}");
                string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath};Persist Security Info=False;";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    if (!TableExists(connection, "Users"))
                    {
                        CreateUsersTable(connection);
                    }
                    if (!TableExists(connection, "Categories"))
                    {
                        CreateCategoriesTable(connection);
                    }
                    if (!TableExists(connection, "Items"))
                    {
                        CreateItemsTable(connection);
                    }
                    if (!TableExists(connection, "TransactionLogs"))
                    {
                        CreateTransactionLogsTable(connection);
                    }
                    if (CountRows(connection, "Users") == 0)
                    {
                        InsertSampleData(connection);
                    }
                    EnsureAdminAccount(connection);
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database initialization error: " + ex.Message);
                return false;
            }
        }

        private static bool TableExists(OleDbConnection connection, string tableName)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM [{tableName}]";
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    cmd.ExecuteScalar();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private static int CountRows(OleDbConnection connection, string tableName)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM [{tableName}]";
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        private static void CreateUsersTable(OleDbConnection connection)
        {
            string query = @"
                CREATE TABLE Users (
                    UserID AUTOINCREMENT PRIMARY KEY,
                    Username TEXT(50) UNIQUE NOT NULL,
                    Email TEXT(100),
                    PasswordHash TEXT(255) NOT NULL,
                    Role TEXT(20) NOT NULL,
                    CreatedDate DATETIME,
                    IsActive YESNO,
                    LastLoginDate DATETIME
                )";
            ExecuteQuery(connection, query);
        }

        private static void CreateCategoriesTable(OleDbConnection connection)
        {
            string query = @"
                CREATE TABLE Categories (
                    CategoryID AUTOINCREMENT PRIMARY KEY,
                    CategoryName TEXT(100) UNIQUE NOT NULL,
                    Description TEXT(255),
                    CreatedDate DATETIME,
                    IsActive YESNO
                )";
            ExecuteQuery(connection, query);
        }

        private static void CreateItemsTable(OleDbConnection connection)
        {
            string query = @"
                CREATE TABLE Items (
                    ItemID AUTOINCREMENT PRIMARY KEY,
                    ItemCode TEXT(50) UNIQUE NOT NULL,
                    ProductName TEXT(150) NOT NULL,
                    CategoryID LONG NOT NULL,
                    Description TEXT(500),
                    UnitPrice CURRENCY NOT NULL,
                    StockQuantity LONG NOT NULL,
                    ReorderLevel LONG NOT NULL,
                    CreatedDate DATETIME,
                    CreatedBy LONG,
                    ModifiedDate DATETIME,
                    ModifiedBy LONG,
                    IsActive YESNO
                )";
            ExecuteQuery(connection, query);
        }

        private static void CreateTransactionLogsTable(OleDbConnection connection)
        {
            string query = @"
                CREATE TABLE TransactionLogs (
                    LogID AUTOINCREMENT PRIMARY KEY,
                    UserID LONG NOT NULL,
                    ActionType TEXT(50) NOT NULL,
                    TableName TEXT(50) NOT NULL,
                    RecordID LONG,
                    OldValues MEMO,
                    NewValues MEMO,
                    ActionDate DATETIME,
                    IPAddress TEXT(50),
                    Status TEXT(20),
                    ErrorMessage MEMO
                )";
            ExecuteQuery(connection, query);
        }

        private static void ExecuteQuery(OleDbConnection connection, string query)
        {
            try
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.CommandTimeout = 30;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error executing query: " + ex.Message);
            }
        }

        private static void InsertSampleData(OleDbConnection connection)
        {
            try
            {
                // Both accounts use password: 123456
                string adminHash = HashPassword("123456");
                string userHash = HashPassword("123456");

                string insertAdmin = "INSERT INTO Users (Username, Email, PasswordHash, Role, IsActive, CreatedDate) VALUES ('admin', 'admin@inventory.com', @PasswordHash, 'Admin', True, Now())";
                using (OleDbCommand cmd = new OleDbCommand(insertAdmin, connection))
                {
                    cmd.Parameters.AddWithValue("@PasswordHash", adminHash);
                    cmd.ExecuteNonQuery();
                }

                string insertUser = "INSERT INTO Users (Username, Email, PasswordHash, Role, IsActive, CreatedDate) VALUES ('user', 'user@inventory.com', @PasswordHash, 'User', True, Now())";
                using (OleDbCommand cmd = new OleDbCommand(insertUser, connection))
                {
                    cmd.Parameters.AddWithValue("@PasswordHash", userHash);
                    cmd.ExecuteNonQuery();
                }

                string[] categories = new string[]
                {
                    "Electronics",
                    "Office Supplies",
                    "Furniture",
                    "Software",
                    "Hardware"
                };
                foreach (string category in categories)
                {
                    string insertCategory = "INSERT INTO Categories (CategoryName, Description, IsActive, CreatedDate) VALUES (@CategoryName, 'Sample category', True, Now())";
                    using (OleDbCommand cmd = new OleDbCommand(insertCategory, connection))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", category);
                        cmd.ExecuteNonQuery();
                    }
                }

                string insertItem = @"INSERT INTO Items (ItemCode, ProductName, CategoryID, Description, UnitPrice, StockQuantity, ReorderLevel, CreatedBy, IsActive, CreatedDate) 
                                    VALUES (@ItemCode, @ProductName, 1, 'Sample item', 100.00, 10, 5, 1, True, Now())";
                string[] itemCodes = new string[] { "IT001", "IT002", "IT003" };
                string[] itemNames = new string[] { "Sample Item 1", "Sample Item 2", "Sample Item 3" };
                for (int i = 0; i < itemCodes.Length; i++)
                {
                    using (OleDbCommand cmd = new OleDbCommand(insertItem, connection))
                    {
                        cmd.Parameters.AddWithValue("@ItemCode", itemCodes[i]);
                        cmd.Parameters.AddWithValue("@ProductName", itemNames[i]);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error inserting sample data: " + ex.Message);
            }
        }

        private static void EnsureAdminAccount(OleDbConnection connection)
        {
            string adminHash = HashPassword("123456");
            string checkAdmin = "SELECT COUNT(*) FROM Users WHERE Username = 'admin'";
            using (OleDbCommand command = new OleDbCommand(checkAdmin, connection))
            {
                if (Convert.ToInt32(command.ExecuteScalar()) > 0)
                {
                    string updateAdmin = "UPDATE Users SET PasswordHash = @PasswordHash, Role = 'Admin', IsActive = True WHERE Username = 'admin'";
                    using (OleDbCommand updateCommand = new OleDbCommand(updateAdmin, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@PasswordHash", adminHash);
                        updateCommand.ExecuteNonQuery();
                    }
                    return;
                }
            }

            string insertAdmin = "INSERT INTO Users (Username, Email, PasswordHash, Role, IsActive, CreatedDate) VALUES ('admin', 'admin@inventory.com', @PasswordHash, 'Admin', True, Now())";
            using (OleDbCommand insertCommand = new OleDbCommand(insertAdmin, connection))
            {
                insertCommand.Parameters.AddWithValue("@PasswordHash", adminHash);
                insertCommand.ExecuteNonQuery();
            }
        }

        private static string HashPassword(string password)
        {
            using (var sha = System.Security.Cryptography.SHA1.Create())
            {
                byte[] hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }
    }
}
