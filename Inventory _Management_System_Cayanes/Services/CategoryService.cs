using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Inventory_Management_System_Cayanes.Data;
namespace Inventory_Management_System_Cayanes.Services
{
    public class CategoryService
    {
        private DatabaseHelper _db;
        public CategoryService()
        {
            _db = new DatabaseHelper();
        }
        public List<Models.Category> GetAllCategories()
        {
            List<Models.Category> categories = new List<Models.Category>();
            try
            {
                string query = @"SELECT CategoryID, CategoryName, Description, CreatedDate, IsActive 
                               FROM Categories 
                               WHERE IsActive = True
                               ORDER BY CategoryName";
                DataTable result = _db.ExecuteDataTable(query);
                foreach (DataRow row in result.Rows)
                {
                    categories.Add(new Models.Category
                    {
                        CategoryID = Convert.ToInt32(row["CategoryID"]),
                        CategoryName = row["CategoryName"].ToString(),
                        Description = row["Description"] == DBNull.Value ? "" : row["Description"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                        IsActive = Convert.ToBoolean(row["IsActive"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving categories: " + ex.Message, ex);
            }
            return categories;
        }
        public Models.Category GetCategoryById(int categoryID)
        {
            try
            {
                string query = @"SELECT CategoryID, CategoryName, Description, CreatedDate, IsActive 
                               FROM Categories WHERE CategoryID = @CategoryID";
                OleDbParameter param = DatabaseHelper.CreateParameter("@CategoryID", OleDbType.Integer, 4, categoryID);
                DataTable result = _db.ExecuteDataTable(query, param);
                if (result.Rows.Count > 0)
                {
                    DataRow row = result.Rows[0];
                    return new Models.Category
                    {
                        CategoryID = Convert.ToInt32(row["CategoryID"]),
                        CategoryName = row["CategoryName"].ToString(),
                        Description = row["Description"] == DBNull.Value ? "" : row["Description"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                        IsActive = Convert.ToBoolean(row["IsActive"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving category: " + ex.Message, ex);
            }
        }
        public int AddCategory(string categoryName, string description = "")
        {
            try
            {
                string query = @"INSERT INTO Categories (CategoryName, Description, CreatedDate, IsActive) 
                               VALUES (@CategoryName, @Description, Now(), True)";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    DatabaseHelper.CreateParameter("@CategoryName", OleDbType.VarChar, 255, categoryName),
                    DatabaseHelper.CreateParameter("@Description", OleDbType.VarChar, 500, description ?? "")
                };
                _db.ExecuteNonQuery(query, parameters);
                string idQuery = "SELECT MAX(CategoryID) FROM Categories";
                object result = _db.ExecuteScalar(idQuery);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding category: " + ex.Message, ex);
            }
        }
        public bool UpdateCategory(int categoryID, string categoryName, string description = "")
        {
            try
            {
                string query = @"UPDATE Categories 
                               SET CategoryName = @CategoryName, Description = @Description 
                               WHERE CategoryID = @CategoryID";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    DatabaseHelper.CreateParameter("@CategoryName", OleDbType.VarChar, 255, categoryName),
                    DatabaseHelper.CreateParameter("@Description", OleDbType.VarChar, 500, description ?? ""),
                    DatabaseHelper.CreateParameter("@CategoryID", OleDbType.Integer, 4, categoryID)
                };
                return _db.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating category: " + ex.Message, ex);
            }
        }
        public bool DeleteCategory(int categoryID)
        {
            try
            {
                string query = "UPDATE Categories SET IsActive = False WHERE CategoryID = @CategoryID";
                OleDbParameter param = DatabaseHelper.CreateParameter("@CategoryID", OleDbType.Integer, 4, categoryID);
                return _db.ExecuteNonQuery(query, param) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting category: " + ex.Message, ex);
            }
        }
        public bool CategoryNameExists(string categoryName, int excludeCategoryID = 0)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Categories WHERE CategoryName = @CategoryName AND CategoryID <> @CategoryID AND IsActive = True";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    DatabaseHelper.CreateParameter("@CategoryName", categoryName),
                    DatabaseHelper.CreateParameter("@CategoryID", excludeCategoryID)
                };
                int count = Convert.ToInt32(_db.ExecuteScalar(query, parameters));
                return count > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
