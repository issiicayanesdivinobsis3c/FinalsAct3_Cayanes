using System;
using System.Data;
using System.Data.OleDb;
using Inventory_Management_System_Cayanes.Data;
namespace Inventory_Management_System_Cayanes.Services
{
    public class TransactionLogger
    {
        private DatabaseHelper _db;
        public TransactionLogger()
        {
            _db = new DatabaseHelper();
        }
        public bool LogTransaction(int userID, string actionType, string tableName, 
                                   int? recordID, string oldValues = null, string newValues = null, 
                                   string status = "Success", string errorMessage = null)
        {
            try
            {
                string query = @"INSERT INTO TransactionLogs 
                               (UserID, ActionType, TableName, RecordID, OldValues, NewValues, ActionDate, Status, ErrorMessage)
                               VALUES 
                               (@UserID, @ActionType, @TableName, @RecordID, @OldValues, @NewValues, Now(), @Status, @ErrorMessage)";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    DatabaseHelper.CreateParameter("@UserID", OleDbType.Integer, 4, userID),
                    DatabaseHelper.CreateParameter("@ActionType", OleDbType.VarChar, 50, actionType),
                    DatabaseHelper.CreateParameter("@TableName", OleDbType.VarChar, 100, tableName),
                    DatabaseHelper.CreateParameter("@RecordID", OleDbType.Integer, 4, (object)recordID ?? DBNull.Value),
                    DatabaseHelper.CreateParameter("@OldValues", OleDbType.LongVarChar, 5000, oldValues ?? ""),
                    DatabaseHelper.CreateParameter("@NewValues", OleDbType.LongVarChar, 5000, newValues ?? ""),
                    DatabaseHelper.CreateParameter("@Status", OleDbType.VarChar, 50, status),
                    DatabaseHelper.CreateParameter("@ErrorMessage", OleDbType.LongVarChar, 5000, errorMessage ?? "")
                };
                return _db.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error logging transaction: " + ex.Message);
                return false;
            }
        }
        public void LogItemAdded(int userID, int itemID, string itemCode, string productName)
        {
            string newValues = $"ItemCode: {itemCode}, ProductName: {productName}, CreatedDate: {DateTime.Now}";
            LogTransaction(userID, "ADD", "Items", itemID, null, newValues);
        }
        public void LogItemUpdated(int userID, int itemID, string oldValues, string newValues)
        {
            LogTransaction(userID, "UPDATE", "Items", itemID, oldValues, newValues);
        }
        public void LogItemDeleted(int userID, int itemID, string itemCode, string productName)
        {
            string oldValues = $"ItemCode: {itemCode}, ProductName: {productName}";
            LogTransaction(userID, "DELETE", "Items", itemID, oldValues, null);
        }
        public void LogItemSearch(int userID, string searchTerm)
        {
            LogTransaction(userID, "SEARCH", "Items", null, null, $"SearchTerm: {searchTerm}");
        }
        public void LogUserLogin(int userID, bool success)
        {
            string status = success ? "Success" : "Failed";
            LogTransaction(userID, "LOGIN", "Users", userID, null, null, status);
        }
        public void LogUserLogout(int userID)
        {
            LogTransaction(userID, "LOGOUT", "Users", userID, null, null);
        }
        public void LogFailedOperation(int userID, string actionType, string tableName, string errorMessage)
        {
            LogTransaction(userID, actionType, tableName, null, null, null, "Failed", errorMessage);
        }
        public int GetUserTransactionCount(int userID)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM TransactionLogs WHERE UserID = @UserID";
                OleDbParameter param = DatabaseHelper.CreateParameter("@UserID", userID);
                object result = _db.ExecuteScalar(query, param);
                return Convert.ToInt32(result ?? 0);
            }
            catch
            {
                return 0;
            }
        }
        public DataTable GetRecentTransactions(int numberOfRecords = 100)
        {
            try
            {
                string query = $@"SELECT TOP {numberOfRecords} 
                               tl.LogID, u.Username, tl.ActionType, tl.TableName, 
                               tl.RecordID, tl.ActionDate, tl.Status
                               FROM TransactionLogs tl
                               LEFT JOIN Users u ON tl.UserID = u.UserID
                               ORDER BY tl.ActionDate DESC";
                return _db.ExecuteDataTable(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving transaction logs: " + ex.Message, ex);
            }
        }
        public int DeleteOldTransactionLogs(int daysToKeep = 90)
        {
            try
            {
                string query = $@"DELETE FROM TransactionLogs 
                               WHERE ActionDate < DateAdd('d', -{daysToKeep}, Now())";
                return _db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting old logs: " + ex.Message, ex);
            }
        }
    }
}
