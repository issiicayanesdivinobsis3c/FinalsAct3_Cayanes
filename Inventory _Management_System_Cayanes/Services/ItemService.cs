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

                // IMPORTANT: Do NOT select Description (Memo field) together with JOIN.

                // Access throws "Cannot join Memo, OLE, or Hyperlink Object".

                string query = @"SELECT Items.ItemID, Items.ItemCode, Items.ProductName, Items.CategoryID, 

                               Items.Description, 
                               Items.UnitPrice, Items.StockQuantity, 

                               Items.ReorderLevel, Items.CreatedDate, Items.CreatedBy, Items.ModifiedDate, 

                               Items.ModifiedBy, Items.IsActive

                               FROM Items

                               WHERE Items.IsActive <> False

                               ORDER BY Items.ProductName";

                DataTable result = _db.ExecuteDataTable(query);

                foreach (DataRow row in result.Rows)

                {

                    items.Add(MapRowToItem(row, includeDescription: true));

                }

            }

            catch (Exception ex)

            {

                throw new Exception("Error retrieving items: " + ex.GetBaseException().Message, ex);

            }

            ApplyCategoryNames(items);

            return items;

        }



        public Item GetItemById(int itemID)

        {

            try

            {

                // Single table query - safe to select Description

                string query = @"SELECT ItemID, ItemCode, ProductName, CategoryID, 

                               Description, UnitPrice, StockQuantity, 

                               ReorderLevel, CreatedDate, CreatedBy, ModifiedDate, 

                               ModifiedBy, IsActive

                               FROM Items

                               WHERE ItemID = @ItemID";

                OleDbParameter param = DatabaseHelper.CreateParameter("@ItemID", OleDbType.Integer, 4, itemID);

                DataTable result = _db.ExecuteDataTable(query, param);

                if (result.Rows.Count > 0)

                {

                    Item item = MapRowToItem(result.Rows[0], includeDescription: true);



                    // Get category name separately (safe)

                    try

                    {

                        string catQuery = "SELECT CategoryName FROM Categories WHERE CategoryID = @CategoryID";

                        OleDbParameter catParam = DatabaseHelper.CreateParameter("@CategoryID", OleDbType.Integer, 4, item.CategoryID);

                        object catName = _db.ExecuteScalar(catQuery, catParam);

                        item.CategoryName = catName != null && catName != DBNull.Value ? catName.ToString() : "";

                    }

                    catch { item.CategoryName = ""; }



                    return item;

                }

                return null;

            }

            catch (Exception ex)

            {

                throw new Exception("Error retrieving item: " + ex.GetBaseException().Message, ex);

            }

        }



        public List<Item> SearchItems(string searchTerm)

        {

            List<Item> items = new List<Item>();

            try

            {

                string query = @"SELECT Items.ItemID, Items.ItemCode, Items.ProductName, Items.CategoryID, 

                               Items.Description, 
                               Items.UnitPrice, Items.StockQuantity, 

                               Items.ReorderLevel, Items.CreatedDate, Items.CreatedBy, Items.ModifiedDate, 

                               Items.ModifiedBy, Items.IsActive

                               FROM Items

                               WHERE Items.IsActive <> False 

                               AND (Items.ItemCode LIKE @SearchCode OR Items.ProductName LIKE @SearchName)

                               ORDER BY Items.ProductName";

                string like = "%" + (searchTerm ?? "") + "%";

                // OLE DB binds parameters by position, so each placeholder needs its own parameter.

                OleDbParameter[] parameters = new OleDbParameter[]

                {

                    DatabaseHelper.CreateParameter("@SearchCode", OleDbType.VarChar, 255, like),

                    DatabaseHelper.CreateParameter("@SearchName", OleDbType.VarChar, 255, like)

                };

                DataTable result = _db.ExecuteDataTable(query, parameters);

                foreach (DataRow row in result.Rows)

                {

                    items.Add(MapRowToItem(row, includeDescription: true));

                }

            }

            catch (Exception ex)

            {

                throw new Exception("Error searching items: " + ex.GetBaseException().Message, ex);

            }

            ApplyCategoryNames(items);

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

                    DatabaseHelper.CreateParameter("@Description", OleDbType.VarChar, 255,

                        (item.Description ?? "").Length > 255 ? (item.Description ?? "").Substring(0, 255) : (item.Description ?? "")),

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

                throw new Exception("Error adding item: " + ex.GetBaseException().Message, ex);

            }

        }



        public bool UpdateItem(Item item, int userID)

        {

            try

            {

                string query = @"UPDATE Items

                               SET ItemCode = @ItemCode, ProductName = @ProductName, CategoryID = @CategoryID,

                                   Description = @Description, UnitPrice = @UnitPrice,

                                   StockQuantity = @StockQuantity, ReorderLevel = @ReorderLevel,

                                   ModifiedBy = @ModifiedBy, ModifiedDate = Now()

                               WHERE ItemID = @ItemID";

                OleDbParameter[] parameters = new OleDbParameter[]

                {

                    DatabaseHelper.CreateParameter("@ItemCode", OleDbType.VarChar, 50, item.ItemCode),

                    DatabaseHelper.CreateParameter("@ProductName", OleDbType.VarChar, 255, item.ProductName),

                    DatabaseHelper.CreateParameter("@CategoryID", OleDbType.Integer, 4, item.CategoryID),

                    DatabaseHelper.CreateParameter("@Description", OleDbType.VarChar, 255,

                        (item.Description ?? "").Length > 255 ? (item.Description ?? "").Substring(0, 255) : (item.Description ?? "")),

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

                throw new Exception("Error updating item: " + ex.GetBaseException().Message, ex);

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

                throw new Exception("Error deleting item: " + ex.GetBaseException().Message, ex);

            }

        }



        public List<Item> GetLowStockItems()

        {

            List<Item> items = new List<Item>();

            try

            {

                string query = @"SELECT Items.ItemID, Items.ItemCode, Items.ProductName, Items.CategoryID, 

                               Items.Description, 
                               Items.UnitPrice, Items.StockQuantity, 

                               Items.ReorderLevel, Items.CreatedDate, Items.CreatedBy, Items.ModifiedDate, 

                               Items.ModifiedBy, Items.IsActive

                               FROM Items

                               WHERE Items.IsActive <> False AND Items.StockQuantity <= Items.ReorderLevel

                               ORDER BY Items.ProductName";

                DataTable result = _db.ExecuteDataTable(query);

                foreach (DataRow row in result.Rows)

                {

                    items.Add(MapRowToItem(row, includeDescription: true));

                }

            }

            catch (Exception ex)

            {

                throw new Exception("Error retrieving low stock items: " + ex.GetBaseException().Message, ex);

            }

            ApplyCategoryNames(items);

            return items;

        }



        public List<Item> GetItemsByCategory(int categoryID)

        {

            List<Item> items = new List<Item>();

            try

            {

                string query = @"SELECT Items.ItemID, Items.ItemCode, Items.ProductName, Items.CategoryID, 

                               Items.Description, 
                               Items.UnitPrice, Items.StockQuantity, 

                               Items.ReorderLevel, Items.CreatedDate, Items.CreatedBy, Items.ModifiedDate, 

                               Items.ModifiedBy, Items.IsActive

                               FROM Items

                               WHERE Items.IsActive <> False AND Items.CategoryID = @CategoryID

                               ORDER BY Items.ProductName";

                OleDbParameter param = DatabaseHelper.CreateParameter("@CategoryID", OleDbType.Integer, 4, categoryID);

                DataTable result = _db.ExecuteDataTable(query, param);

                foreach (DataRow row in result.Rows)

                {

                    items.Add(MapRowToItem(row, includeDescription: true));

                }

            }

            catch (Exception ex)

            {

                throw new Exception("Error retrieving items by category: " + ex.GetBaseException().Message, ex);

            }

            ApplyCategoryNames(items);

            return items;

        }



        public bool ItemCodeExists(string itemCode, int excludeItemID = 0)

        {

            try

            {

                // Soft-deleted items still hold their code (ItemCode is UNIQUE in the database),

                // so every row must be checked, not only active ones.

                string query = "SELECT COUNT(*) FROM Items WHERE ItemCode = @ItemCode AND ItemID <> @ItemID";

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

                string query = "SELECT SUM(StockQuantity * UnitPrice) FROM Items WHERE IsActive <> False";

                object result = _db.ExecuteScalar(query);

                return result == DBNull.Value || result == null ? 0 : Convert.ToDecimal(result);

            }

            catch

            {

                return 0;

            }

        }



        // Looks up category names in a separate query, so no JOIN is needed

        // (Access refuses to join on Memo/OLE/Hyperlink columns).

        private void ApplyCategoryNames(List<Item> items)

        {

            if (items.Count == 0)

            {

                return;

            }

            DataTable categories = _db.ExecuteDataTable("SELECT CategoryID, CategoryName FROM Categories");

            Dictionary<int, string> names = new Dictionary<int, string>();

            foreach (DataRow row in categories.Rows)

            {

                names[Convert.ToInt32(row["CategoryID"])] = row["CategoryName"] == DBNull.Value ? "" : row["CategoryName"].ToString();

            }

            foreach (Item item in items)

            {

                item.CategoryName = names.TryGetValue(item.CategoryID, out string name) ? name : "";

            }

        }



        private Item MapRowToItem(DataRow row, bool includeDescription)

        {

            Item item = new Item

            {

                ItemID = Convert.ToInt32(row["ItemID"]),

                ItemCode = row["ItemCode"].ToString(),

                ProductName = row["ProductName"].ToString(),

                CategoryID = Convert.ToInt32(row["CategoryID"]),

                CategoryName = row.Table.Columns.Contains("CategoryName") && row["CategoryName"] != DBNull.Value

                    ? row["CategoryName"].ToString() : "",

                UnitPrice = Convert.ToDecimal(row["UnitPrice"]),

                StockQuantity = Convert.ToInt32(row["StockQuantity"]),

                ReorderLevel = Convert.ToInt32(row["ReorderLevel"]),

                CreatedDate = Convert.ToDateTime(row["CreatedDate"]),

                CreatedBy = row["CreatedBy"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["CreatedBy"]),

                ModifiedDate = row["ModifiedDate"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["ModifiedDate"]),

                ModifiedBy = row["ModifiedBy"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["ModifiedBy"]),

                IsActive = Convert.ToBoolean(row["IsActive"])

            };



            if (includeDescription && row.Table.Columns.Contains("Description"))

            {

                item.Description = row["Description"] == DBNull.Value ? "" : row["Description"].ToString();

            }

            else

            {

                item.Description = "";

            }



            return item;

        }

    }

}

