using System;
namespace Inventory_Management_System_Cayanes.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public Item()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            StockQuantity = 0;
            ReorderLevel = 10;
        }
        public Item(int itemID, string itemCode, string productName, int categoryID, 
                   string categoryName, string description, decimal unitPrice, 
                   int stockQuantity, int reorderLevel)
        {
            ItemID = itemID;
            ItemCode = itemCode;
            ProductName = productName;
            CategoryID = categoryID;
            CategoryName = categoryName;
            Description = description;
            UnitPrice = unitPrice;
            StockQuantity = stockQuantity;
            ReorderLevel = reorderLevel;
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
        public bool IsLowStock()
        {
            return StockQuantity <= ReorderLevel;
        }
        public override string ToString()
        {
            return $"{ProductName} ({ItemCode})";
        }
    }
}
