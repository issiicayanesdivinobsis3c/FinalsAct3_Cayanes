using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Inventory_Management_System_Cayanes.Models;
using Inventory_Management_System_Cayanes.Data;
namespace Inventory_Management_System_Cayanes.Services
{
    public class UserService
    {
        private DatabaseHelper _db;
        public UserService()
        {
            _db = new DatabaseHelper();
        }
        public User AuthenticateUser(string username, string password)
        {
            try
            {
                string passwordHash = HashPassword(password);
                string query = @"SELECT UserID, Username, Email, PasswordHash, Role, CreatedDate, IsActive, LastLoginDate 
                               FROM Users 
                               WHERE Username = @Username AND PasswordHash = @PasswordHash AND IsActive = True";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    DatabaseHelper.CreateParameter("@Username", OleDbType.VarChar, 255, username),
                    DatabaseHelper.CreateParameter("@PasswordHash", OleDbType.VarChar, 255, passwordHash)
                };
                DataTable result = _db.ExecuteDataTable(query, parameters);
                if (result.Rows.Count > 0)
                {
                    DataRow row = result.Rows[0];
                    User user = new User
                    {
                        UserID = Convert.ToInt32(row["UserID"]),
                        Username = row["Username"].ToString(),
                        Email = row["Email"].ToString(),
                        Role = row["Role"].ToString(),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        LastLoginDate = row["LastLoginDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["LastLoginDate"])
                    };
                    UpdateLastLoginDate(user.UserID);
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Authentication error: " + ex.Message, ex);
            }
        }
        public User GetUserById(int userID)
        {
            try
            {
                string query = @"SELECT UserID, Username, Email, Role, CreatedDate, IsActive, LastLoginDate 
                               FROM Users WHERE UserID = @UserID";
                OleDbParameter param = DatabaseHelper.CreateParameter("@UserID", OleDbType.Integer, 4, userID);
                DataTable result = _db.ExecuteDataTable(query, param);
                if (result.Rows.Count > 0)
                {
                    DataRow row = result.Rows[0];
                    return new User
                    {
                        UserID = Convert.ToInt32(row["UserID"]),
                        Username = row["Username"].ToString(),
                        Email = row["Email"].ToString(),
                        Role = row["Role"].ToString(),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        LastLoginDate = row["LastLoginDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["LastLoginDate"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user: " + ex.Message, ex);
            }
        }
        public bool AddUser(string username, string email, string password, string role)
        {
            try
            {
                string passwordHash = HashPassword(password);
                string query = @"INSERT INTO Users (Username, Email, PasswordHash, Role, IsActive, CreatedDate) 
                               VALUES (@Username, @Email, @PasswordHash, @Role, True, Now())";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    DatabaseHelper.CreateParameter("@Username", OleDbType.VarChar, 255, username),
                    DatabaseHelper.CreateParameter("@Email", OleDbType.VarChar, 255, email),
                    DatabaseHelper.CreateParameter("@PasswordHash", OleDbType.VarChar, 255, passwordHash),
                    DatabaseHelper.CreateParameter("@Role", OleDbType.VarChar, 50, role)
                };
                return _db.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user: " + ex.Message, ex);
            }
        }
        public bool UpdateUser(int userID, string email, string role)
        {
            try
            {
                string query = @"UPDATE Users SET Email = @Email, Role = @Role WHERE UserID = @UserID";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    DatabaseHelper.CreateParameter("@Email", OleDbType.VarChar, 255, email),
                    DatabaseHelper.CreateParameter("@Role", OleDbType.VarChar, 50, role),
                    DatabaseHelper.CreateParameter("@UserID", OleDbType.Integer, 4, userID)
                };
                return _db.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user: " + ex.Message, ex);
            }
        }
        public bool ChangePassword(int userID, string oldPassword, string newPassword)
        {
            OleDbConnection connection = null;
            try
            {
                User user = GetUserById(userID);
                if (user == null)
                    return false;

                string oldHash = HashPassword(oldPassword);

                // Verify old password is correct
                string checkQuery = @"SELECT COUNT(*) FROM Users WHERE UserID = @UserID AND PasswordHash = @OldPasswordHash";
                OleDbParameter[] checkParams = new OleDbParameter[]
                {
                    new OleDbParameter("@UserID", OleDbType.Integer) { Value = userID },
                    new OleDbParameter("@OldPasswordHash", OleDbType.VarChar, 255) { Value = oldHash }
                };

                int count = Convert.ToInt32(_db.ExecuteScalar(checkQuery, checkParams));
                if (count == 0)
                    return false;

                string newHash = HashPassword(newPassword);

                // Update password with explicit connection and command
                connection = _db.GetConnection();
                string updateQuery = @"UPDATE Users SET PasswordHash = @NewPasswordHash WHERE UserID = @UserID";

                using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                {
                    command.Parameters.Add(new OleDbParameter("@NewPasswordHash", OleDbType.VarChar, 255) { Value = newHash });
                    command.Parameters.Add(new OleDbParameter("@UserID", OleDbType.Integer) { Value = userID });

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected <= 0)
                        return false;
                }
                connection.Close();

                // Verify the password was actually updated
                string verifyQuery = @"SELECT COUNT(*) FROM Users WHERE UserID = @UserID AND PasswordHash = @NewPasswordHash";
                OleDbParameter[] verifyParams = new OleDbParameter[]
                {
                    new OleDbParameter("@UserID", OleDbType.Integer) { Value = userID },
                    new OleDbParameter("@NewPasswordHash", OleDbType.VarChar, 255) { Value = newHash }
                };

                int verifyCount = Convert.ToInt32(_db.ExecuteScalar(verifyQuery, verifyParams));
                return verifyCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error changing password: " + ex.Message, ex);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        public bool ChangeUsername(int userID, string newUsername)
        {
            OleDbConnection connection = null;
            try
            {
                if (UsernameExists(newUsername))
                    return false;

                connection = _db.GetConnection();
                string query = @"UPDATE Users SET Username = @NewUsername WHERE UserID = @UserID";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.Add(new OleDbParameter("@NewUsername", OleDbType.VarChar, 255) { Value = newUsername });
                    command.Parameters.Add(new OleDbParameter("@UserID", OleDbType.Integer) { Value = userID });

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error changing username: " + ex.Message, ex);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            try
            {
                string query = @"SELECT UserID, Username, Email, Role, CreatedDate, IsActive, LastLoginDate FROM Users WHERE IsActive = True";
                DataTable result = _db.ExecuteDataTable(query);
                foreach (DataRow row in result.Rows)
                {
                    users.Add(new User
                    {
                        UserID = Convert.ToInt32(row["UserID"]),
                        Username = row["Username"].ToString(),
                        Email = row["Email"].ToString(),
                        Role = row["Role"].ToString(),
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        LastLoginDate = row["LastLoginDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["LastLoginDate"])
                    });
                }
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users: " + ex.Message, ex);
            }
        }
        public bool UsernameExists(string username)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                OleDbParameter param = DatabaseHelper.CreateParameter("@Username", OleDbType.VarChar, 255, username);
                int count = Convert.ToInt32(_db.ExecuteScalar(query, param));
                return count > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool DeactivateUser(int userID)
        {
            try
            {
                string query = "UPDATE Users SET IsActive = False WHERE UserID = @UserID";
                OleDbParameter param = DatabaseHelper.CreateParameter("@UserID", OleDbType.Integer, 4, userID);
                return _db.ExecuteNonQuery(query, param) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deactivating user: " + ex.Message, ex);
            }
        }
        private void UpdateLastLoginDate(int userID)
        {
            try
            {
                string query = "UPDATE Users SET LastLoginDate = Now() WHERE UserID = @UserID";
                OleDbParameter param = DatabaseHelper.CreateParameter("@UserID", OleDbType.Integer, 4, userID);
                _db.ExecuteNonQuery(query, param);
            }
            catch { }
        }
        private string HashPassword(string password)
        {
            using (var sha = System.Security.Cryptography.SHA1.Create())
            {
                var hashedPassword = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedPassword);
            }
        }
    }
}
