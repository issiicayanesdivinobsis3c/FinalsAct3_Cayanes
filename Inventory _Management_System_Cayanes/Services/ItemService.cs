using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Inventory_Management_System_Cayanes.Models;
using Inventory_Management_System_Cayanes.Data;

namespace Inventory_Management_System_Cayanes.Services
{
    public class ItemService
    {
        private DatabaseHelper _db;

        public ItemService()
        {
            _db = new DatabaseHelper();
        }

        public List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();
            try
            {
                string query = @"SELECT i.ItemID, i.ItemCode, i.ProductName, i.CategoryID, c.CategoryName, 
                               i.Description, i.UnitPrice, i.StockQuantity, i.ReorderLevel, i.CreatedDate, 
                               i.CreatedBy, i.ModifiedDate, i.ModifiedBy, i.IsActive
                               FROM Items i
                               LEFT JOIN Categories c ON i.CategoryID = c.CategoryID
                               WHERE i.IsActive = True
                               ORDER BY i.ProductName";
                DataTable result = _db.ExecuteDataTable(query);
                foreach (DataRow row in result.Rows)
                {
                    items.Add(MapRowToItem(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving items: " + ex.Message, ex);
            }
            return items;
        }

        public Item GetItemById(int itemID)
        {
            try
            {
                string query = @"SELECT i.ItemID, i.ItemCode, i.ProductName, i.CategoryID, c.CategoryName, 
                               i.Description, i.UnitPrice, i.StockQuantity, i.ReorderLevel, i.CreatedDate, 
                               i.CreatedBy, i.ModifiedDate, i.ModifiedBy, i.IsActive
                               FROM Items i
                               LEFT JOIN Categories c ON i.CategoryID = c.CategoryID
                               WHERE i.ItemID = @ItemID";
                OleDbParameter param = DatabaseHelper.CreateParameter("@ItemID", itemID);
                DataTable result = _db.ExecuteDataTable(query, param);
                if (result.Rows.Count > 0)
                {
                    return MapRowToItem(result.Rows[0]);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving item: " + ex.Message, ex);
            }
        }

        public List<Item> SearchItems(string searchTerm)
        {
            List<Item> items = new List<Item>();
            try
            {
                // OleDb is positional - need one parameter for each placeholder
                string query = @"SELECT i.ItemID, i.ItemCode, i.ProductName, i.CategoryID, c.CategoryName, 
                               i.Description, i.UnitPrice, i.StockQuantity, i.ReorderLevel, i.CreatedDate, 
                               i.CreatedBy, i.ModifiedDate, i.ModifiedBy, i.IsActive
                               FROM Items i
                               LEFT JOIN Categories c ON i.CategoryID = c.CategoryID
                               WHERE i.IsActive = True AND (i.ItemCode LIKE @SearchTerm1 OR i.ProductName LIKE @SearchTerm2 
                               OR i.Description LIKE @SearchTerm3)
                               ORDER BY i.ProductName";

                string searchPattern = "%" + searchTerm + "%";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    DatabaseHelper.CreateParameter("@SearchTerm1", searchPattern),
                    DatabaseHelper.CreateParameter("@SearchTerm2", searchPattern),
                    DatabaseHelper.CreateParameter("@SearchTerm3", searchPattern)
                };

                DataTable result = _db.ExecuteDataTable(query, parameters);
                foreach (DataRow row in result.Rows)
                {
                    items.Add(MapRowToItem(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching items: " + ex.Message, ex);
            }
            return items;
        }

        public int AddItem(Item item, int userID)
        {
            try
            {
                string query = @"INSERT INTO Items 
                               (ItemCode, ProductName, CategoryID, Description, UnitPrice, StockQuantity, 
                                ReorderLevel, CreatedBy, CreatedDate, IsActive)
                               VALUES 
                               (@ItemCode, @ProductName, @CategoryID, @Description, @UnitPrice, @StockQuantity, 
                                @ReorderLevel, @CreatedBy, Now(), True)";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    DatabaseHelper.CreateParameter("@ItemCode", OleDbType.VarChar, 50, item.ItemCode),
                    DatabaseHelper.CreateParameter("@ProductName", OleDbType.VarChar, 255, item.ProductName),
                    DatabaseHelper.CreateParameter("@CategoryID", OleDbType.Integer, 4, item.CategoryID),
                    DatabaseHelper.CreateParameter("@Description", OleDbType.VarChar, 500, item.Description ?? ""),
                    DatabaseHelper.CreateParameter("@UnitPrice", OleDbType.Currency, 8, item.UnitPrice),
                    DatabaseHelper.CreateParameter("@StockQuantity", OleDbType.Integer, 4, item.StockQuantity),
                    DatabaseHelper.CreateParameter("@ReorderLevel", OleDbType.Integer, 4, item.ReorderLevel),
                    DatabaseHelper.CreateParameter("@CreatedBy", OleDbType.Integer, 4, userID)
                };
                _db.ExecuteNonQuery(query, parameters);
                string idQuery = "SELECT MAX(ItemID) FROM Items";
                object result = _db.ExecuteScalar(idQuery);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding item: " + ex.Message, ex);
            }
        }

        public bool UpdateItem(Item item, int userID)
        {
            try
            {
                string query = @"UPDATE Items 
                               SET ProductName = @ProductName, CategoryID = @CategoryID, 
                                   Description = @Description, UnitPrice = @UnitPrice, 
                                   StockQuantity = @StockQuantity, ReorderLevel = @ReorderLevel,
                                   ModifiedBy = @ModifiedBy, ModifiedDate = Now()
                               WHERE ItemID = @ItemID";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    DatabaseHelper.CreateParameter("@ProductName", OleDbType.VarChar, 255, item.ProductName),
                    DatabaseHelper.CreateParameter("@CategoryID", OleDbType.Integer, 4, item.CategoryID),
                    DatabaseHelper.CreateParameter("@Description", OleDbType.VarChar, 500, item.Description ?? ""),
                    DatabaseHelper.CreateParameter("@UnitPrice", OleDbType.Currency, 8, item.UnitPrice),
                    DatabaseHelper.CreateParameter("@StockQuantity", OleDbType.Integer, 4, item.StockQuantity),
                    DatabaseHelper.CreateParameter("@ReorderLevel", OleDbType.Integer, 4, item.ReorderLevel),
                    DatabaseHelper.CreateParameter("@ModifiedBy", OleDbType.Integer, 4, userID),
                    DatabaseHelper.CreateParameter("@ItemID", OleDbType.Integer, 4, item.ItemID)
                };
                return _db.ExecuteNonQuery(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating item: " + ex.Message, ex);
            }
        }

        public bool DeleteItem(int itemID)
        {
            try
            {
                string query = "UPDATE Items SET IsActive = False WHERE ItemID = @ItemID";
                OleDbParameter param = DatabaseHelper.CreateParameter("@ItemID", OleDbType.Integer, 4, itemID);
                return _db.ExecuteNonQuery(query, param) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting item: " + ex.Message, ex);
            }
        }

        public List<Item> GetLowStockItems()
        {
            List<Item> items = new List<Item>();
            try
            {
                string query = @"SELECT i.ItemID, i.ItemCode, i.ProductName, i.CategoryID, c.CategoryName, 
                               i.Description, i.UnitPrice, i.StockQuantity, i.ReorderLevel, i.CreatedDate, 
                               i.CreatedBy, i.ModifiedDate, i.ModifiedBy, i.IsActive
                               FROM Items i
                               LEFT JOIN Categories c ON i.CategoryID = c.CategoryID
                               WHERE i.IsActive = True AND i.StockQuantity <= i.ReorderLevel
                               ORDER BY i.StockQuantity ASC";
                DataTable result = _db.ExecuteDataTable(query);
                foreach (DataRow row in result.Rows)
                {
                    items.Add(MapRowToItem(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving low stock items: " + ex.Message, ex);
            }
            return items;
        }

        public List<Item> GetItemsByCategory(int categoryID)
        {
            List<Item> items = new List<Item>();
            try
            {
                string query = @"SELECT i.ItemID, i.ItemCode, i.ProductName, i.CategoryID, c.CategoryName, 
                               i.Description, i.UnitPrice, i.StockQuantity, i.ReorderLevel, i.CreatedDate, 
                               i.CreatedBy, i.ModifiedDate, i.ModifiedBy, i.IsActive
                               FROM Items i
                               LEFT JOIN Categories c ON i.CategoryID = c.CategoryID
                               WHERE i.IsActive = True AND i.CategoryID = @CategoryID
                               ORDER BY i.ProductName";
                OleDbParameter param = DatabaseHelper.CreateParameter("@CategoryID", categoryID);
                DataTable result = _db.ExecuteDataTable(query, param);
                foreach (DataRow row in result.Rows)
                {
                    items.Add(MapRowToItem(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving category items: " + ex.Message, ex);
            }
            return items;
        }

        public bool ItemCodeExists(string itemCode, int excludeItemID = 0)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Items WHERE ItemCode = @ItemCode AND ItemID <> @ItemID AND IsActive = True";
                OleDbParameter[] parameters = new OleDbParameter[]
                {
                    DatabaseHelper.CreateParameter("@ItemCode", itemCode),
                    DatabaseHelper.CreateParameter("@ItemID", excludeItemID)
                };
                int count = Convert.ToInt32(_db.ExecuteScalar(query, parameters));
                return count > 0;
            }
            catch
            {
                return false;
            }
        }

        public decimal GetTotalInventoryValue()
        {
            try
            {
                string query = "SELECT SUM(StockQuantity * UnitPrice) FROM Items WHERE IsActive = True";
                object result = _db.ExecuteScalar(query);
                return result == DBNull.Value || result == null ? 0 : Convert.ToDecimal(result);
            }
            catch
            {
                return 0;
            }
        }

        private Item MapRowToItem(DataRow row)
        {
            return new Item
            {
                ItemID = Convert.ToInt32(row["ItemID"]),
                ItemCode = row["ItemCode"].ToString(),
                ProductName = row["ProductName"].ToString(),
                CategoryID = Convert.ToInt32(row["CategoryID"]),
                CategoryName = row["CategoryName"] == DBNull.Value ? "" : row["CategoryName"].ToString(),
                Description = row["Description"] == DBNull.Value ? "" : row["Description"].ToString(),
                UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                StockQuantity = Convert.ToInt32(row["StockQuantity"]),
                ReorderLevel = Convert.ToInt32(row["ReorderLevel"]),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                CreatedBy = row["CreatedBy"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["CreatedBy"]),
                ModifiedDate = row["ModifiedDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["ModifiedDate"]),
                ModifiedBy = row["ModifiedBy"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["ModifiedBy"]),
                IsActive = Convert.ToBoolean(row["IsActive"])
            };
        }
    }
}
