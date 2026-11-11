using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Inventory_Management_System_Cayanes.Data
{
    public class DatabaseHelper
    {
        private string _connectionString;
        private string _databasePath;

        public DatabaseHelper()
        {
            _databasePath = DatabaseInitializer.ResolveDatabasePath("InventoryDB.accdb");
            _connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={_databasePath};Persist Security Info=False;Mode=Share Deny None;";
        }

        public DatabaseHelper(string databasePath)
        {
            _databasePath = DatabaseInitializer.ResolveDatabasePath(databasePath);
            _connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={_databasePath};Persist Security Info=False;Mode=Share Deny None;";
        }

        public OleDbConnection GetConnection()
        {
            try
            {
                OleDbConnection connection = new OleDbConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch (OleDbException ex)
            {
                throw new Exception("Database connection failed: " + ex.Message + 
                    "\n\nMake sure Microsoft Access Database Engine (ACE) is installed and matches your platform (x64).", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Database connection failed: " + ex.Message, ex);
            }
        }

        public int ExecuteNonQuery(string query, params OleDbParameter[] parameters)
        {
            using (OleDbConnection connection = GetConnection())
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar(string query, params OleDbParameter[] parameters)
        {
            using (OleDbConnection connection = GetConnection())
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteScalar();
                }
            }
        }

        public DataTable ExecuteDataTable(string query, params OleDbParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (OleDbConnection connection = GetConnection())
                {
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Show the real underlying error (very important for debugging Access queries)
                throw new Exception("Error executing query: " + ex.GetBaseException().Message, ex);
            }
            return dataTable;
        }

        public OleDbDataReader ExecuteReader(string query, params OleDbParameter[] parameters)
        {
            OleDbConnection connection = GetConnection();
            try
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                connection.Close();
                connection.Dispose();
                throw new Exception("Error executing reader: " + ex.GetBaseException().Message, ex);
            }
        }

        public bool TestConnection()
        {
            try
            {
                using (OleDbConnection connection = GetConnection())
                {
                    return connection.State == ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DatabaseExists()
        {
            return File.Exists(_databasePath);
        }

        public string GetDatabasePath()
        {
            return _databasePath;
        }

        public static OleDbParameter CreateParameter(string name, object value)
        {
            return new OleDbParameter(name, value ?? DBNull.Value);
        }

        public static OleDbParameter CreateParameter(string name, OleDbType type, int size, object value)
        {
            return new OleDbParameter(name, type, size) { Value = value ?? DBNull.Value };
        }
    }
}
